using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueTableViewController : UITableViewController
	{

		public IEnumerable<Team> Teams { get; set; }


		public LeagueTableViewController() : base("LeagueTableViewController", null)
		{
		}

		public LeagueTableViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
			TableView.TableFooterView = new UIView(CGRect.Empty);
			
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Teams.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueTableCell", indexPath);

			var actualTeam = Teams.ElementAt(indexPath.Row);

			(cell.ViewWithTag(200) as UILabel).Text = actualTeam.Standing.ToString();
			(cell.ViewWithTag(201) as UILabel).Text = actualTeam.Name;
			(cell.ViewWithTag(202) as UILabel).Text = actualTeam.Match.ToString();
			(cell.ViewWithTag(203) as UILabel).Text = "2";
			(cell.ViewWithTag(204) as UILabel).Text = "3";
			(cell.ViewWithTag(205) as UILabel).Text = "4";
			(cell.ViewWithTag(206) as UILabel).Text = actualTeam.Scored.ToString() + "-" + actualTeam.Get.ToString();
			(cell.ViewWithTag(207) as UILabel).Text = actualTeam.Points.ToString();

			return cell;
		}


	}
}

