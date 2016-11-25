using System;

using UIKit;

namespace Floorball.iOS
{
	public partial class MenuViewController : UITableViewController
	{

		public RootViewController Root
		{
			get;
			set;
		}


		public MenuViewController() : base("MenuViewController", null)
		{
		}


		public MenuViewController(IntPtr handle) : base (handle)
        {
		}



		public override void ViewDidLoad()
		{
			base.ViewDidLoad();


		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		partial void BackButtonPressed(UIBarButtonItem sender)
		{

		}

		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			//base.RowSelected(tableView, indexPath);


			ChangeContentView(indexPath.Row);

		}

		void ChangeContentView(int row)
		{

			UINavigationController newContent;

			switch (row)
			{

				case 0:

					newContent = Storyboard.InstantiateViewController("ActualNav") as UINavigationController;
					(newContent.ViewControllers[0] as ActualViewController).Root = Root;

					break;
				
				case 1:

					newContent = Storyboard.InstantiateViewController("LeaguesNav") as UINavigationController;
					(newContent.ViewControllers[0] as YearsViewController).Root = Root;

					break;
	
				case 2:

					newContent = Storyboard.InstantiateViewController("TeamsNav") as UINavigationController;
					(newContent.ViewControllers[0] as TeamsViewController).Root = Root;

					break;

				case 3:

					newContent = Storyboard.InstantiateViewController("PlayersNav") as UINavigationController;
					(newContent.ViewControllers[0] as PlayersViewController).Root = Root;

					break;


				case 4:

					newContent = Storyboard.InstantiateViewController("RefereesNav") as UINavigationController;
					(newContent.ViewControllers[0] as RefereesViewController).Root = Root;

					break;

				default:
					return;
			}



			Root.SideBarController.ChangeContentView(newContent);

		}
	}
}

