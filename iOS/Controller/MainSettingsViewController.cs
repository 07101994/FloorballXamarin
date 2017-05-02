using System;
using CoreGraphics;
using UIKit;

namespace Floorball.iOS
{
	public partial class MainSettingsViewController : UITableViewController
	{	

		public RootViewController Root { get; set; }

		public MainSettingsViewController() : base("MainSettingsViewController", null)
		{
		}

		public MainSettingsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");

			TableView.TableFooterView = new UIView(CGRect.Empty);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();

		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}
	}
}

