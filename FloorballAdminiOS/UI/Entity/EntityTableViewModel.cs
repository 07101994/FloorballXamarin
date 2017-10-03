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

        public object Model { get; set; }

        public bool IsVisible { get; set; }

        public int ModelAsInt 
        {
            get
            {
                return Convert.ToInt32(Model);
            }
        }

        public string ModelAsString
        {
            get
            {
                return Model.ToString();   
            }
        }

        public CountriesEnum ModelAsCountriesEnum
        {
            get
            {
                return Model.ToString().ToEnum<CountriesEnum>();
            }
        }

        public DateTime ModelAsDateTime
        {
            get
            {
                return DateTime.Parse(Model.ToString());
            }
        }

    }
}
