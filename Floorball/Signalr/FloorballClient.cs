using Floorball.LocalDB;
using FloorballServer.Models.Floorball;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Floorball.Signalr
{

    public class FloorballClient
    {

        private static string connectionString = "http://192.168.0.20:8080/";

        public ConnectionState ConnectionState { get; set; }
        public ConnectionState OldConnectionState { get; set; }

        public delegate void ChangedEventHandler(int id);
        public event ChangedEventHandler MatchStarted;
        public event ChangedEventHandler MatchEnded;
        public event ChangedEventHandler NewEventAdded;


        private static FloorballClient instance;

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
        }

        public async void Connect(SortedSet<CountriesEnum> countries)
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
            // TODO
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
                countriesQueryString.Remove(countriesQueryString.Length - 1);
            }

            quersStringData.Add("countries", countriesQueryString);

            return quersStringData;
        }

        private void RegisterClientMethods(IHubProxy hubProxy)
        {

            hubProxy.On<EventModel>("AddEventToMatch", AddEventToMatch);
            hubProxy.On<int,StateEnum>("ChangeMatchState", ChangeMatchState);

        }

        private void AddEventToMatch(EventModel e)
        {
            Manager.AddEvent(e.Id,e.MatchId,e.Type,e.Time,e.PlayerId,e.EventMessageId,e.TeamId);

            NewEventAdded(e.Id);

        }

        private void ChangeMatchState(int matchId, StateEnum state)
        {

            Manager.UpdateMatchState(matchId, state);

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
