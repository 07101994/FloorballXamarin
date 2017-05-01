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
	[Register ("MatchDetailsViewController")]
	partial class MatchDetailsViewController
	{
		[Outlet]
		UIKit.UIImageView Country { get; set; }

		[Outlet]
		UIKit.UILabel Date { get; set; }

		[Outlet]
		UIKit.UILabel LeagueName { get; set; }

		[Outlet]
		UIKit.UILabel StadiumAddress { get; set; }

		[Outlet]
		UIKit.UILabel StadiumName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LeagueName != null) {
				LeagueName.Dispose ();
				LeagueName = null;
			}

			if (Country != null) {
				Country.Dispose ();
				Country = null;
			}

			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}

			if (StadiumName != null) {
				StadiumName.Dispose ();
				StadiumName = null;
			}

			if (StadiumAddress != null) {
				StadiumAddress.Dispose ();
				StadiumAddress = null;
			}
		}
	}
}
