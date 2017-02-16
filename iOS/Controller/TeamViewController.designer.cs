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

            if (MatchesContainer != null) {
                MatchesContainer.Dispose ();
                MatchesContainer = null;
            }

            if (PlayersContainer != null) {
                PlayersContainer.Dispose ();
                PlayersContainer = null;
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
        }
    }
}