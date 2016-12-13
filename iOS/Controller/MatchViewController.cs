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
			//InitProperties();

			//Init outlets
			InitOutlets();
		}

		private void InitOutlets()
		{
			LeagueName.Text = League.Name + " " + Match.Round + ". forduló";
			MatchDate.Text = Match.Date.ToShortDateString();
			StadiumName.Text = Stadium.Name;

			HomeTeamName.Text = HomeTeam.Name;
			AwayTeamName.Text = AwayTeam.Name;
			//HomeTeamImage.Image = new UIImage("phx.png");
			//AwayTeamImage.Image = new UIImage("phx.png");


			Result.Text = Match.GoalsH + " - " + Match.GoalsA;
			ActualTime.Text = Match.Time.Hours == 1 ? "Vége" : Match.Time.Minutes + ":" + Match.Time.Seconds;
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
			switch (segue.Identifier)
			{
				case "MatchEvents":

					InitProperties();

					var vc = segue.DestinationViewController as MatchEventsViewController;
					vc.Events = Events.Where(e => e.Type != "A");
					vc.HomeTeam = HomeTeam;
					vc.AwayTeam = AwayTeam;

					break;

				default:
					break;
			}
		}
	}
}

