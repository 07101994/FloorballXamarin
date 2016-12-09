using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class TeamMatchesViewController : UITableViewController
	{

		public List<List<Match>> MatchesByLeague { get; set; }

		public TeamMatchesViewController() : base("TeamMatchesViewController", null)
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
			return MatchesByLeague.Count;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return MatchesByLeague.ElementAt(Convert.ToInt16(section)).Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("MatchCell", indexPath);

			//cell.TextLabel.Text = Players.ElementAt(indexPath.Row).Name;

			return cell;
		}

	}
}

