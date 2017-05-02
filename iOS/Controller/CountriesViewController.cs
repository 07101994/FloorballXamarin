using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace Floorball.iOS
{
	public partial class CountriesViewController : UITableViewController
	{
		public static List<string> countries = new List<string> { "HU", "SE", "FL", "SW", "CZ" };

		public CountriesViewController() : base("CountriesViewController", null)
		{
		}

		public CountriesViewController(IntPtr handle) : base(handle)
		{
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return countries.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("CountryCell", indexPath);

			cell.TextLabel.Text = countries[indexPath.Row].ToEnum<CountriesEnum>().ToFriendlyString();

			return cell;
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

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{

			switch (segue.Identifier)
			{
				case "LeaguesSegue":

					var vc = segue.DestinationViewController as LeagueChooserViewController;

					var leagues = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeaguesByCountry(countries[TableView.IndexPathForSelectedRow.Row].ToEnum<CountriesEnum>());

					var userdefaults = NSUserDefaults.StandardUserDefaults;

					var models = new List<LeagueChooserViewController.ChooserModel>();

					List<int> userLeaguesIds;

					try
					{
						userLeaguesIds = JsonConvert.DeserializeObject<List<int>>(userdefaults.StringForKey("leagues"));
					}
					catch (Exception)
					{
						userLeaguesIds = new List<int>();					
					}

					foreach (var league in leagues)
					{
						var model = new LeagueChooserViewController.ChooserModel();

						model.Name = league.Name;
						model.Id = league.Id;

						model.Value = userLeaguesIds.Contains(league.Id);

						models.Add(model);
					
					}

					vc.Leagues = models;
					vc.Key = "leagues";

					break;
				default:
					break;
			}


		}

	}
}

