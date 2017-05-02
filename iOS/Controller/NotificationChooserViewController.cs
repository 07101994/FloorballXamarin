using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace Floorball.iOS
{
	public partial class NotificationChooserViewController : UITableViewController
	{
		public NotificationChooserViewController() : base("NotificationChooserViewController", null)
		{
		}

		public NotificationChooserViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			TableView.TableFooterView = new UIView(CGRect.Empty);

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			switch (segue.Identifier)
			{
				case "LeaguesSegue":

					var vc = segue.DestinationViewController as LeagueChooserViewController;

					if (TableView.IndexPathForSelectedRow.Row == 0)
					{
						vc.Key = "onehour";
					} else
					{
						if (TableView.IndexPathForSelectedRow.Row == 1)
						{
							vc.Key = "event";
						} 
						else
						{
							if (TableView.IndexPathForSelectedRow.Row == 2)
							{
								vc.Key = "today";
							}
						}
					}

					var userdefaults = NSUserDefaults.StandardUserDefaults;

					List<int> userLeaguesIds;

					try
					{
						userLeaguesIds = JsonConvert.DeserializeObject<List<int>>(userdefaults.StringForKey("leagues"));
					}
					catch (Exception)
					{
						userLeaguesIds = new List<int>();					
					}

					var models = new List<LeagueChooserViewController.ChooserModel>();

					List<int> leagueIds;

					try
					{
						leagueIds = JsonConvert.DeserializeObject<List<int>>(userdefaults.StringForKey(vc.Key));
					}
					catch (Exception)
					{
						leagueIds = new List<int>();					
					}

					foreach (var league in AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeaguesByIds(userLeaguesIds))
					{

						var model = new LeagueChooserViewController.ChooserModel();

						model.Name = league.Name;

						model.Id = league.Id;

						model.Value = leagueIds.Contains(league.Id);

						models.Add(model);

					}

					vc.Leagues = models;

					break;
				
				default:
					break;
			}
		}

	}
}

