using Floorball.LocalDB;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR.Client;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity.Abstractions;

namespace Floorball.Signalr
{

    public class FloorballClient
    {

        private static string connectionString = "https://floorball.azurewebsites.net";
        //private static string connectionString = "http://192.168.0.20:8080/";

        public ConnectionState ConnectionState { get; set; }
        public ConnectionState OldConnectionState { get; set; }

        public delegate void ChangedEventHandler(int id);
        public event ChangedEventHandler MatchStarted;
        public event ChangedEventHandler MatchEnded;
        public event ChangedEventHandler NewEventAdded;
        public event ChangedEventHandler EventDeleted;
        public event ChangedEventHandler MatchTimeUpdated;

        public delegate void ConnectionHandler();
        public event ConnectionHandler Connecting;
        public event ConnectionHandler Connected;
        public event ConnectionHandler Disconnected;
        public event ConnectionHandler Reconnecting;

        private static FloorballClient instance;

        public UnitOfWork UoW { get; set; }

        private HubConnection HubConnection;

        public static FloorballClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FloorballClient();
                }

                return instance;
            }
        }

        protected FloorballClient()
        {
            ConnectionState = ConnectionState.Disconnected;
            OldConnectionState = ConnectionState.Disconnected;
            UoW = new UnitOfWork();
            CrossConnectivity.Current.ConnectivityChanged += ConnectivityChanged;
        }

        public async Task<bool> Connect(SortedSet<CountriesEnum> countries)
        {
            HubConnection = new HubConnection(connectionString, CreateQueryStringData(countries));
            HubConnection.Error += HubConnectionError;
            HubConnection.StateChanged += HubConnectionStateChanged;

            IHubProxy hubProxy = HubConnection.CreateHubProxy("FloorballHub");
            
            RegisterClientMethods(hubProxy);

            if (!CrossConnectivity.Current.IsConnected)
            {
                RaiseConnectionEvent(Disconnected);
                return false;
            }

            await ConnectIfCan(CrossConnectivity.Current.IsConnected);
            return true;
        }

        private async Task ConnectIfCan(bool isConnected)
        {
            if (isConnected && ConnectionState == ConnectionState.Disconnected)
            {
                RaiseConnectionEvent(Connecting);
                await HubConnection.Start();
            }
        }

        private void RaiseConnectionEvent(ConnectionHandler e)
        {
            if (e != null)
            {
                e();
            }
            
        }

        private async void HubConnectionStateChanged(StateChange connectionState)
        {
            OldConnectionState = connectionState.OldState;
            ConnectionState = connectionState.NewState;   
            
            if (connectionState.NewState == ConnectionState.Disconnected)
            {
                RaiseConnectionEvent(Disconnected);
                if (CrossConnectivity.Current.IsConnected)
                {
                    RaiseConnectionEvent(Connecting);
                    await HubConnection.Start();
                }
            }
            else
            {
                if (connectionState.NewState == ConnectionState.Connected)
                {
                    RaiseConnectionEvent(Connected);
                }
                else
                {
                    if (connectionState.NewState == ConnectionState.Reconnecting)
                    {
                        RaiseConnectionEvent(Reconnecting);
                    }
                }
            }
             
        }

        private async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            await ConnectIfCan(e.IsConnected);
        }

        private void HubConnectionError(Exception exception)
        {
            string msg = exception.Message;
        }

        private Dictionary<string,string> CreateQueryStringData(SortedSet<CountriesEnum> countries)
        {
            var quersStringData = new Dictionary<string, string>();
            string countriesQueryString = "";

            foreach (var country in countries)
            {
                countriesQueryString += country.ToString() + ";";
            }

            if (countriesQueryString.Length > 0)
            {
                countriesQueryString = countriesQueryString.Remove(countriesQueryString.Length - 1);
            }

            quersStringData.Add("countries", countriesQueryString);

            return quersStringData;
        }

        private void RegisterClientMethods(IHubProxy hubProxy)
        {

            hubProxy.On<EventModel>("AddEventToMatch", AddEventToMatch);
            hubProxy.On<int>("RemoveEventToFrom", RemoveEventFromMatch);
            hubProxy.On<int,StateEnum>("ChangeMatchState", ChangeMatchState);
            hubProxy.On<int,TimeSpan>("UpdateMatchTime", UpdateMatchTime);

        }

        private void RemoveEventFromMatch(int eventId)
        {
            //db
            UoW.EventRepo.RemoveEvent(eventId);

            //ui

        }

        private void UpdateMatchTime(int matchId, TimeSpan newTime)
        {
            //db
            UoW.MatchRepo.UpdateMatchTime(matchId, newTime);

            //ui
            MatchTimeUpdated(matchId);

        }

        private void AddEventToMatch(EventModel e)
        {
            //db
            UoW.EventRepo.AddEvent(e.Id,e.MatchId,e.Type,e.Time,e.PlayerId,e.EventMessageId,e.TeamId);

            //ui
            NewEventAdded(e.Id);

        }

        private void ChangeMatchState(int matchId, StateEnum state)
        {
            //db
            UoW.MatchRepo.UpdateMatchState(matchId, state);

            //ui
            switch (state)
            {
                case StateEnum.Confirmed:
                    
                    break;
                case StateEnum.Playing:
                    MatchStarted(matchId);
                    break;
                case StateEnum.Ended:
                    MatchEnded(matchId);
                    break;
                default:
                    break;
            }
        }
    }
}
