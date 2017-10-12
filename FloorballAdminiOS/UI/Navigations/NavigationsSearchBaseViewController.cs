using System;
using System.Collections.Generic;
using System.Linq;
using FloorballAdminiOS.UI.Entity;
using UIKit;

namespace FloorballAdminiOS.UI.Navigations
{
    public class NavigationsSearchBaseViewController : UITableViewController
    {

        public EntityPresenter<EntityScreen> Presenter { get; set; }

        public string SelectedText { get; set; }
        public string NonSelectedText { get; set; }

        public NavigationsSearchBaseViewController() { }

		public NavigationsSearchBaseViewController(IntPtr handle) : base (handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return section == 0 ? SelectedText : NonSelectedText;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return Presenter.FilteredNavigationModels.Count;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return Presenter.FilteredNavigationModels[Convert.ToInt32(section)].Count;
        }

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, false);

            ChangeModel(indexPath.Section,indexPath.Row,(indexPath.Section + 1) % 2);

        }

        protected virtual void ChangeModel(int fromSection, int fromRow, int toSection)
        {

        }

    }
}
