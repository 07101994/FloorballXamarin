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
	[Register ("MatchPlayersViewController")]
	partial class MatchPlayersViewController
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
		UIKit.UILabel HomeScore { get; set; }

		[Outlet]
		UIKit.UILabel HomeTeamName { get; set; }
		
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

			if (HomeScore != null) {
				HomeScore.Dispose ();
				HomeScore = null;
			}

			if (AwayScore != null) {
				AwayScore.Dispose ();
				AwayScore = null;
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
