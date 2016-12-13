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
            if (ActualTime != null) {
                ActualTime.Dispose ();
                ActualTime = null;
            }

            if (AwayTeamImage != null) {
                AwayTeamImage.Dispose ();
                AwayTeamImage = null;
            }

            if (AwayTeamName != null) {
                AwayTeamName.Dispose ();
                AwayTeamName = null;
            }

            if (HomeTeamImage != null) {
                HomeTeamImage.Dispose ();
                HomeTeamImage = null;
            }

            if (HomeTeamName != null) {
                HomeTeamName.Dispose ();
                HomeTeamName = null;
            }

            if (LeagueName != null) {
                LeagueName.Dispose ();
                LeagueName = null;
            }

            if (MatchDate != null) {
                MatchDate.Dispose ();
                MatchDate = null;
            }

            if (Result != null) {
                Result.Dispose ();
                Result = null;
            }

            if (StadiumName != null) {
                StadiumName.Dispose ();
                StadiumName = null;
            }
        }
    }
}