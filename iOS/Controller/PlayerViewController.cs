using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class PlayerViewController : UITableViewController
	{

		public Player Player { get; set; }

		public List<List<Statistic>> StatisticsByTeam { get; set; }

		public IEnumerable<Team> Teams { get; set; }

		public IEnumerable<int> MatchCounts { get; set; }

		public PlayerViewController() : base("PlayerViewController", null)
		{
		}

		public PlayerViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//InitProperties();

			//NavigationItem.Title = Player.Name;
			TableView.TableFooterView = new UIView(CGRect.Empty);
			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");
			

		}

		void InitProperties()
		{
			//throw new NotImplementedException();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		public override nint NumberOfSections(UITableView tableView)
		{
			return StatisticsByTeam.Count;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 120;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{

			var header = tableView.DequeueReusableCell("HeaderCell");

			var team = Teams.ElementAt(Convert.ToInt16(section));

			header.TextLabel.Text = team.Name;
			header.DetailTextLabel.Text = "(" + team.Year.Year + "-" + (team.Year.Year + 1) + ")";

			return header;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("PlayerStatCell", indexPath);

			var stats = StatisticsByTeam.ElementAt(indexPath.Section);

			(cell.ViewWithTag(200) as UILabel).Text = stats.First(s => s.Type == StatType.G).Number.ToString();
			(cell.ViewWithTag(201) as UILabel).Text = stats.First(s => s.Type == StatType.A).Number.ToString();
			(cell.ViewWithTag(202) as UILabel).Text = CratePenalty(stats,Teams.ElementAt(indexPath.Section).Id);
			(cell.ViewWithTag(203) as UILabel).Text = MatchCounts.ElementAt(indexPath.Section).ToString();

			return cell;
		}


		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 44;
		}

		private string CratePenalty(IEnumerable<Statistic> stats, int teamId)
		{

			int penaltySum = 0;
			penaltySum += stats.Where(s => s.TeamId == teamId && s.Type == StatType.P2).First().Number * 2;
			penaltySum += stats.Where(s => s.TeamId == teamId && s.Type == StatType.P5).First().Number * 5;
			int p10 = stats.Where(s => s.TeamId == teamId && s.Type == StatType.P10).First().Number * 10;
			penaltySum += p10;

			return penaltySum + " (" + p10 + ") perc";

		}

	}
}

