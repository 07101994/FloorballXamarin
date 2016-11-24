using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class RefereesViewController : UITableViewController
	{

		public IEnumerable<Referee> Referees { get; set; }

		public IEnumerable<Referee> ActualReferees { get; set; }

		public RootViewController Root { get; set; }

		public RefereesViewController() : base("RefereesViewController", null)
		{
		}

		public RefereesViewController(IntPtr handle) : base(handle)
		{
			InitProperties();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void InitProperties()
		{
			Referees = Manager.GetAllReferee().OrderBy(p => p.Name).ToList();
			ActualReferees = Referees;
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
			return ActualReferees.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("playercell", indexPath);

			cell.TextLabel.Text = ActualReferees.ElementAt(indexPath.Row).Name;

			return cell;
		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}
	}
}

