﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using UIKit;

namespace Floorball.iOS
{
	
	public partial class YearsViewController : UITableViewController
	{
		public RootViewController Root { get; set; }
					
		public IEnumerable<int> Years { get; set; }

		public string NavTitle { get; set; }

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

			TableView.TableFooterView = new UIView(CGRect.Empty);

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");

		}

		void InitProperties()
		{
			Years = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetAllYear();
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return NavTitle;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Years.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("yearcell", indexPath);

			var date = Years.ElementAt(indexPath.Row);

			cell.TextLabel.Text = date + "-" + (date+1);

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



		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			var year = Years.ElementAt(TableView.IndexPathForSelectedRow.Row);

			if (segue.Identifier == "TeamsSegue" || segue.Identifier == "LeaguesSegue")
			{
				var vc = segue.DestinationViewController as SexChooserViewController;
				vc.Date = new DateTime(year,1,1);
			}
		}

	}
}


