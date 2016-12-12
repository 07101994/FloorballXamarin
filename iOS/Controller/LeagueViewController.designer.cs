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
	[Register ("LeagueViewController")]
	partial class LeagueViewController
	{
		[Outlet]
		UIKit.UIView MatchContainer { get; set; }

		[Outlet]
		UIKit.UIView StatContainer { get; set; }

		[Outlet]
		UIKit.UIView TableContainer { get; set; }

		[Action ("SegmentChanged:")]
		partial void SegmentChanged (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (StatContainer != null) {
				StatContainer.Dispose ();
				StatContainer = null;
			}

			if (TableContainer != null) {
				TableContainer.Dispose ();
				TableContainer = null;
			}

			if (MatchContainer != null) {
				MatchContainer.Dispose ();
				MatchContainer = null;
			}
		}
	}
}
