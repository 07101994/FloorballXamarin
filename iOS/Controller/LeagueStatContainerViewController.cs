using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueStatContainerViewController : UIViewController
	{


		public IEnumerable<PlayerStatisticsModel> PlayerStatistics { get; set; }
		public IEnumerable<Player> Players { get; set; }
		public IEnumerable<Team> Teams { get; set; }
		

		public LeagueStatContainerViewController() : base("LeagueStatContainerViewController", null)
		{
		}

		public LeagueStatContainerViewController(IntPtr handle) : base(handle)
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

				case "LeagueStat1":

					var vc = segue.DestinationViewController as LeagueStatTableViewController;
					vc.Players = Players;
					vc.Teams = Teams;
					vc.PlayerStatistics = PlayerStatistics;

					break;

				default:
					break;
			}
		}

	}
}

