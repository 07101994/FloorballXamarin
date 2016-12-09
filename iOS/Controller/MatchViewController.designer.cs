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
	[Register ("MatchViewController")]
	partial class MatchViewController
	{
		[Outlet]
		UIKit.UILabel ActualTime { get; set; }

		[Outlet]
		UIKit.UIImageView AwayTeamImage { get; set; }

		[Outlet]
		UIKit.UILabel AwayTeamName { get; set; }

		[Outlet]
		UIKit.UIImageView HomeTeamImage { get; set; }

		[Outlet]
		UIKit.UILabel HomeTeamName { get; set; }

		[Outlet]
		UIKit.UILabel LeagueName { get; set; }

		[Outlet]
		UIKit.UILabel MatchDate { get; set; }

		[Outlet]
		UIKit.UILabel Result { get; set; }

		[Outlet]
		UIKit.UILabel StadiumName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LeagueName != null) {
				LeagueName.Dispose ();
				LeagueName = null;
			}

			if (MatchDate != null) {
				MatchDate.Dispose ();
				MatchDate = null;
			}

			if (StadiumName != null) {
				StadiumName.Dispose ();
				StadiumName = null;
			}

			if (Result != null) {
				Result.Dispose ();
				Result = null;
			}

			if (HomeTeamImage != null) {
				HomeTeamImage.Dispose ();
				HomeTeamImage = null;
			}

			if (AwayTeamImage != null) {
				AwayTeamImage.Dispose ();
				AwayTeamImage = null;
			}

			if (ActualTime != null) {
				ActualTime.Dispose ();
				ActualTime = null;
			}

			if (HomeTeamName != null) {
				HomeTeamName.Dispose ();
				HomeTeamName = null;
			}

			if (AwayTeamName != null) {
				AwayTeamName.Dispose ();
				AwayTeamName = null;
			}
		}
	}
}
