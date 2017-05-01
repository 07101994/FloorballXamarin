using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchEventTabViewController : UIViewController
	{

		public Match Match { get; set; }

		public Team HomeTeam { get; set; }

		public Team AwayTeam { get; set; }

		public League League { get; set; }

		public IEnumerable<Event> Events { get; set; }


		public MatchEventTabViewController() : base("MatchEventTabViewController", null)
		{
		}

		public MatchEventTabViewController(IntPtr handle) : base(handle)
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
			//HomeTeamImage.Image = new UIImage("phx.png");
			//AwayTeamImage.Image = new UIImage("phx.png");

			HomsScore.Text = Match.GoalsH.ToString();
			AwayScore.Text = Match.GoalsA.ToString();
			Time.Text = UIHelper.GetMatchTime(Match.Time, Match.State);
		
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{

			if (segue.Identifier == "MatchEventsSegue")
			{
					var vc = segue.DestinationViewController as MatchEventsViewController;
					vc.Events = Events.Where(e => e.Type != "A");
					vc.HomeTeam = HomeTeam;
					vc.AwayTeam = AwayTeam;
			}


		}
	}
}

