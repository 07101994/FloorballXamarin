using System;

using UIKit;

namespace Floorball.iOS
{
	public partial class SexChooserViewController : UIViewController
	{

		public DateTime Year { get; set; }

		public SexChooserViewController() : base("SexChooserViewController", null)
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
			if (segue.Identifier == "TeamsSegue")
			{
				var vc = segue.DestinationViewController as TeamsViewController;
				vc.Teams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByYear(Year);
			} 
			else
			{
				if (segue.Identifier == "LeaguesSeque")
				{
					var vc = segue.DestinationViewController as LeaguesViewController;
					vc.Leagues = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeaguesByYear(Year);	
				}
			}
		}
	}
}

