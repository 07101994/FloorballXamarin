using System;
using System.Collections.Generic;
using Floorball;
using FloorballServer.Models.Floorball;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.League
{
    public partial class LeagueViewContoller : EntityViewController
    {
        private bool yearPickerVisible;
        private bool countryPickerVisible;

        public EntityPresenter<LeagueModel> EntityPresenter { get; set; }

        public LeagueViewContoller() : base("LeagueViewContoller", null)
        {
            yearPickerVisible = false;
            countryPickerVisible = false;
        }

		public LeagueViewContoller(IntPtr handle) : base(handle)
        {
            
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            EntityPresenter = new EntityPresenter<LeagueModel>();

            InitPicker(YearChooser, new UIFloorballPickerViewModel(UIHelper.GetNumbers(2014, 2018)));
			InitPicker(CountryChooser, new UIFloorballPickerViewModel(GetCountries()));

        }

        private List<string> GetCountries()
        {
            var countries = new List<string>();

			foreach (CountriesEnum country in Enum.GetValues(typeof(CountriesEnum)))
			{
				countries.Add(NSBundle.MainBundle.LocalizedString(country.ToString().ToLower(), null));
			}

            return countries;
        }

        private void InitPicker(UIPickerView pickerView, UIFloorballPickerViewModel pickerViewModel)
        {
            pickerViewModel.SelectionChanged += PickerSelected;
			pickerView.Model = pickerViewModel;
			pickerView.Hidden = true;
			pickerView.TranslatesAutoresizingMaskIntoConstraints = false;

        }

        private void PickerSelected(string val)
        {
            //TODO            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var height = TableView.RowHeight;

            if (indexPath.Row == 2) 
            {
                height = yearPickerVisible ? 216.0f : 0.0f;
            } else if (indexPath.Row == 4)
            {
				height = countryPickerVisible ? 216.0f : 0.0f;
            }

            return height;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            SelectedRow = indexPath.Row;

			if (indexPath.Row == 1)
			{
                ChangePickerVisibility(YearChooser, !yearPickerVisible, out yearPickerVisible);
                if (countryPickerVisible) 
                {
                    ChangePickerVisibility(CountryChooser, false, out countryPickerVisible);    
                }
			} else if (indexPath.Row == 3)
            {
                ChangePickerVisibility(CountryChooser, !countryPickerVisible, out countryPickerVisible);
                if (yearPickerVisible) 
                {
                    ChangePickerVisibility(YearChooser, false, out yearPickerVisible);    
                }
            }

            TableView.DeselectRow(indexPath, true);
        }

        void ChangePickerVisibility(UIPickerView pickerView, bool isVisible, out bool pickerVisibility)
        {
            pickerVisibility = isVisible;
            TableView.BeginUpdates();
            TableView.EndUpdates();
            if (isVisible) {
                pickerView.Alpha = 0.0f;
			}
            UIView.Animate(0.25,() => { pickerView.Alpha = isVisible ? 1.0f : 0.0f; }, () => { pickerView.Hidden =  !isVisible; } );

        }

        protected override void Save()
        {
            
        }
    }
}

