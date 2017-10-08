using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class PlayersViewController : UITableViewController
	{


		public IEnumerable<Player> ActualPlayers { get; set; }



		public PlayersViewController() : base("PlayersViewController", null)
		{
		}

		public PlayersViewController(IntPtr handle) : base(handle)
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
			return ActualPlayers.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("PlayerViewCell", indexPath);

			cell.TextLabel.Text = ActualPlayers.ElementAt(indexPath.Row).Name;

			return cell;
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var player = ActualPlayers.ElementAt(indexPath.Row);

			var vc = Storyboard.InstantiateViewController("PlayerContainerViewController") as PlayerContainerViewController;
			vc.Player = player;
			vc.Teams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByPlayer(player.Id);
			vc.StatisticsByTeam = AppDelegate.SharedAppDelegate.UoW.StatiscticRepo.GetStatisticsByPlayer(player.Id).GroupBy(s => s.TeamId).Select(s => s.ToList()).ToList();
			vc.MatchCounts = player.Matches.GroupBy(m => m.LeagueId).Select(m => m.ToList()).Select(m => m.Count).ToList();

			ParentViewController.NavigationController.PushViewController(vc, true);

		}

	}
}

