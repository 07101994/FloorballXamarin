// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FloorballAdminiOS.UI.Entity.TableViewCells
{
    [Register ("EntityTextFieldCell")]
    partial class EntityTextFieldCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UILabel Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UITextField TextField { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Label != null) {
                Label.Dispose ();
                Label = null;
            }

            if (TextField != null) {
                TextField.Dispose ();
                TextField = null;
            }
        }
    }
}