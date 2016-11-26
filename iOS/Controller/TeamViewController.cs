using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class TeamViewController : UIViewController
	{


		public Team Team { get; set; }

		public IEnumerable<Player> Players { get; set; }

		public IEnumerable<Match> Matches { get; set; }

		public TeamViewController() : base("TeamViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			TeamName.Text = Team.Name;
			CoachName.Text = Team.Coach;
			StadiumName.Text = AppDelegate.SharedAppDelegate.UoW.StadiumRepo.GetStadiumById(Team.StadiumId).Name;
		
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

				case "PlayersSegue":

					var vc = segue.DestinationViewController as TeamPlayersViewController;
					vc.Players = Players;

					break;

				case "MatchesSegue":

					var vc1 = segue.DestinationViewController as TeamMatchesViewController;
					vc1.MatchesByLeague = Matches.GroupBy(m => m.LeagueId).Select(m => m.ToList()).ToList();

					break;

				default:
					break;
			}
		}

		partial void SegmentControlChanged(UISegmentedControl sender)
		{
			if (sender.Tag == 0)
			{
				MatchesContainer.Hidden = true;
				PlayersContainer.Hidden = false;
			} 
			else
			{
				if (sender.Tag == 1)
				{
					MatchesContainer.Hidden = false;
					PlayersContainer.Hidden = true;
				}
			}
		}
	}
}

