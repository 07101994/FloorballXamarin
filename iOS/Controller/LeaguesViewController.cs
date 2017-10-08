using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeaguesViewController : UITableViewController
	{

		public IEnumerable<League> Leagues { get; set; }

		public IEnumerable<League> ActualLeagues { get; set; }

		public List<List<League>> LeaguesByCountry { get; set; }

		private const int headerHeight = 40;

		public LeaguesViewController() : base("LeaguesViewController", null)
		{
		}

		public LeaguesViewController(IntPtr handle) : base(handle)
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
			LeaguesByCountry = ActualLeagues.GroupBy(l => l.Country).Select(l => l.ToList()).ToList();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return LeaguesByCountry.Count;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{
			var league = LeaguesByCountry.ElementAt(Convert.ToInt16(section)).First();

            return UIHelper.MakeImageWithLabelInSectionHeader(headerHeight, league.Country.ToString().ToLower(), AppDelegate.SharedAppDelegate.TextManager.GetText(league.Country.ToString()));	
		}

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return headerHeight;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return LeaguesByCountry.ElementAt(Convert.ToInt16(section)).Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueCell", indexPath);

			var league = LeaguesByCountry.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			cell.TextLabel.Text = league.Name;

			return cell;

		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var league = LeaguesByCountry.ElementAt(indexPath.Section).ElementAt(indexPath.Row);

			var vc = Storyboard.InstantiateViewController("LeagueViewController") as LeagueViewController;
			vc.League = league;
			vc.Teams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByLeague(league.Id);
			vc.Matches = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetMatchesByLeague(league.Id);
			vc.Statistics = AppDelegate.SharedAppDelegate.UoW.StatiscticRepo.GetStatisticsByLeague(league.Id);
			vc.PlayerStatistics = PlayerStatisticsMaker.CreatePlayerStatistics(vc.Statistics);
			vc.Players = AppDelegate.SharedAppDelegate.UoW.PlayerRepo.GetPlayersByLeague(league.Id);

			ParentViewController.NavigationController.PushViewController(vc, true);

		}

		public void Update(GenderEnum gender)
		{
			ActualLeagues = Leagues.Where(l => l.Gender == gender);
			InitProperties();
			TableView.ReloadData();

		}
	}
}

