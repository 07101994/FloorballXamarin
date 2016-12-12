using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class TeamsViewController : UITableViewController
	{

		public IEnumerable<Team> Teams { get; set; }

		public IEnumerable<Team> ActualTeams { get; set; }

		public IEnumerable<League> Leagues { get; set; }

		public List<List<Team>> TeamsByLeague { get; set; }

		public TeamsViewController() : base("TeamsViewController", null)
		{
		}

		public TeamsViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			InitProperties();
			TableView.TableFooterView = new UIView(CGRect.Empty);
			
		}

		private void InitProperties()
		{
			TeamsByLeague = ActualTeams.GroupBy(t => t.LeagueId).Select(t => t.ToList()).ToList();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		public override nint NumberOfSections(UITableView tableView)
		{
			return TeamsByLeague.Count;
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeagueById(TeamsByLeague.ElementAt(Convert.ToInt16(section)).First().LeagueId).Name;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return TeamsByLeague.ElementAt(Convert.ToInt16(section)).Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("TeamCell", indexPath);

			var team = TeamsByLeague.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			cell.TextLabel.Text = team.Name;

			return cell;

		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var team = TeamsByLeague.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			var vc = Storyboard.InstantiateViewController("TeamViewController") as TeamViewController;
			vc.Team = team;
			vc.Players = AppDelegate.SharedAppDelegate.UoW.PlayerRepo.GetPlayersByTeam(team.Id).ToList();
			vc.Matches = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetMatchesByTeam(team.Id).OrderBy(m => m.LeagueId).ThenBy(m => m.Date).ToList();

			ParentViewController.NavigationController.PushViewController(vc, true);

		}

		public void Update(string sex)
		{

			ActualTeams = Teams.Where(t => t.Sex == sex);
			InitProperties();
			TableView.ReloadData();
		}
	}
}

