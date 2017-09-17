// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace FloorballAdminiOS.UI.Login
{
	[Register ("LoginViewController")]
	partial class LoginViewController
	{
		[Outlet]
		UIKit.UIView LoginView { get; set; }

		[Outlet]
		UIKit.UITextField TFPassword { get; set; }

		[Outlet]
		UIKit.UITextField TFUserName { get; set; }

		[Action ("LoginTap:")]
		partial void LoginTap (UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LoginView != null) {
				LoginView.Dispose ();
				LoginView = null;
			}

			if (TFUserName != null) {
				TFUserName.Dispose ();
				TFUserName = null;
			}

			if (TFPassword != null) {
				TFPassword.Dispose ();
				TFPassword = null;
			}
		}
	}
}
