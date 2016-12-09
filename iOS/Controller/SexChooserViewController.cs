using System;
using System.Linq;
using UIKit;

namespace Floorball.iOS
{
	public partial class SexChooserViewController : UIViewController
	{

		public DateTime Year { get; set; }

		public UIViewController Embedded { get; set; }


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
				vc.ActualTeams = vc.Teams.Where(t => t.Sex == "ferfi");
			} 
			else
			{
				if (segue.Identifier == "LeaguesSeque")
				{
					var vc = segue.DestinationViewController as LeaguesViewController;
					vc.Leagues = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeaguesByYear(Year);	
					vc.ActualLeagues = vc.Leagues.Where(l => l.Sex == "ferfi");
					
				}
			}
		}

		partial void SexChanged(UISegmentedControl sender)
		{
			switch (sender.AccessibilityIdentifier)
			{

				case "TeamSexChooser":

					var teamsVC = Embedded as TeamsViewController;

					if (sender.SelectedSegment == 0)
					{
						teamsVC.ActualTeams = teamsVC.Teams.Where(t => t.Sex == "ferfi");
					}
					else
					{
						if (sender.SelectedSegment == 1)
						{
							teamsVC.ActualTeams = teamsVC.Teams.Where(t => t.Sex == "noi");
						}
					}
				
					teamsVC.TableView.ReloadData();

					break;

				case "LeagueSexChooser":


					var leaguesVC = Embedded as LeaguesViewController;

					if (sender.SelectedSegment == 0)
					{
						leaguesVC.ActualLeagues = leaguesVC.Leagues.Where(l => l.Sex == "ferfi");
					}
					else
					{
						if (sender.SelectedSegment == 1)
						{
							leaguesVC.ActualLeagues = leaguesVC.Leagues.Where(l => l.Sex == "noi");
						}
					}

					leaguesVC.TableView.ReloadData();

					break;

				default:
					return;
			}
		}

	}
}

