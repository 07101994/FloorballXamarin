using System;

using UIKit;

namespace FloorballAdminiOS.UI.Navigations
{
    public partial class NavigationsResultsViewController : NavigationsSearchBaseViewController
    {
        public NavigationsResultsViewController(IntPtr handle) : base(handle) { }

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("NavigationCell");

			var model = Presenter.FilteredNavigationModels[indexPath.Section][indexPath.Row];

			cell.TextLabel.Text = model.Title;
			cell.DetailTextLabel.Text = model.Subtitle;

            cell.Accessory = indexPath.Section == 0 ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		public void ReloadData()
		{
			TableView.ReloadData();
		}
    }
}

