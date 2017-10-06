using System;
using System.Linq;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.EntitySearch
{
    public partial class EntitySearchViewController : SearchBaseViewController, EntitySearchScreen
    {

        UISearchController searchController;
		bool searchControllerWasActive;
		bool searchControllerSearchFieldWasFirstResponder;

        ResultsTableViewController resultsTableController;

        public EntitySearchViewController()  { }

        public EntitySearchViewController(IntPtr handle) : base(handle) { }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            try
            {
				var downloadTask = Presenter.GetEntitiesFromServer();

				resultsTableController = Storyboard.InstantiateViewController("ResultsTableViewController") as ResultsTableViewController;
                resultsTableController.Presenter = Presenter;

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

				await downloadTask;

				TableView.ReloadData();
            }
            catch (Exception ex)
            {
                AppDelegate.SharedAppDelegate.ShowErrorMessage(this,ex.Message);
            }

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		[Export("updateSearchResultsForSearchController:")]
		public virtual void UpdateSearchResultsForSearchController(UISearchController searchController)
		{
			var tableController = (ResultsTableViewController)searchController.SearchResultsController;
			
            Presenter.Search(searchController.SearchBar.Text);
			
            tableController.ReloadData();

		}

        [Export("searchBarSearchButtonClicked:")]
		public virtual void SearchButtonClicked(UISearchBar searchBar)
		{
			searchBar.ResignFirstResponder();
		}
    }
}

