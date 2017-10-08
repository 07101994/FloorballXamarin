using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
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

		public LeagueStatTableViewController(IntPtr handle) : base(handle)
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
			return PlayerStatistics.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueStatCell", indexPath);

			var actualStat = PlayerStatistics.ElementAt(indexPath.Row);

			(cell.ViewWithTag(200) as UILabel).Text = (indexPath.Row + 1).ToString();
			(cell.ViewWithTag(201) as UILabel).Text = Players.First(p => p.Id == actualStat.PlayerId).Name;
			(cell.ViewWithTag(202) as UILabel).Text = Teams.First(t => t.Id == actualStat.TeamId).Name;
			(cell.ViewWithTag(203) as UILabel).Text = actualStat.Goals.ToString();
			(cell.ViewWithTag(204) as UILabel).Text = actualStat.Assists.ToString();
			(cell.ViewWithTag(205) as UILabel).Text = actualStat.Points.ToString();
			(cell.ViewWithTag(206) as UILabel).Text = actualStat.Penalties;

			return cell;
		}
	}
}

