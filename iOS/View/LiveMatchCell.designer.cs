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
	[Register ("LiveMatchCell")]
	partial class LiveMatchCell
	{
		[Outlet]
		public UIKit.UILabel AwayScore { get; set; }

		[Outlet]
		public UIKit.UILabel AwayTeam { get; set; }

		[Outlet]
		public UIKit.UILabel Date { get; set; }

		[Outlet]
		public UIKit.UILabel HomeScore { get; set; }

		[Outlet]
		public UIKit.UILabel HomeTeam { get; set; }

		[Outlet]
		public UIKit.UIView Indicator { get; set; }

		[Outlet]
		public UIKit.UILabel Time { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (Indicator != null) {
				Indicator.Dispose ();
				Indicator = null;
			}

			if (Date != null) {
				Date.Dispose ();
				Date = null;
			}

			if (Time != null) {
				Time.Dispose ();
				Time = null;
			}

			if (HomeTeam != null) {
				HomeTeam.Dispose ();
				HomeTeam = null;
			}

			if (AwayTeam != null) {
				AwayTeam.Dispose ();
				AwayTeam = null;
			}

			if (HomeScore != null) {
				HomeScore.Dispose ();
				HomeScore = null;
			}

			if (AwayScore != null) {
				AwayScore.Dispose ();
				AwayScore = null;
			}
		}
	}
}
