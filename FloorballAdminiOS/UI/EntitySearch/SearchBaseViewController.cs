using System;
using System.Linq;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.EntitySearch
{
    public class SearchBaseViewController : UITableViewController, EntitySearchScreen
    {
        public EntitySearchPresenter<EntitySearchScreen> Presenter { get; set; }

		public SearchBaseViewController()
		{
            
		}

		public SearchBaseViewController(IntPtr handle) : base (handle)
        {
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Presenter.AttachScreen(this);
        }

		public override nint NumberOfSections(UITableView tableView)
		{
			return Presenter.FilteredSearchModel.Count;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Presenter.FilteredSearchModel.ElementAt(Convert.ToInt32(section)).Count();
		}

		public override string TitleForHeader(UITableView tableView, nint section)
		{
			return Presenter.FilteredTitles.ElementAt(Convert.ToInt32(section));
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 66;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("SearchCell");

			var model = Presenter.FilteredSearchModel[Convert.ToInt32(indexPath.Section)][indexPath.Row];

			(cell.ViewWithTag(100) as UILabel).Text = model.Title;
			(cell.ViewWithTag(101) as UILabel).Text = model.Subtitle;
			(cell.ViewWithTag(102) as UILabel).Text = model.RightDetail;

			return cell;
		}
    }
}
