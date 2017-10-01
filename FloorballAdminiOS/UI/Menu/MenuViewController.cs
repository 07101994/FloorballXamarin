using System;
using Floorball;
using FloorballAdminiOS.UI.EntityChoose;
using FloorballAdminiOS.UI.Matches;
using UIKit;

namespace FloorballAdminiOS.UI.Menu
{
    public partial class MenuViewController : UITableViewController
    {
        public RootViewController Root { get; set; }

        public MenuViewController() : base("MenuViewController", null)
        {
        }

		public MenuViewController(IntPtr handle) : base(handle)
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

					newContent = Storyboard.InstantiateViewController("MatchesNav") as UINavigationController;
					(newContent.ViewControllers[0] as MatchesTableViewController).Root = Root;
					//(newContent.ViewControllers[0] as MatchesTableViewController).InitProperties();

					break;

				case 2:

					newContent = Storyboard.InstantiateViewController("EntityChooserNav") as UINavigationController;
					(newContent.ViewControllers[0] as EntityChooserViewController).Root = Root;
					(newContent.ViewControllers[0] as EntityChooserViewController).Crud = UpdateType.Create;


					break;

				case 3:

					newContent = Storyboard.InstantiateViewController("EntityChooserNav") as UINavigationController;
					(newContent.ViewControllers[0] as EntityChooserViewController).Root = Root;
					(newContent.ViewControllers[0] as EntityChooserViewController).Crud = UpdateType.Update;

					break;

				default:
					return;
			}



			Root.SideBarController.ChangeContentView(newContent);

		}
    }
}

