// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
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
            if (AwayImage != null) {
                AwayImage.Dispose ();
                AwayImage = null;
            }

            if (AwayScore != null) {
                AwayScore.Dispose ();
                AwayScore = null;
            }

            if (AwayTeamName != null) {
                AwayTeamName.Dispose ();
                AwayTeamName = null;
            }

            if (HomeImage != null) {
                HomeImage.Dispose ();
                HomeImage = null;
            }

            if (HomeTeamName != null) {
                HomeTeamName.Dispose ();
                HomeTeamName = null;
            }

            if (HomsScore != null) {
                HomsScore.Dispose ();
                HomsScore = null;
            }

            if (Time != null) {
                Time.Dispose ();
                Time = null;
            }
        }
    }
}