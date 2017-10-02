using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    public partial class EntityPickerViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EntityPickerViewCell");
        public static readonly UINib Nib;

        static EntityPickerViewCell()
        {
            Nib = UINib.FromName("EntityPickerViewCell", NSBundle.MainBundle);
        }

        protected EntityPickerViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
