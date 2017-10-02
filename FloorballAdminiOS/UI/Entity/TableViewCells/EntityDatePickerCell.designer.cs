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
    [Register ("EntityDatePickerCell")]
    partial class EntityDatePickerCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        public UIKit.UIDatePicker DatePicker { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DatePicker != null) {
                DatePicker.Dispose ();
                DatePicker = null;
            }
        }
    }
}