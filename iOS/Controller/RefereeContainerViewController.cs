using System;
using System.Collections.Generic;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class RefereeContainerViewController : UIViewController
	{

		public List<RefereeStatModel> StatsByLeague { get; set; }

		public IEnumerable<League> Leagues { get; set; }

		public Referee Referee { get; set; }

		public RefereeContainerViewController() : base("RefereeContainerViewController", null)
		{
		}

		public RefereeContainerViewController(IntPtr handle) : base(handle)
		{ 
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			RefereeName.Text = Referee.Name;

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			if (segue.Identifier == "StatSegue")
			{

				var vc = segue.DestinationViewController as RefereeViewController;

				vc.Referee = Referee;
				vc.Leagues = Leagues;
				vc.StatsByLeague = StatsByLeague;
			
			}
		}
	}
}

