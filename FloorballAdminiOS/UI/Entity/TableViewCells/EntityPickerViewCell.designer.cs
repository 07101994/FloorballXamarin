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
    [Register ("EntityPickerViewCell")]
    partial class EntityPickerViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UIPickerView PickerView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (PickerView != null) {
                PickerView.Dispose ();
                PickerView = null;
            }
        }
    }
}