using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    public partial class EntityLabelCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EntityLabelCell");
        public static readonly UINib Nib;

        static EntityLabelCell()
        {
            Nib = UINib.FromName("EntityLabelCell", NSBundle.MainBundle);
        }

        protected EntityLabelCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
