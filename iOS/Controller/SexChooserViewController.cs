﻿using System;
using System.Linq;
using UIKit;

namespace Floorball.iOS
{
	public partial class SexChooserViewController : UIViewController
	{

		public DateTime Date { get; set; }

		public UIViewController Embedded { get; set; }


		public SexChooserViewController() : base("SexChooserViewController", null)
		{
		}

		public SexChooserViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			Year.Text = Date.Year + "-" + Date.AddYears(1).Year;

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");
			
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (segue.Identifier == "TeamsSegue1")
			{
				var vc = segue.DestinationViewController as TeamsViewController;
				vc.Teams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByYear(Date);
				vc.ActualTeams = vc.Teams.Where(t => t.Gender == GenderEnum.Men);
				Embedded = vc;
			} 
			else
			{
				if (segue.Identifier == "LeaguesSegue")
				{
					var vc = segue.DestinationViewController as LeaguesViewController;
					vc.Leagues = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeaguesByYear(Date);	
					vc.ActualLeagues = vc.Leagues.Where(l => l.Gender == GenderEnum.Men);
					Embedded = vc;
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
						teamsVC.Update(GenderEnum.Men);
					}
					else
					{
						if (sender.SelectedSegment == 1)
						{
							teamsVC.Update(GenderEnum.Women);
						}
					}

					break;

				case "LeagueSexChooser":


					var leaguesVC = Embedded as LeaguesViewController;

					if (sender.SelectedSegment == 0)
					{
						leaguesVC.Update(GenderEnum.Men);
					}
					else
					{
						if (sender.SelectedSegment == 1)
						{
							leaguesVC.Update(GenderEnum.Women);
						}
					}

					break;

				default:
					return;
			}
		}

	}
}

