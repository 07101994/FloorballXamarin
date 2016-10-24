using System;
using SidebarNavigation;
using UIKit;

namespace Floorball.iOS
{
	public partial class MainViewController : UIViewController
	{

		public SidebarController SidebarController { get; private set; }

		public MainViewController (IntPtr handle) : base (handle)
		{		
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var actualViewController = Storyboard.InstantiateViewController("Actual");
			var menuViewController = Storyboard.InstantiateViewController("SideMenu");

			CreateSideMenuController(actualViewController, menuViewController);

		}

		void CreateSideMenuController(UIViewController actualViewController, UIViewController menuViewController)
		{
			SidebarController = new SidebarController(this, actualViewController, menuViewController);
			SidebarController.HasShadowing = false;
			SidebarController.MenuWidth = 220;
			SidebarController.MenuLocation = MenuLocations.Left;
		}

		partial void button2TouchUpInside(Foundation.NSObject sender)
		{
			var alertView = new UIAlertView("Alert", "Alert", null, "Cancel", "Ok");
			alertView.Show();
		}

		public override void DidReceiveMemoryWarning ()
		{		
			base.DidReceiveMemoryWarning ();		
			// Release any cached data, images, etc that aren't in use.		
		}
	}
}
