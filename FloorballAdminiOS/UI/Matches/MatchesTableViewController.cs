using System;

using UIKit;

namespace FloorballAdminiOS.UI.Matches
{
    public partial class MatchesTableViewController : UITableViewController
    {
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
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

