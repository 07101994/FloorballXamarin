using System;
using System.Collections.Generic;
using Floorball;

namespace FloorballAdminiOS.UI.Entity
{
    public enum TableViewCellType {
        TextField, SegmenControl, Label, Picker, DatePicker, DateAndTimePicker, TimePicker
    }

    public class EntityTableViewModel
    {

        public TableViewCellType CellType { get; set; }

        public string Label { get; set; }

        public object Value { get; set; }

        public bool IsVisible { get; set; }

        public string ValueAsString
        {
            get
            {
                return Value.ToString();
            }
        }

        private object PickerModelSelectedObjet => (Value as UIFloorballPickerViewModel).SelectedValue;

        public object SegmentModelSelectedValue
        {
            get
            {
                var segmentControl = Value as SegmentControlModel;

                return segmentControl.SelectedValues[segmentControl.Selected];
            }
        }

        public DateTime PickerValueAsDateTime => Convert.ToDateTime(PickerModelSelectedObjet);

        public TimeSpan PickerValueAsTimeSpan => TimeSpan.Parse(PickerModelSelectedObjet.ToString());

        public string PickerValueAsString => PickerModelSelectedObjet.ToString();

        public int PickerValueAsInt => Convert.ToInt32(PickerModelSelectedObjet);

        public short PickerValueAsShort => Convert.ToInt16(PickerModelSelectedObjet);

        public T GetPickerValuesAsEnum<T>()
        {
            return PickerModelSelectedObjet.ToString().ToEnum<T>();
        }

        public List<List<NavigationModel>> ValueAsNavModels => Value as List<List<NavigationModel>>;

    }
}
