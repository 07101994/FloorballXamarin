using System;
using BigTed;
using Foundation;
using UIKit;

namespace Floorball.iOS
{
	public partial class InitViewController : UIViewController
	{
		public InitViewController() : base("InitViewController", null)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			View.BackgroundColor = UIColor.Orange;

			//BTProgressHUD.Show("Application initialization");

			AppDelegate.SharedAppDelegate.InitAppAsync(NSUserDefaults.StandardUserDefaults);

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

