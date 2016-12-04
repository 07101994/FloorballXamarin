using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueMatchesViewController : UITableViewController
	{

		public List<List<Match>> MatchesByRound { get; set; }
		public IEnumerable<Team> Teams { get; set; }


		public LeagueMatchesViewController() : base("LeagueMatchesViewController", null)
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
			return MatchesByRound.Count;
		}


		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return "Round " + (section + 1).ToString();
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return MatchesByRound.ElementAt(Convert.ToInt16(section)).Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueMatchCell", indexPath);

			var match = MatchesByRound.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			(cell.ViewWithTag(0) as UILabel).Text = match.Date.ToString();
			(cell.ViewWithTag(1) as UILabel).Text = Teams.First(t => t.Id == match.HomeTeamId).Name;
			(cell.ViewWithTag(2) as UILabel).Text = match.GoalsH.ToString();
			(cell.ViewWithTag(3) as UILabel).Text = Teams.First(t => t.Id == match.AwayTeamId).Name;
			(cell.ViewWithTag(4) as UILabel).Text = match.GoalsA.ToString();

			return cell;
		}

	}
}

