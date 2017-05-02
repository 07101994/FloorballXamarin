using System;

using Foundation;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchPlayerStatView : UITableViewCell
	{
		public static readonly NSString Key = new NSString("MatchPlayerStatView");
		public static readonly UINib Nib;

		static MatchPlayerStatView()
		{
			Nib = UINib.FromName("MatchPlayerStatView", NSBundle.MainBundle);
		}

		protected MatchPlayerStatView(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.

		}
	}
}
