using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreGraphics;
using Floorball;
using Floorball.Exceptions;
using FloorballAdminiOS.Helper;
using FloorballAdminiOS.UI.Entity.TableViewCells;
using FloorballPCL.Exceptions;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity
{
    public partial class EntityViewController : UITableViewController, EntityScreen
    {

        public EntityPresenter<EntityScreen> EntityPresenter { get; set; }

        public UpdateType Crud { get; set; }

        public int SelectedRow { get; set; }

        public EntityViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }

		public EntityViewController(IntPtr handle) : base(handle) { }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            SelectedRow = -1;

            EntityPresenter.AttachScreen(this);

            AddTableViewHeader(EntityPresenter.GetTableHeader(Crud));
            AddSaveButton();

            try
            {
				await EntityPresenter.SetDataFromServer(Crud);

				TableView.ReloadData();
            }
            catch (Exception ex)
            {
                AppDelegate.SharedAppDelegate.ShowErrorMessage(this,ex.Message);
            }

        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            EntityPresenter.ClearModel();
        }

        private void AddSaveButton()
        {
            var button = new UIBarButtonItem();
            button.Style = UIBarButtonItemStyle.Plain;
            button.Title = Crud == UpdateType.Create ? "Create" : "Update";

            button.Clicked += SaveClicked;

            NavigationItem.RightBarButtonItem = button;
        }

        void SaveClicked(object sender, EventArgs e)
        {
            AppDelegate.SharedAppDelegate.ShowConfirmationMessage(this,"Confirm saving", "Are you sure to save changes?",SavedHandler);
        }

        async void SavedHandler(UIAlertAction obj)
        {
            NavigationItem.RightBarButtonItem.Enabled = false;
            try
            {
                await EntityPresenter.ValidateAndSave(Crud);
            }
            catch (CommunicationException)
            {
                AppDelegate.SharedAppDelegate.ShowErrorMessage(this, "Hiba történt a mentés során!");
            } 
            catch (ValidationException ex)
            {
				AppDelegate.SharedAppDelegate.ShowErrorMessage(this, ex.Message, ex.Title);
            }
            finally
            {
                NavigationItem.RightBarButtonItem.Enabled = true;
            }
        }

        protected void AddTableViewHeader(string headerText)
        {
			TableView.TableFooterView = new UIView(CGRect.Empty);

			var headerView = new UIView(new CGRect(0, 0, TableView.Frame.Size.Width, 44));

			var header = new UILabel(new CGRect(0, 0, TableView.Frame.Size.Width, 44));
			header.Text = headerText;
			header.TextAlignment = UITextAlignment.Center;

			headerView.AddSubview(header);

			TableView.TableHeaderView = headerView;

		}

        public override nint NumberOfSections(UITableView tableView)
        {
            return EntityPresenter.Model.Count();
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return section == 0 ? EntityPresenter.TextManager.GetText("entityMainSection") : EntityPresenter.TextManager.GetText("entitySubSection");
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return EntityPresenter.Model[Convert.ToInt32(section)].Count();
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{

            if (indexPath.Section == 0) 
            {
                var model = EntityPresenter.Model[0][indexPath.Row];

				return model.CellType == TableViewCellType.DatePicker || model.CellType == TableViewCellType.Picker || model.CellType == TableViewCellType.TimePicker || model.CellType == TableViewCellType.DateAndTimePicker ? (model.IsVisible ? 216.0f : 0.0f) : TableView.RowHeight;
			}

            return base.GetHeightForRow(tableView, indexPath);
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            if (indexPath.Section == 0)
            {
                var model = EntityPresenter.Model[0][indexPath.Row];

                switch (model.CellType)
                {
                    case TableViewCellType.TextField:

                        return SetNonSelectable(GetTextFieldCell(tableView, model));

                    case TableViewCellType.SegmenControl:

                        return SetNonSelectable(GetSegmentControlCell(tableView, model));

                    case TableViewCellType.Label:

                        return GetLabelCell(tableView, model);

                    case TableViewCellType.Picker:

                        return SetNonSelectable(GetPickerCell(tableView, model));

                    case TableViewCellType.DatePicker:

                        return SetNonSelectable(GetDatePickerCell(tableView, model));

                    case TableViewCellType.TimePicker:

                        return SetNonSelectable(GetTimePickerCell(tableView, model));

                    case TableViewCellType.DateAndTimePicker:

                        return SetNonSelectable(GetDateAndTimePickerCell(tableView, model));

                    default:

                        throw new Exception("Unsupported cell type");

                }

            }
            else 
            {
                var cell = tableView.DequeueReusableCell("NavigationCell");

                cell.TextLabel.Text = EntityPresenter.Model[1][indexPath.Row].Label;

                return cell;
            }


        }

        UITableViewCell SetNonSelectable(UITableViewCell cell)
        {
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {

            if (indexPath.Section == 0) 
            {
				try
				{
					var model = EntityPresenter.Model[0][indexPath.Row + 1];

					var previouslySelectedRow = SelectedRow;
					SelectedRow = indexPath.Row;

					if (model.CellType == TableViewCellType.Picker)
					{

						var selectedCell = tableView.CellAt(NSIndexPath.FromRowSection(indexPath.Row + 1, 0)) as EntityPickerViewCell;

						if (!ChangePreviousVisibility(tableView, previouslySelectedRow, selectedCell.PickerView, model))
						{

							model.IsVisible = !model.IsVisible;

							ChangePickerVisibility(selectedCell.PickerView, model.IsVisible);

						};


					}
					else if (model.CellType == TableViewCellType.DatePicker || model.CellType == TableViewCellType.TimePicker || model.CellType == TableViewCellType.DateAndTimePicker)
					{

						var selectedCell = tableView.CellAt(NSIndexPath.FromRowSection(indexPath.Row + 1, 0)) as EntityDatePickerCell;

						if (!ChangePreviousVisibility(tableView, previouslySelectedRow, selectedCell.DatePicker, model))
						{

							model.IsVisible = !model.IsVisible;

							ChangePickerVisibility(selectedCell.DatePicker, model.IsVisible);

						}

					}
				}
				catch (Exception)
				{
				}

				TableView.DeselectRow(indexPath, true);
            }



        }

        bool ChangePreviousVisibility(UITableView tableView, int previouslySelectedRow, UIView selectedCell, EntityTableViewModel selectedModel)
        {
			if (previouslySelectedRow != -1 && previouslySelectedRow != SelectedRow)
			{
                previouslySelectedRow++;

				var previousModel = EntityPresenter.Model[0][previouslySelectedRow];

				previousModel.IsVisible = false;

				if (previousModel.CellType == TableViewCellType.Picker)
				{
					var cell = tableView.CellAt(NSIndexPath.FromRowSection(previouslySelectedRow, 0)) as EntityPickerViewCell;

					ChangePickerVisibility(cell.PickerView, previousModel.IsVisible, selectedCell, selectedModel);

                    return true;
				}
				else if (previousModel.CellType == TableViewCellType.DatePicker || previousModel.CellType == TableViewCellType.TimePicker || previousModel.CellType == TableViewCellType.DateAndTimePicker)
				{
					var cell = tableView.CellAt(NSIndexPath.FromRowSection(previouslySelectedRow, 0)) as EntityDatePickerCell;

					ChangePickerVisibility(cell.DatePicker, previousModel.IsVisible, selectedCell, selectedModel);

                    return true;
				}
			}

            return false;
        }

        void ChangePickerVisibility(UIView view, bool isVisible, UIView view2 = null, EntityTableViewModel model = null)
        {

			TableView.BeginUpdates();
			TableView.EndUpdates();
			if (isVisible)
			{
				view.Alpha = 0.0f;
			}
            UIView.Animate(0.25, () => { view.Alpha = (isVisible ? 1.0f : 0.0f); }, () => 
            {
                view.Hidden = !isVisible;

                if (view2 != null) 
                {
                    model.IsVisible = !model.IsVisible;

                    ChangePickerVisibility(view2,model.IsVisible);    
                } 

            });
        }

		private UITableViewCell GetDateAndTimePickerCell(UITableView tableView, EntityTableViewModel model)
		{
			var cell = tableView.DequeueReusableCell("DatePickerCell") as EntityDatePickerCell;

			cell.DatePicker.Hidden = true;
			cell.DatePicker.TranslatesAutoresizingMaskIntoConstraints = false;

			cell.DatePicker.Mode = UIDatePickerMode.DateAndTime;

			cell.DatePicker.ValueChanged += (sender, e) =>
			{
				EntityPresenter.Model[0][SelectedRow].Value = (sender as UIDatePicker).Date.ToString();

				TableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(SelectedRow, 0) }, UITableViewRowAnimation.None);
			};

			return cell;
		}

		private UITableViewCell GetTimePickerCell(UITableView tableView, EntityTableViewModel model)
		{
			var cell = tableView.DequeueReusableCell("DatePickerCell") as EntityDatePickerCell;

			cell.DatePicker.Hidden = true;
			cell.DatePicker.TranslatesAutoresizingMaskIntoConstraints = false;

            cell.DatePicker.Mode = UIDatePickerMode.Time;

			cell.DatePicker.ValueChanged += (sender, e) =>
			{
                EntityPresenter.Model[0][SelectedRow].Value = iOSHelper.NSDateToDateTime((sender as UIDatePicker).Date).TimeOfDay.ToString(@"hh\-mm");

				TableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(SelectedRow, 0) }, UITableViewRowAnimation.None);
			};

			return cell;
		}

        private UITableViewCell GetDatePickerCell(UITableView tableView, EntityTableViewModel model)
        {
            var cell = tableView.DequeueReusableCell("DatePickerCell") as EntityDatePickerCell;

			cell.DatePicker.Hidden = true;
			cell.DatePicker.TranslatesAutoresizingMaskIntoConstraints = false;

            cell.DatePicker.ValueChanged += (sender, e) => 
            {
                EntityPresenter.Model[0][SelectedRow].Value = iOSHelper.NSDateToDateTime((sender as UIDatePicker).Date).Date.ToString("yyyy-MM-dd");
				
                TableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(SelectedRow, 0) }, UITableViewRowAnimation.None);
			};

			return cell;
        }

        private UITableViewCell GetPickerCell(UITableView tableView, EntityTableViewModel model)
        {
            var cell = tableView.DequeueReusableCell("PickerViewCell") as EntityPickerViewCell;

            var pickerModel = model.Value as UIFloorballPickerViewModel;

            pickerModel.SelectionChanged += PickerModel_SelectionChanged;

            cell.PickerView.Model = pickerModel;
            cell.PickerView.Hidden = true;
			cell.PickerView.TranslatesAutoresizingMaskIntoConstraints = false;


			return cell;
        }

        private UITableViewCell GetLabelCell(UITableView tableView, EntityTableViewModel model)
        {
			var cell = tableView.DequeueReusableCell("LabelCell") as EntityLabelCell;

            cell.Label.Text = model.Label;
            cell.LabelChoosed.Text = model.Value as string;

			return cell;
        }

        private UITableViewCell GetSegmentControlCell(UITableView tableView, EntityTableViewModel model)
        {
			var cell = tableView.DequeueReusableCell("SegmentControlCell") as EntitySegmentControlCell;

            cell.Label.Text = model.Label;

            var segmentModel = model.Value as SegmentControlModel;

            cell.SegmentControl.RemoveAllSegments();

            int i = 0;

            foreach (var segment in segmentModel.Model)
            {
                cell.SegmentControl.InsertSegment(segment,i++,false);
            }

            cell.SegmentControl.SelectedSegment = segmentModel.Selected;
            cell.SegmentControl.ValueChanged += (sender, e) =>
            {
                segmentModel.Selected = Convert.ToInt32(((UISegmentedControl)sender).SelectedSegment); 
            };


			return cell;
        }

        private UITableViewCell GetTextFieldCell(UITableView tableView, EntityTableViewModel model)
        {

            var cell = tableView.DequeueReusableCell("TextFieldCell") as EntityTextFieldCell;

            cell.Label.Text = model.Label;
            cell.TextField.Text = model.Value as string;


            cell.TextField.AddTarget((sender, e) => 
            { 
                model.Value = ((UITextField)sender).Text; 
            }, UIControlEvent.EditingChanged);

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

        void PickerModel_SelectionChanged(string val)
        {
            EntityPresenter.Model[0][SelectedRow].Value = val;

            TableView.ReloadRows(new NSIndexPath[] { NSIndexPath.FromRowSection(SelectedRow, 0) }, UITableViewRowAnimation.None);

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "NavigationSegue")
            {
                
            }
        }

    }
}

