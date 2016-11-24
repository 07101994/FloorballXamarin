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

        public IEnumerable<Player> Players { get; set; }

		public IEnumerable<Player> ActualPlayers { get; set; }

		public RootViewController Root { get; set; }

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

			//Init properties
			InitProperties();

			//Setup header view
			UIView headerView = new UIView();
			headerView.BackgroundColor = UIColor.Red;


			TableView.TableHeaderView = headerView;


		}

		void InitProperties()
		{
			Players = Manager.GetAllPlayer().OrderBy(p => p.Name).ToList();
			ActualPlayers = Players;
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

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}
	}
}

