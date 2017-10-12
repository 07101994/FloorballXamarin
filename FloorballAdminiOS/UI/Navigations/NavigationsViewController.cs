using System;
using System.Linq;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Navigations
{
    public partial class NavigationsViewController : NavigationsSearchBaseViewController
    {
		UISearchController searchController;
		bool searchControllerWasActive;
		bool searchControllerSearchFieldWasFirstResponder;

        NavigationsResultsViewController resultsTableController;

        public NavigationsViewController() : base() { }

        public NavigationsViewController(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			resultsTableController = Storyboard.InstantiateViewController("NavigationsResultsViewController") as NavigationsResultsViewController;
            resultsTableController.Presenter = Presenter;
            resultsTableController.SelectedText = SelectedText;
            resultsTableController.NonSelectedText = NonSelectedText;

			searchController = new UISearchController(resultsTableController)
			{
				WeakDelegate = this,
				DimsBackgroundDuringPresentation = false,
				WeakSearchResultsUpdater = this
			};

			searchController.SearchBar.SizeToFit();
			TableView.TableHeaderView = searchController.SearchBar;

			resultsTableController.TableView.WeakDelegate = this;
			searchController.SearchBar.WeakDelegate = this;

			DefinesPresentationContext = true;

			if (searchControllerWasActive)
			{
				searchController.Active = searchControllerWasActive;
				searchControllerWasActive = false;

				if (searchControllerSearchFieldWasFirstResponder)
				{
					searchController.SearchBar.BecomeFirstResponder();
					searchControllerSearchFieldWasFirstResponder = false;
				}
			}

        }


		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("NavigationCell");

			var model = Presenter.FilteredNavigationModels[indexPath.Section][indexPath.Row];

			cell.TextLabel.Text = model.Title;
			cell.DetailTextLabel.Text = model.Subtitle;

            cell.Accessory = indexPath.Section == 0 ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		[Export("updateSearchResultsForSearchController:")]
		public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
		{

            Presenter.Search(searchController.SearchBar.Text);

			TableView.ReloadData();

			var tableController = searchController.SearchResultsController as NavigationsResultsViewController;

			tableController.ReloadData();


		}

        protected override void ChangeModel(int fromSection, int fromRow, int toSection)
        {
            int changedId = ChangeFilterModel(fromSection, fromRow, toSection);

            ChangeFullModel(changedId, fromSection, toSection);

			TableView.ReloadData();
			
            var tableController = searchController.SearchResultsController as NavigationsResultsViewController;

			tableController.ReloadData();

        }

        private void ChangeFullModel(int changedId, int fromSection, int toSection)
        {
            var model = Presenter.NavigationModels[fromSection].Single(m => m.Id == changedId);

            Presenter.NavigationModels[fromSection].Remove(model);

			Presenter.NavigationModels[toSection].Add(model);

			Presenter.NavigationModels[toSection] = Presenter.NavigationModels[toSection].OrderBy(n => n.Title).ToList();
        }

        private int ChangeFilterModel(int fromSection, int fromRow, int toSection)
        {
			var model = Presenter.FilteredNavigationModels[fromSection][fromRow];

			Presenter.FilteredNavigationModels[fromSection].Remove(model);

			Presenter.FilteredNavigationModels[toSection].Add(model);

			Presenter.FilteredNavigationModels[toSection] = Presenter.FilteredNavigationModels[toSection].OrderBy(n => n.Title).ToList();

            return model.Id;
        }

        [Export("searchBarSearchButtonClicked:")]
		public virtual void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();
		}
    }
}

