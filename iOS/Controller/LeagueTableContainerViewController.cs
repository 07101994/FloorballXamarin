using System;
using System.Collections.Generic;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueTableContainerViewController : UIViewController
	{

		public IEnumerable<Team> Teams { get; set; }
		

		public LeagueTableContainerViewController() : base("LeagueTableContainerViewController", null)
		{
		}

		public LeagueTableContainerViewController(IntPtr handle) : base(handle)
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


		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			switch (segue.Identifier)
			{

				case "LeagueTable1":


					var vc1 = segue.DestinationViewController as LeagueTableViewController;
					vc1.Teams = Teams;

					break;


				default:
					break;
			}
		}

	}
}

