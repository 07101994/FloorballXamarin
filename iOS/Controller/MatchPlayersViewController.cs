using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using Floorball.Util;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchPlayersViewController : UIViewController
	{
		public Match Match { get; set; }

		public Team HomeTeam { get; set; }

		public Team AwayTeam { get; set; }

		//public IEnumerable<Player> HomePlayers { get; set; }

		//public IEnumerable<Player> AwayPlayers { get; set; }

		public IEnumerable<Event> Events { get; set; }

		public MatchPlayersViewController() : base("MatchPlayersViewController", null)
		{
		}

		public MatchPlayersViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitOutlets();

		}

		private void InitOutlets()
		{

			HomeTeamName.Text = HomeTeam.Name;
			AwayTeamName.Text = AwayTeam.Name;

			HomeScore.Text = Match.GoalsH.ToString();
			AwayScore.Text = Match.GoalsA.ToString();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{

			if (segue.Identifier == "MatchStats")
			{
				var vc = segue.DestinationViewController as MatchStatsViewController;
				vc.HomePlayers = HomeTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.RegNum));
            	vc.AwayPlayers = AwayTeam.Players.Intersect(Match.Players, new KeyEqualityComparer<Player>(p => p.RegNum));
				vc.Events = Events;


			}

		}

	}
}

