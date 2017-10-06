using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    public partial class EntitySegmentControlCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("EntitySegmentControlCell");
        public static readonly UINib Nib;

        static EntitySegmentControlCell()
        {
            Nib = UINib.FromName("EntitySegmentControlCell", NSBundle.MainBundle);
        }

        protected EntitySegmentControlCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
