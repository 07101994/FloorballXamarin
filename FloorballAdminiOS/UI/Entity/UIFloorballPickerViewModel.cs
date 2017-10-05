using System;
using System.Collections.Generic;
using System.Linq;
using Floorball;
using UIKit;

namespace FloorballAdminiOS.UI.Entity
{
    public delegate void SelectionChangedEventHandler(string val);

    public class UIFloorballPickerViewModel : UIPickerViewModel
    {
        List<string> pickerModel;
        List<object> selectedValues = new List<object>();

        public object SelectedValue { get; set; }

        public event SelectionChangedEventHandler SelectionChanged;

        public UIFloorballPickerViewModel()
        {
        }

		public UIFloorballPickerViewModel(IEnumerable<string> pickerModel, IEnumerable<string> selectedValues = null)
		{
            this.pickerModel = pickerModel.ToList();

            foreach (var v in selectedValues)
            {
                this.selectedValues.Add(v);
            }
		}

		public UIFloorballPickerViewModel(IEnumerable<string> pickerModel, IEnumerable<int> selectedValues = null)
		{
			this.pickerModel = pickerModel.ToList();

			foreach (var v in selectedValues)
			{
				this.selectedValues.Add(v);
			}
		}

		public UIFloorballPickerViewModel(IEnumerable<string> pickerModel, IEnumerable<CountriesEnum> selectedValues = null)
		{
			this.pickerModel = pickerModel.ToList();

			foreach (var v in selectedValues)
			{
				this.selectedValues.Add(v);
			}
		}

		public UIFloorballPickerViewModel(IEnumerable<string> pickerModel, IEnumerable<LeagueTypeEnum> selectedValues = null)
		{
			this.pickerModel = pickerModel.ToList();

			foreach (var v in selectedValues)
			{
				this.selectedValues.Add(v);
			}
		}

		public UIFloorballPickerViewModel(IEnumerable<string> pickerModel, IEnumerable<ClassEnum> selectedValues = null)
		{
			this.pickerModel = pickerModel.ToList();

			foreach (var v in selectedValues)
			{
				this.selectedValues.Add(v);
			}
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return pickerModel.Count;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return pickerModel.ElementAt(Convert.ToInt32(row));
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }
		
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            int selectedRow = Convert.ToInt32(row);

            SelectedValue = selectedValues[selectedRow];
            SelectionChanged?.Invoke(pickerModel.ElementAt(selectedRow));

        }
    }
}
