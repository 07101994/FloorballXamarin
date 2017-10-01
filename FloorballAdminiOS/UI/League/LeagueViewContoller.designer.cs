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

namespace FloorballAdminiOS.UI.League
{
    [Register ("LeagueViewContoller")]
    partial class LeagueViewContoller
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView CountryChooser { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField TFLeagueName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPickerView YearChooser { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CountryChooser != null) {
                CountryChooser.Dispose ();
                CountryChooser = null;
            }

            if (TFLeagueName != null) {
                TFLeagueName.Dispose ();
                TFLeagueName = null;
            }

            if (YearChooser != null) {
                YearChooser.Dispose ();
                YearChooser = null;
            }
        }
    }
}