using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    public partial class EntityDatePickerCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EntityDatePickerCell");
        public static readonly UINib Nib;

        static EntityDatePickerCell()
        {
            Nib = UINib.FromName("EntityDatePickerCell", NSBundle.MainBundle);
        }

        protected EntityDatePickerCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
