// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FloorballAdminiOS.UI.Matches
{
    [Register ("MatchViewCell")]
    partial class MatchViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel AwayScore { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel AwayTeamName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel Date { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel HomeScore { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel HomeTeamName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel LeagueName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AwayScore != null) {
                AwayScore.Dispose ();
                AwayScore = null;
            }

            if (AwayTeamName != null) {
                AwayTeamName.Dispose ();
                AwayTeamName = null;
            }

            if (Date != null) {
                Date.Dispose ();
                Date = null;
            }

            if (HomeScore != null) {
                HomeScore.Dispose ();
                HomeScore = null;
            }

            if (HomeTeamName != null) {
                HomeTeamName.Dispose ();
                HomeTeamName = null;
            }

            if (LeagueName != null) {
                LeagueName.Dispose ();
                LeagueName = null;
            }
        }
    }
}