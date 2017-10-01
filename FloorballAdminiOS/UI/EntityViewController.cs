using System;
using CoreGraphics;
using Floorball;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI
{
    public abstract partial class EntityViewController : UITableViewController
    {
        public UpdateType Crud { get; set; }

        public int SelectedRow { get; set; }

        public EntityViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            
        }

		public EntityViewController(IntPtr handle) : base(handle)
        {

		}

        protected abstract void Save();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            AddTableViewHeader();

            AddSaveButton();
        }

        private void AddSaveButton()
        {
            var button = new UIBarButtonItem();
            button.Style = UIBarButtonItemStyle.Plain;
            button.Title = "Save";

            button.Clicked += SaveClicked;

            NavigationItem.RightBarButtonItem = button;
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            AppDelegate.SharedAppDelegate.ShowConfirmationMessage(this,"Confirm", "Are you sure to save changes?",SavedHandler);
        }

        private void SavedHandler(UIAlertAction obj)
        {
            Save();
        }

        private void AddTableViewHeader()
        {
			TableView.TableFooterView = new UIView(CGRect.Empty);

			var headerView = new UIView(new CGRect(0, 0, TableView.Frame.Size.Width, 44));

			var header = new UILabel(new CGRect(0, 0, TableView.Frame.Size.Width, 44));
			header.Text = "Add League";
			header.TextAlignment = UITextAlignment.Center;

			headerView.AddSubview(header);

			TableView.TableHeaderView = headerView;

		}

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void SizeHeaderToFit()
        {
            var headerView = TableView.TableHeaderView;

            headerView.SetNeedsLayout();
            headerView.LayoutIfNeeded();


        }

		public void ShowConfirmationMessage(string title, string message, Action<UIAlertAction> handler)
		{
			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, handler));
			alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
			PresentViewController(alert, true, null);
		}
    }
}

