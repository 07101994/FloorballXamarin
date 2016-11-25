using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace Floorball.iOS
{
	
		public partial class YearsViewController : UITableViewController
		{

			public RootViewController Root { get; set; }

			public IEnumerable<DateTime> Years { get; set; }

			public YearsViewController() : base("LeaguesViewController", null)
			{
			}

			public YearsViewController(IntPtr handle) : base(handle)
			{

			}

			public override void ViewDidLoad()
			{
				base.ViewDidLoad();
				// Perform any additional setup after loading the view, typically from a nib.

				InitProperties();

			}

			void InitProperties()
			{
				Years = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetAllYear();
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}


			public override nint RowsInSection(UITableView tableView, nint section)
			{
				return Years.Count();
			}

			public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("leagueyearcell", indexPath);

				var date = Years.ElementAt(indexPath.Row);

				cell.TextLabel.Text = date.Year.ToString() + "-" + date.AddYears(1).Year.ToString();

				return cell;

			}

			public override void DidReceiveMemoryWarning()
			{
				base.DidReceiveMemoryWarning();
				// Release any cached data, images, etc that aren't in use.
			}

			partial void MenuPressed(UIBarButtonItem sender)
			{
				Root.SideBarController.ToggleMenu();
			}


		}
}


