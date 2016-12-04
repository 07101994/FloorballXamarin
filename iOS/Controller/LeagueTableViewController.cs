using System;
using System.Collections.Generic;
using System.Linq;
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

			(cell.ViewWithTag(0) as UILabel).Text = actualTeam.Standing.ToString();
			(cell.ViewWithTag(1) as UILabel).Text = actualTeam.Name;
			(cell.ViewWithTag(2) as UILabel).Text = actualTeam.Match.ToString();
			(cell.ViewWithTag(3) as UILabel).Text = "2";
			(cell.ViewWithTag(4) as UILabel).Text = "3";
			(cell.ViewWithTag(5) as UILabel).Text = "4";
			(cell.ViewWithTag(6) as UILabel).Text = actualTeam.Scored.ToString() + "-" + actualTeam.Get.ToString();
			(cell.ViewWithTag(7) as UILabel).Text = actualTeam.Points.ToString();

			return cell;
		}


	}
}

