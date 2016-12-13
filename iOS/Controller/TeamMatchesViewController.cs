using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class TeamMatchesViewController : UITableViewController
	{

		public List<List<Match>> MatchesByLeague { get; set; }

		public Team Team { get; set; }

		public TeamMatchesViewController() : base("TeamMatchesViewController", null)
		{
		}

		public TeamMatchesViewController(IntPtr handle) : base(handle)
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

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeagueById(MatchesByLeague.ElementAt(Convert.ToInt16(section)).First().LeagueId).Name;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return MatchesByLeague.Count;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return MatchesByLeague.ElementAt(Convert.ToInt16(section)).Count;
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 100;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("TeamMatchCell", indexPath);

			var match = MatchesByLeague.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			(cell.ViewWithTag(200) as UILabel).Text = match.Date.ToString();

			if (Team.Id == match.HomeTeamId)
			{
				(cell.ViewWithTag(201) as UILabel).Text = Team.Name;
				(cell.ViewWithTag(203) as UILabel).Text = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamById(match.AwayTeamId).Name;
			}
			else
			{
				(cell.ViewWithTag(203) as UILabel).Text = Team.Name;
				(cell.ViewWithTag(201) as UILabel).Text = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamById(match.HomeTeamId).Name;
			}

			(cell.ViewWithTag(202) as UILabel).Text = match.GoalsH.ToString();
			(cell.ViewWithTag(204) as UILabel).Text = match.GoalsA.ToString();
			
			return cell;
		}

	}
}

