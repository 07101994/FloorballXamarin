using Floorball.LocalDB;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public event ChangedEventHandler MatchTimeUpdated;

        private static FloorballClient instance;

        public UnitOfWork UoW { get; set; }

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
        }

        public async Task Connect(SortedSet<CountriesEnum> countries)
        {

            var hubConnection = new HubConnection(connectionString, CreateQueryStringData(countries));
            hubConnection.Error += HubConnectionError;
            hubConnection.StateChanged += HubConnectionStateChanged;

            IHubProxy hubProxy = hubConnection.CreateHubProxy("FloorballHub");

            RegisterClientMethods(hubProxy);

            await hubConnection.Start();

        }

        private void HubConnectionStateChanged(StateChange connectionState)
        {
            OldConnectionState = connectionState.OldState;
            ConnectionState = connectionState.NewState;    
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
                countriesQueryString += country.ToFriendlyString() + ";";
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
            hubProxy.On<int,StateEnum>("ChangeMatchState", ChangeMatchState);
            hubProxy.On<int,TimeSpan>("UpdateMatchTime", UpdateMatchTime);

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
