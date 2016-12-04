using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class RefereeViewController : UITableViewController
	{

		public List<RefereeStatModel> StatsByLeague { get; set; }

		public IEnumerable<League> Leagues { get; set; }

		public Referee Referee { get; set; }

		public RefereeViewController() : base("RefereeViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			NavigationItem.Title = Referee.Name;

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		public override nint NumberOfSections(UITableView tableView)
		{
			return StatsByLeague.Count;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return 1;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{

			var header = tableView.DequeueReusableCell("HeaderCell");

			var league = Leagues.ElementAt(Convert.ToInt16(section));

			header.TextLabel.Text = league.Name;
			header.DetailTextLabel.Text = "(" + league.Year.Year + "-" + (league.Year.Year + 1) + ")";

			return header;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("RefereeStatCell", indexPath);

			var stat = StatsByLeague.ElementAt(indexPath.Section);

			(cell.ViewWithTag(0) as UILabel).Text = stat.NumberOfMatches.ToString();
			(cell.ViewWithTag(1) as UILabel).Text = stat.TwoMinutesPenalties.ToString();
			(cell.ViewWithTag(2) as UILabel).Text = stat.FiveMinutesPenalties.ToString();
			(cell.ViewWithTag(3) as UILabel).Text = stat.TenMinutesPenalties.ToString();
			(cell.ViewWithTag(4) as UILabel).Text = stat.FinalPenalties.ToString();

			return cell;
		}


		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 44;
		}

		private string CratePenalty(IEnumerable<Statistic> stats, int teamId)
		{

			int penaltySum = 0;
			penaltySum += stats.Where(s => s.TeamId == teamId && s.Name == "P2").First().Number * 2;
			penaltySum += stats.Where(s => s.TeamId == teamId && s.Name == "P5").First().Number * 5;
			int p10 = stats.Where(s => s.TeamId == teamId && s.Name == "P10").First().Number * 10;
			penaltySum += p10;

			return penaltySum + " (" + p10 + ") perc";

		}

	}
}

