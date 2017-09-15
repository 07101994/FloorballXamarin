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
            if (Country != null) {
                Country.Dispose ();
                Country = null;
            }

            if (Date != null) {
                Date.Dispose ();
                Date = null;
            }

            if (LeagueName != null) {
                LeagueName.Dispose ();
                LeagueName = null;
            }

            if (StadiumAddress != null) {
                StadiumAddress.Dispose ();
                StadiumAddress = null;
            }

            if (StadiumName != null) {
                StadiumName.Dispose ();
                StadiumName = null;
            }
        }
    }
}