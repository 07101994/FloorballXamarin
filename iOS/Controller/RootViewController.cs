using Foundation;
using System;
using UIKit;
using SidebarNavigation;

namespace Floorball.iOS
{
    public partial class RootViewController : UIViewController
    {

		public SidebarController SideBarController
		{
			get;
			set;
		}


        public RootViewController (IntPtr handle) : base (handle)
        {
        }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


			var actualNavController = Storyboard.InstantiateViewController("ActualNav") as UINavigationController;
			(actualNavController.ViewControllers[0] as ActualViewController).Root = this;

			var menuNavController = Storyboard.InstantiateViewController("SideMenu") as UINavigationController;
			(menuNavController.ViewControllers[0] as MenuViewController).Root = this;

			SideBarController = CreateSideMenuController(actualNavController, menuNavController);

		}


		SidebarController CreateSideMenuController(UIViewController actualViewController, UIViewController menuViewController)
		{
			var sidebarController= new SidebarController(this, actualViewController, menuViewController);

			sidebarController.HasShadowing = true;
			sidebarController.MenuWidth = 220;
			sidebarController.MenuLocation = MenuLocations.Left;

			return sidebarController;
		}


		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

    }
}