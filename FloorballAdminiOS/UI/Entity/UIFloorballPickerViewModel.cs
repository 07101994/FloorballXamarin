using System;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace FloorballAdminiOS.UI.Entity
{
    public delegate void SelectionChangedEventHandler(string val);

    public class UIFloorballPickerViewModel : UIPickerViewModel
    {
        List<string> pickerModel;
        List<object> selectedValues;

        public object SelectedValue { get; set; }

        public event SelectionChangedEventHandler SelectionChanged;

        public UIFloorballPickerViewModel()
        {
        }

		public UIFloorballPickerViewModel(List<string> pickerModel, List<object> selectedValues = null)
		{
            this.pickerModel = pickerModel;
            this.selectedValues = selectedValues;
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
