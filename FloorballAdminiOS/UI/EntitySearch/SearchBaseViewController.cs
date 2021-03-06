﻿using System;
using System.Linq;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.EntitySearch
{
    public class SearchBaseViewController : UITableViewController, EntitySearchScreen
    {
        public EntitySearchPresenter<EntitySearchScreen> Presenter { get; set; }

        public EntityPresenter<EntityScreen> EntityPresenter { get; set; }

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

		/*public override string TitleForHeader(UITableView tableView, nint section)
		{
			return Presenter.FilteredTitles.ElementAt(Convert.ToInt32(section));
		}*/

        public override UIView GetViewForHeader(UITableView tableView, nint section)
        {
            var cell = tableView.DequeueReusableCell("HeaderCell");

			(cell.ViewWithTag(100) as UILabel).Text = Presenter.FilteredTitles.ElementAt(Convert.ToInt32(section)).MainTitle;
			(cell.ViewWithTag(101) as UILabel).Text = Presenter.FilteredTitles.ElementAt(Convert.ToInt32(section)).Subtitle;

            return cell;
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 28;
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

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {

            if (segue.Identifier == "UpdateSegue")
            {
                var indexPath = TableView.IndexPathForSelectedRow;

                EntityPresenter.EntityId = Presenter.FilteredSearchModel[Convert.ToInt32(indexPath.Section)][indexPath.Row].Id;

				var controller = segue.DestinationViewController as EntityViewController;
				controller.Crud = UpdateType.Update;
				controller.EntityPresenter = EntityPresenter;
				

            }

        }
    }
}
