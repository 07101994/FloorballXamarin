// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Floorball.iOS
{
    [Register ("LeagueViewController")]
    partial class LeagueViewController
    {
        [Outlet]
        UIKit.UILabel LeagueName { get; set; }


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
            if (LeagueName != null) {
                LeagueName.Dispose ();
                LeagueName = null;
            }

            if (MatchContainer != null) {
                MatchContainer.Dispose ();
                MatchContainer = null;
            }

            if (StatContainer != null) {
                StatContainer.Dispose ();
                StatContainer = null;
            }

            if (TableContainer != null) {
                TableContainer.Dispose ();
                TableContainer = null;
            }
        }
    }
}