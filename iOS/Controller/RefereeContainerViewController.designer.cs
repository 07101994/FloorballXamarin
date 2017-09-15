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
    [Register ("RefereeContainerViewController")]
    partial class RefereeContainerViewController
    {
        [Outlet]
        UIKit.UILabel RefereeName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (RefereeName != null) {
                RefereeName.Dispose ();
                RefereeName = null;
            }
        }
    }
}