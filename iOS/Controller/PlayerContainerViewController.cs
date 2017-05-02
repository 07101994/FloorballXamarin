using System;
using System.Collections.Generic;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class PlayerContainerViewController : UIViewController
	{

		public Player Player { get; set; }

		public List<List<Statistic>> StatisticsByTeam { get; set; }

		public IEnumerable<Team> Teams { get; set; }

		public IEnumerable<int> MatchCounts { get; set; }

		public PlayerContainerViewController() : base("PlayerContainerViewController", null)
		{
		}

		public PlayerContainerViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			PlayerName.Text = Player.Name;

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (segue.Identifier == "StatSegue")
			{

				var vc = segue.DestinationViewController as PlayerViewController;

				vc.Player = Player;
				vc.Teams = Teams;
				vc.StatisticsByTeam = StatisticsByTeam;


			}
		}

	}
}

