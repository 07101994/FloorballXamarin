using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    public partial class EntityTextFieldCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EntityTextFieldCell");
        public static readonly UINib Nib;

        static EntityTextFieldCell()
        {
            Nib = UINib.FromName("EntityTextFieldCell", NSBundle.MainBundle);
        }

        protected EntityTextFieldCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
