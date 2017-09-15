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
		UIKit.UIView DetailsContainer { get; set; }

		[Outlet]
		UIKit.UIView EventsContainer { get; set; }

		[Outlet]
		UIKit.UIView PlayersContainer { get; set; }

		[Action ("SegmentChanged:")]
		partial void SegmentChanged (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (EventsContainer != null) {
				EventsContainer.Dispose ();
				EventsContainer = null;
			}

			if (PlayersContainer != null) {
				PlayersContainer.Dispose ();
				PlayersContainer = null;
			}

			if (DetailsContainer != null) {
				DetailsContainer.Dispose ();
				DetailsContainer = null;
			}
		}
	}
}
