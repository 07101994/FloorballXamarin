using System;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace FloorballAdminiOS.UI.Matches
{
    public partial class MatchesTableViewController : UITableViewController, MatchesScreen
    {

        public MatchesPresenter MatchesPresenter { get; set; }

        public MatchesTableViewController() : base("MatchesTableViewController", null)
        {
        }

		public MatchesTableViewController(IntPtr handle) : base(handle)
        {

		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            MatchesPresenter = new MatchesPresenter();

        }

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			MatchesPresenter.AttachScreen(this);

			await MatchesPresenter.InitAsync();
			TableView.ReloadData();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			MatchesPresenter.DetachScreen();
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
			return MatchesPresenter.Matches.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
            var cell = tableView.DequeueReusableCell("MatchCell", indexPath) as MatchViewCell;

            var match = MatchesPresenter.Matches.ElementAt(indexPath.Row);

            cell.LeagueName.Text = MatchesPresenter.Leagues.Single(l => l.Id == match.LeagueId).Name;
            cell.Date.Text = match.Date.ToString();
            cell.HomeTeamName.Text = MatchesPresenter.Teams.Single(t => t.Id == match.HomeTeamId).Name;
            cell.AwayTeamName.Text = MatchesPresenter.Teams.Single(t => t.Id == match.AwayTeamId).Name;
			cell.HomeScore.Text = match.GoalsH.ToString();
			cell.AwayScore.Text = match.GoalsA.ToString();

			return cell;
		}

    }
}

