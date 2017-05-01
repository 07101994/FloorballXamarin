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
	[Register ("MatchEventTabViewController")]
	partial class MatchEventTabViewController
	{
		[Outlet]
		UIKit.UIImageView AwayImage { get; set; }

		[Outlet]
		UIKit.UILabel AwayScore { get; set; }

		[Outlet]
		UIKit.UILabel AwayTeamName { get; set; }

		[Outlet]
		UIKit.UIImageView HomeImage { get; set; }

		[Outlet]
		UIKit.UILabel HomeTeamName { get; set; }

		[Outlet]
		UIKit.UILabel HomsScore { get; set; }

		[Outlet]
		UIKit.UILabel Time { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (HomeImage != null) {
				HomeImage.Dispose ();
				HomeImage = null;
			}

			if (AwayImage != null) {
				AwayImage.Dispose ();
				AwayImage = null;
			}

			if (HomsScore != null) {
				HomsScore.Dispose ();
				HomsScore = null;
			}

			if (AwayScore != null) {
				AwayScore.Dispose ();
				AwayScore = null;
			}

			if (Time != null) {
				Time.Dispose ();
				Time = null;
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
