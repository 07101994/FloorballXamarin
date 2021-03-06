﻿using System;
using CoreGraphics;
using UIKit;

namespace Floorball.iOS
{
	public partial class MenuViewController : UITableViewController
	{

		public RootViewController Root { get; set; }


		public MenuViewController() : base("MenuViewController", null)
		{
		}


		public MenuViewController(IntPtr handle) : base (handle)
        {
		}



		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//TableView.TableHeaderView = TableView.DequeueReusableCell("MenuHeader");
			TableView.TableFooterView = new UIView(CGRect.Empty);

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		partial void BackButtonPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
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

				case 1:

					newContent = Storyboard.InstantiateViewController("ActualNav") as UINavigationController;
					(newContent.ViewControllers[0] as ActualViewController).Root = Root;
					(newContent.ViewControllers[0] as ActualViewController).InitProperties();

					break;
				
				case 2:

					newContent = Storyboard.InstantiateViewController("LeaguesNav") as UINavigationController;
					(newContent.ViewControllers[0] as YearsViewController).Root = Root;
					//(newContent.ViewControllers[0] as YearsViewController).NavTitle = "League Seasons";

					break;
	
				case 3:

					newContent = Storyboard.InstantiateViewController("TeamsNav") as UINavigationController;
					(newContent.ViewControllers[0] as YearsViewController).Root = Root;
					(newContent.ViewControllers[0] as YearsViewController).NavTitle = "Team Seasons";

					break;

				case 4:

					newContent = Storyboard.InstantiateViewController("PlayersNav") as UINavigationController;
					(newContent.ViewControllers[0] as PlayersContainerViewController).Root = Root;

					break;


				case 5:

					newContent = Storyboard.InstantiateViewController("RefereesNav") as UINavigationController;
					(newContent.ViewControllers[0] as RefereesContainerViewController).Root = Root;

					break;

				case 6:

					newContent = Storyboard.InstantiateViewController("SettingsNav") as UINavigationController;
					(newContent.ViewControllers[0] as MainSettingsViewController).Root = Root;

					break;

				default:
					return;
			}



			Root.SideBarController.ChangeContentView(newContent);

		}
	}
}

