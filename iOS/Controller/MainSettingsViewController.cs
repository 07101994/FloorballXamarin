using System;

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

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();

		}
	}
}

