// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

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
            if (AwayScore != null) {
                AwayScore.Dispose ();
                AwayScore = null;
            }

            if (AwayTeam != null) {
                AwayTeam.Dispose ();
                AwayTeam = null;
            }

            if (Date != null) {
                Date.Dispose ();
                Date = null;
            }

            if (HomeScore != null) {
                HomeScore.Dispose ();
                HomeScore = null;
            }

            if (HomeTeam != null) {
                HomeTeam.Dispose ();
                HomeTeam = null;
            }

            if (Indicator != null) {
                Indicator.Dispose ();
                Indicator = null;
            }

            if (Time != null) {
                Time.Dispose ();
                Time = null;
            }
        }
    }
}