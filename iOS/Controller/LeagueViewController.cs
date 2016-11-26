using System;
using System.Collections.Generic;
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
	}
}

