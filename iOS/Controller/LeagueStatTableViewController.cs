using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueStatTableViewController : UITableViewController
	{

		public IEnumerable<PlayerStatisticsModel> PlayerStatistics { get; set; }
		public IEnumerable<Player> Players { get; set; }
		public IEnumerable<Team> Teams { get; set; }


		public LeagueStatTableViewController() : base("LeagueStatTableViewController", null)
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
			return PlayerStatistics.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueStatCell", indexPath);

			var actualStat = PlayerStatistics.ElementAt(indexPath.Row);

			(cell.ViewWithTag(0) as UILabel).Text = (indexPath.Row + 1).ToString();
			(cell.ViewWithTag(1) as UILabel).Text = Players.First(p => p.RegNum == actualStat.PlayerId).Name;
			(cell.ViewWithTag(2) as UILabel).Text = Teams.First(t => t.Id == actualStat.TeamId).Name;
			(cell.ViewWithTag(3) as UILabel).Text = actualStat.Goals.ToString();
			(cell.ViewWithTag(4) as UILabel).Text = actualStat.Assists.ToString();
			(cell.ViewWithTag(5) as UILabel).Text = actualStat.Points.ToString();
			(cell.ViewWithTag(6) as UILabel).Text = actualStat.Penalties;

			return cell;
		}
	}
}

