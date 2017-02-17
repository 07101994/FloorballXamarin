using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace Floorball.iOS
{
    public partial class RootViewController : UIViewController
    {

		public SidebarController SideBarController { get; set; }


        public RootViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//BTProgressHUD.Show("Application initialization");

			UIViewController activeController;

			//check first launch
			if (AppDelegate.SharedAppDelegate.LastSyncDate.CompareTo(new DateTime(1900, 12, 12)) == 0)
			{
				activeController = Storyboard.InstantiateViewController("InitViewController");
    		} 
			else
			{
				var actualNavController = Storyboard.InstantiateViewController("ActualNav") as UINavigationController;
				(actualNavController.ViewControllers[0] as ActualViewController).Root = this;
				activeController = actualNavController;;
			}

			var menuNavController = Storyboard.InstantiateViewController("SideMenu") as UINavigationController;
			(menuNavController.ViewControllers[0] as MenuViewController).Root = this;

			SideBarController = CreateSideMenuController(activeController, menuNavController);

		}


		SidebarController CreateSideMenuController(UIViewController actualViewController, UIViewController menuViewController)
		{
			var sidebarController= new SidebarController(this, actualViewController, menuViewController);

			sidebarController.HasShadowing = true;
			sidebarController.MenuWidth = 220;
			sidebarController.MenuLocation = MenuLocations.Left;
			//sidebarController.IsOpen = true;

			return sidebarController;
		}


		public void InitStopped()
		{
			UINavigationController newContent = Storyboard.InstantiateViewController("ActualNav") as UINavigationController;
			(newContent.ViewControllers[0] as ActualViewController).Root = this;

			SideBarController.ChangeContentView(newContent);

			//BTProgressHUD.Dismiss();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

    }
}