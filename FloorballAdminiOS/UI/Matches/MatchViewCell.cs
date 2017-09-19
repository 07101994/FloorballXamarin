using System;

using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.Matches
{
    public partial class MatchViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MatchViewCell");
        public static readonly UINib Nib;

        static MatchViewCell()
        {
            Nib = UINib.FromName("MatchViewCell", NSBundle.MainBundle);
        }

        protected MatchViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
