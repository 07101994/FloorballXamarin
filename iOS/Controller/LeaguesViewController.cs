using System;

using UIKit;

namespace Floorball.iOS
{
	public partial class LeaguesViewController : UITableViewController
	{

		public RootViewController Root
		{
			get;
			set;
		}

		public LeaguesViewController() : base("LeaguesViewController", null)
		{
		}

		public LeaguesViewController(IntPtr handle) : base(handle)
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

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}
	}
}

