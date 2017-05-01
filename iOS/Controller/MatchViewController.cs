using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchViewController : UIViewController
	{

		public Match Match { get; set; }

		public Team HomeTeam { get; set; }

		public Team AwayTeam { get; set; }

		public League League { get; set; }

		public IEnumerable<Event> Events { get; set; }

		public IEnumerable<EventMessage> EventMessages { get; set; }

		public IEnumerable<Referee> Referees { get; set; }

		public Stadium Stadium { get; set; }

		bool initialized;

		public MatchViewController() : base("MatchViewController", null)
		{
		}

		public MatchViewController(IntPtr handle) : base (handle)
        {
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//Init properties
			if (!initialized)
			{
				InitProperties();
			}

			NavigationItem.Title = "Match";
		}

		partial void SegmentChanged(UISegmentedControl sender)
		{
			if (sender.SelectedSegment == 0)
			{
				EventsContainer.Hidden = false;
				PlayersContainer.Hidden = true;
				DetailsContainer.Hidden = true;
			} 
			else
			{
				if (sender.SelectedSegment == 1)
				{
					EventsContainer.Hidden = true;
					PlayersContainer.Hidden = false;
					DetailsContainer.Hidden = true;
				} 
				else
				{
					if (sender.SelectedSegment == 2)
					{
						EventsContainer.Hidden = true;
						PlayersContainer.Hidden = true;
						DetailsContainer.Hidden = false;
					}
				}
			}
		}

		private void InitProperties()
		{
			HomeTeam = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamById(Match.HomeTeamId);
			AwayTeam = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamById(Match.AwayTeamId);
			League = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeagueById(Match.LeagueId);
			Stadium = AppDelegate.SharedAppDelegate.UoW.StadiumRepo.GetStadiumById(Match.StadiumId);
			Referees = Match.Referees;
			EventMessages = AppDelegate.SharedAppDelegate.UoW.EventMessageRepo.GetAllEventMessage();
			Events = AppDelegate.SharedAppDelegate.UoW.EventRepo.GetEventsByMatch(Match.Id).OrderByDescending(e => e.Time);

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (!initialized)
			{
				InitProperties();
				initialized = true;
			}


			switch (segue.Identifier)
			{
				case "MatchEventsTab":

					var vc = segue.DestinationViewController as MatchEventTabViewController;
					vc.Match = Match;
					vc.HomeTeam = HomeTeam;
					vc.AwayTeam = AwayTeam;
					vc.Events = Events;


					break;

				case "MatchPlayers":

					var vc1 = segue.DestinationViewController as MatchPlayersViewController;
					vc1.Match = Match;
					vc1.HomeTeam = HomeTeam;
					vc1.AwayTeam = AwayTeam;
					vc1.Events = Events;

					break;

				case "MatchDetails":

					var vc2 = segue.DestinationViewController as MatchDetailsViewController;
					vc2.Stadium = Stadium;
					vc2.League = League;
					vc2.Match = Match;

					break;

				default:
					break;
			}
		}
	}
}

