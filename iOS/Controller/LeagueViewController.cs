using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueViewController : UIViewController
	{


		public League League { get; set; }

		public IEnumerable<Team> Teams { get; set; }

		public IEnumerable<Match> Matches { get; set; }

		public IEnumerable<Statistic> Statistics { get; set; }

		public IEnumerable<PlayerStatisticsModel> PlayerStatistics { get; set; }

		public IEnumerable<Player> Players { get; set; }


		public LeagueViewController() : base("LeagueViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
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

				case "LeagueStat":

					var vc = segue.DestinationViewController as LeagueStatContainerViewController;
					vc.Players = Players;
					vc.Teams = Teams;
					vc.PlayerStatistics = PlayerStatisticsMaker.CreatePlayerStatistics(Statistics).OrderByDescending(s => s.Points);

					break;

				case "LeagueMatches":

					var vc2 = segue.DestinationViewController as LeagueMatchesViewController;
					vc2.Teams = Teams;
					vc2.MatchesByRound = Matches.GroupBy(m => m.Round).Select(m => m.OrderBy(m1 => m1.Date).ToList()).ToList();

					break;

				case "LeagueTable":

					var vc1 = segue.DestinationViewController as LeagueTableContainerViewController;
					vc1.Teams = Teams;

					break;

				
				default:
					break;
			}

		}


	}
}

