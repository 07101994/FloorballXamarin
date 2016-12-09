// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Floorball.iOS
{
	[Register ("TeamViewController")]
	partial class TeamViewController
	{
		[Outlet]
		UIKit.UILabel CoachName { get; set; }

		[Outlet]
		UIKit.UIView MatchesContainer { get; set; }

		[Outlet]
		UIKit.UIView PlayersContainer { get; set; }

		[Outlet]
		UIKit.UILabel StadiumName { get; set; }

		[Outlet]
		UIKit.UIImageView TeamImage { get; set; }

		[Outlet]
		UIKit.UILabel TeamName { get; set; }

		[Action ("SegmentControlChanged:")]
		partial void SegmentControlChanged (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (CoachName != null) {
				CoachName.Dispose ();
				CoachName = null;
			}

			if (StadiumName != null) {
				StadiumName.Dispose ();
				StadiumName = null;
			}

			if (TeamImage != null) {
				TeamImage.Dispose ();
				TeamImage = null;
			}

			if (TeamName != null) {
				TeamName.Dispose ();
				TeamName = null;
			}

			if (PlayersContainer != null) {
				PlayersContainer.Dispose ();
				PlayersContainer = null;
			}

			if (MatchesContainer != null) {
				MatchesContainer.Dispose ();
				MatchesContainer = null;
			}
		}
	}
}
