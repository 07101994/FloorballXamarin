using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball;
using FloorballAdminiOS.UI.Entity.TableViewCells;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity
{
    public abstract partial class EntityViewController : UITableViewController
    {

        public List<EntityTableViewModel> Model { get; set; }

        public UpdateType Crud { get; set; }

        public int SelectedRow { get; set; }

        public EntityViewController(string nibName, NSBundle bundle) : base(nibName, bundle)
        {
            SelectedRow = -1;
        }

		public EntityViewController(IntPtr handle) : base(handle)
        {
            SelectedRow = -1;
		}

        protected abstract void Save();

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            AddTableViewHeader();

            AddSaveButton();
        }

        private void AddSaveButton()
        {
            var button = new UIBarButtonItem();
            button.Style = UIBarButtonItemStyle.Plain;
            button.Title = "Save";

            button.Clicked += SaveClicked;

            NavigationItem.RightBarButtonItem = button;
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            AppDelegate.SharedAppDelegate.ShowConfirmationMessage(this,"Confirm", "Are you sure to save changes?",SavedHandler);
        }

        private void SavedHandler(UIAlertAction obj)
        {
            Save();
        }

        private void AddTableViewHeader()
        {
			TableView.TableFooterView = new UIView(CGRect.Empty);

			var headerView = new UIView(new CGRect(0, 0, TableView.Frame.Size.Width, 44));

			var header = new UILabel(new CGRect(0, 0, TableView.Frame.Size.Width, 44));
			header.Text = "Add League";
			header.TextAlignment = UITextAlignment.Center;

			headerView.AddSubview(header);

			TableView.TableHeaderView = headerView;

		}

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 0;
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            var model = Model.ElementAt(indexPath.Row);

            return model.CellType == TableViewCellType.DatePicker || model.CellType == TableViewCellType.Picker ? (model.IsVisible ? 216.0f : 0.0f) : TableView.RowHeight;
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var model = Model.ElementAt(indexPath.Row);

            switch (model.CellType)
            {
                case TableViewCellType.TextField:

                    return GetTextFieldCell(tableView, model);
                
                case TableViewCellType.SegmenControl:

                    return GetSegmentControlCell(tableView, model);

                case TableViewCellType.Label:

                    return GetLabelCell(tableView, model);

                case TableViewCellType.Picker:

                    return GetPickerCell(tableView, model);

                case TableViewCellType.DatePicker:

                    return GetDatePickerCell(tableView, model);

                default:

                    throw new Exception("Unsupported cell type");

            }

        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var previouslySelectedRow = SelectedRow;
			SelectedRow = indexPath.Row;

            var model = Model.ElementAt(indexPath.Row + 1);

            if (model.CellType == TableViewCellType.Picker)
            {

                var selectedCell = tableView.CellAt(NSIndexPath.FromRowSection(indexPath.Row + 1, 0)) as EntityPickerViewCell;

                model.IsVisible = !model.IsVisible;

                ChangePickerVisibility(selectedCell.PickerView, model.IsVisible);

                ChangePreviousVisibility(tableView, previouslySelectedRow);

            } else if (model.CellType == TableViewCellType.DatePicker) {

				var selectedCell = tableView.CellAt(NSIndexPath.FromRowSection(indexPath.Row + 1, 0)) as EntityDatePickerCell;

				model.IsVisible = !model.IsVisible;

				ChangePickerVisibility(selectedCell.DatePicker, model.IsVisible);

				ChangePreviousVisibility(tableView, previouslySelectedRow);

            }

			TableView.DeselectRow(indexPath, true);

        }

        private void ChangePreviousVisibility(UITableView tableView, int previouslySelectedRow)
        {
			if (previouslySelectedRow != -1)
			{

				var previousModel = Model.ElementAt(previouslySelectedRow);

				previousModel.IsVisible = false;

				if (previousModel.CellType == TableViewCellType.Picker)
				{
					var cell = tableView.CellAt(NSIndexPath.FromRowSection(previouslySelectedRow + 1, 0)) as EntityPickerViewCell;

					ChangePickerVisibility(cell.PickerView, previousModel.IsVisible);

				}
				else if (previousModel.CellType == TableViewCellType.Picker)
				{
					var cell = tableView.CellAt(NSIndexPath.FromRowSection(previouslySelectedRow + 1, 0)) as EntityDatePickerCell;

					ChangePickerVisibility(cell.DatePicker, previousModel.IsVisible);
				}

			}
        }

        void ChangePickerVisibility(UIView view, bool isVisible)
        {

			TableView.BeginUpdates();
			TableView.EndUpdates();
			if (isVisible)
			{
				view.Alpha = 0.0f;
			}
			UIView.Animate(0.25, () => { view.Alpha = isVisible ? 1.0f : 0.0f; }, () => { view.Hidden = !isVisible; });
        }

        private UITableViewCell GetDatePickerCell(UITableView tableView, EntityTableViewModel model)
        {
            var cell = tableView.DequeueReusableCell("DatePickerCell") as EntityDatePickerCell;

			return cell;
        }

        private UITableViewCell GetPickerCell(UITableView tableView, EntityTableViewModel model)
        {
            var cell = tableView.DequeueReusableCell("PickerViewCell") as EntityPickerViewCell;

            cell.PickerView.Model = model.Model as UIFloorballPickerViewModel;

			return cell;
        }

        private UITableViewCell GetLabelCell(UITableView tableView, EntityTableViewModel model)
        {
			var cell = tableView.DequeueReusableCell("LabelCell") as EntityLabelCell;

            cell.Label.Text = model.Label;
            cell.LabelChoosed.Text = model.Model as string;

			return cell;
        }

        private UITableViewCell GetSegmentControlCell(UITableView tableView, EntityTableViewModel model)
        {
			var cell = tableView.DequeueReusableCell("SegmenControlCell") as EntitySegmentControlCell;

            cell.Label.Text = model.Label;
            cell.SegmentControl.SelectedSegment = Convert.ToInt32(model.Model);
                
			return cell;
        }

        private UITableViewCell GetTextFieldCell(UITableView tableView, EntityTableViewModel model)
        {

            var cell = tableView.DequeueReusableCell("TextFieldCell") as EntityTextFieldCell;

            cell.Label.Text = model.Label;
            cell.TextField.Text = model.Model as string;

            return cell;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

		public void ShowConfirmationMessage(string title, string message, Action<UIAlertAction> handler)
		{
			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, handler));
			alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, null));
			PresentViewController(alert, true, null);
		}
    }
}

