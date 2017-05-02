using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class RefereesContainerViewController : UIViewController
	{

		public RootViewController Root { get; set; }

		public IEnumerable<Referee> Referees { get; set; }
		

		public RefereesContainerViewController() : base("RefereesContainerViewController", null)
		{
		}

		public RefereesContainerViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//InitProperties();
			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");
			

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		void InitProperties()
		{
			Referees = AppDelegate.SharedAppDelegate.UoW.RefereeRepo.GetAllReferee().OrderBy(r => r.Name).ToList();
		}

		partial void Filter(UITextField sender)
		{
			var refereesContainer = ChildViewControllers[0] as RefereesViewController;

			if (sender.Text == "")
			{
				refereesContainer.Referees = Referees;
			}
			else
			{
				refereesContainer.Referees = Referees.Where(p => p.Name.ToLower().Contains(sender.Text.ToLower()));
			}

			refereesContainer.TableView.ReloadData();
		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			switch (segue.Identifier)
			{
				case "referees":

					InitProperties();

					var vc = segue.DestinationViewController as RefereesViewController;
					vc.Referees = Referees;
				
					break;

				default:
					break;
			}
		}
	}
}

