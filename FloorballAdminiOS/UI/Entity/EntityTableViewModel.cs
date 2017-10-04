using System;
using Floorball;

namespace FloorballAdminiOS.UI.Entity
{
    public enum TableViewCellType {
        TextField, SegmenControl, Label, Picker, DatePicker
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

        public string SegmentModelSelectedValue
        {
            get
            {
                var segmentControl = Value as SegmentControlModel;

                return segmentControl.Segments[segmentControl.Selected].Item1;
            }
        }

        public DateTime PickerValueAsDateTime => Convert.ToDateTime(PickerModelSelectedObjet);

        public TimeSpan PickerValueAsTimeSpan => TimeSpan.Parse(PickerModelSelectedObjet.ToString());

        public string PickerValueAsString => PickerModelSelectedObjet.ToString();

        public CountriesEnum PickerValueAsCountriesEnum => PickerModelSelectedObjet.ToString().ToEnum<CountriesEnum>();

        public int PickerValueAsInt => Convert.ToInt32(PickerModelSelectedObjet);

        public short PickerValueAsShort => Convert.ToInt16(PickerModelSelectedObjet);

    }
}
