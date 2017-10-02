using System;
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

    }
}
