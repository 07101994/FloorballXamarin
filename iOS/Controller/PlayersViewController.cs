using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class PlayersViewController : UITableViewController
	{


		public IEnumerable<Player> ActualPlayers { get; set; }

		public UnitOfWork UoW { get; set; }


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

			UoW = new UnitOfWork();

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
			var cell = tableView.DequeueReusableCell("playercell", indexPath);

			cell.TextLabel.Text = ActualPlayers.ElementAt(indexPath.Row).Name;

			return cell;
		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{

			var player = ActualPlayers.ElementAt(indexPath.Row);

			var vc = Storyboard.InstantiateViewController("PlayerViewController") as PlayerViewController;
			vc.Player = player;
			vc.Teams = UoW.TeamRepo.GetTeamsByPlayer(player.RegNum);
			vc.StatisticsByTeam = UoW.StatiscticRepo.GetStatisticsByPlayer(player.RegNum).GroupBy(s => s.TeamId).Select(s => s.ToList()).ToList();


			ParentViewController.NavigationController.PushViewController(vc, true);

		}

	}
}

