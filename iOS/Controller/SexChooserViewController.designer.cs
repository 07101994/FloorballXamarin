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
    [Register ("SexChooserViewController")]
    partial class SexChooserViewController
    {
        [Outlet]
        UIKit.UILabel Year { get; set; }


        [Action ("SexChanged:")]
        partial void SexChanged (UIKit.UISegmentedControl sender);

        void ReleaseDesignerOutlets ()
        {
            if (Year != null) {
                Year.Dispose ();
                Year = null;
            }
        }
    }
}