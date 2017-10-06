using System;

using UIKit;

namespace FloorballAdminiOS.UI.EntitySearch
{

    public partial class ResultsTableViewController : SearchBaseViewController
    {
        public ResultsTableViewController() : base() { }

		public ResultsTableViewController(IntPtr handle) : base(handle) { }

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

		public void ReloadData()
		{
			TableView.ReloadData();
		}
    }
}

