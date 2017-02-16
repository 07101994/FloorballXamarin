using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class PlayersContainerViewController : UIViewController
	{

		public RootViewController Root { get; set; }

		public IEnumerable<Player> Players { get; set; }


		public PlayersContainerViewController() : base("PlayersContainerViewController", null)
		{
		}

		public PlayersContainerViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//InitProperties();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		void InitProperties()
		{
			Players = AppDelegate.SharedAppDelegate.UoW.PlayerRepo.GetAllPlayer().OrderBy(p => p.Name).ToList();
		}

		partial void Filter(UITextField sender)
		{
			var playersContainer = ChildViewControllers[0] as PlayersViewController;

			if (sender.Text == "")
			{
				playersContainer.ActualPlayers = Players;
			}
			else
			{
				playersContainer.ActualPlayers = Players.Where(p => p.Name.Contains(sender.Text));
			}

			playersContainer.TableView.ReloadData();
		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			switch (segue.Identifier)
			{
				case "players":

					InitProperties();

					var vc = segue.DestinationViewController as PlayersViewController;
					vc.ActualPlayers = Players;

					break;

				default:
					break;
			}
		}

	}
}

