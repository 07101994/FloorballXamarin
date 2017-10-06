using System;
using System.Collections.Generic;
using System.Linq;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using FloorballAdminiOS.UI.EntitySearch;
using UIKit;

namespace FloorballAdminiOS.UI.EntityChoose
{
    public partial class EntityChooserViewController : UITableViewController, EntityChooserScreen
    {
        public RootViewController Root { get; set; }

        public EntityChooserPresenter EntityChooserPresenter { get; set; }

        UpdateType crud;

        public UpdateType Crud
        {
            get
            {
                return crud;
            }
            set
            {
                crud = value;
                switch (crud)
                {
                    case UpdateType.Create:
                        Opertaion = "Add ";
                        break;
                    case UpdateType.Update:
                        Opertaion = "Update ";
                        break;
                    default:
                        break;
                }
            }
        }

        private string Opertaion { get; set; }

        public EntityChooserViewController() : base("EntityChooserViewController", null)
        {
        }

		public EntityChooserViewController(IntPtr handle) : base(handle)
        {
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            EntityChooserPresenter = new EntityChooserPresenter();
            NavigationItem.Title = "Choose action";

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			EntityChooserPresenter.AttachScreen(this);

        }

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			EntityChooserPresenter.DetachScreen();
		}

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return EntityChooserPresenter.Entitites.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("EntityCell", indexPath);

            cell.TextLabel.Text = Opertaion + EntityChooserPresenter.Entitites.ElementAt(indexPath.Row).Item1.ToString();

			return cell;

		}

        public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            switch (Crud)
            {
                case UpdateType.Create:

                    ShowEntityVC(EntityChooserPresenter.Entitites.ElementAt(indexPath.Row).Item2);

                    break;

				case UpdateType.Update:

                    ShowSearchEntityVC(EntityChooserPresenter.Entitites.ElementAt(indexPath.Row).Item3);

					break;

                default:
                    break;
            }


        
        }

        private void ShowSearchEntityVC(EntitySearchPresenter<EntitySearchScreen> presenter)
        {
            var controller = Storyboard.InstantiateViewController("EntitySearchViewController") as EntitySearchViewController;
            controller.Presenter = presenter;
            NavigationController.PushViewController(controller, true);
        }

        private void ShowEntityVC(EntityPresenter<EntityScreen> presenter)
        {
			var controller = Storyboard.InstantiateViewController("EntityViewController") as EntityViewController;
            controller.Crud = Crud;
            controller.EntityPresenter = presenter;
			NavigationController.PushViewController(controller, true);
        }

        partial void MenuPressed(UIBarButtonItem sender)
        {
            Root.SideBarController.ToggleMenu();
        }
    }
}

