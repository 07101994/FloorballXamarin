using System;
using FloorballAdminiOS.UI.Login;
using FloorballAdminiOS.UI.Matches;
using FloorballAdminiOS.UI.Menu;
using SidebarNavigation;
using UIKit;

namespace FloorballAdminiOS.UI
{
    public partial class RootViewController : UIViewController
    {

        public SidebarController SideBarController { get; set; }

        public RootViewController() : base("RootViewController", null)
        {
        }

		public RootViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//BTProgressHUD.Show("Application initialization");

            var matchesNavController = Storyboard.InstantiateViewController("MatchesNav") as UINavigationController;
			(matchesNavController.ViewControllers[0] as MatchesTableViewController).Root = this;
			//(actualNavController.ViewControllers[0] as ActualViewController).InitProperties();
			
            UIViewController activeController = matchesNavController; ;

			var menuNavController = Storyboard.InstantiateViewController("SideMenu") as UINavigationController;
			
            (menuNavController.ViewControllers[0] as MenuViewController).Root = this;

			SideBarController = CreateSideMenuController(activeController, menuNavController);

		}


		SidebarController CreateSideMenuController(UIViewController actualViewController, UIViewController menuViewController)
		{
			var sidebarController = new SidebarController(this, actualViewController, menuViewController);

			sidebarController.HasShadowing = true;
			sidebarController.MenuWidth = 220;
			sidebarController.MenuLocation = MenuLocations.Left;
			//sidebarController.IsOpen = true;

			return sidebarController;
		}
    }
}

