// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Floorball.iOS
{
	[Register ("MenuViewController")]
	partial class MenuViewController
	{
		[Outlet]
		UIKit.UIBarButtonItem BackButton { get; set; }

		[Action ("BackButtonPressed:")]
		partial void BackButtonPressed (UIKit.UIBarButtonItem sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (BackButton != null) {
				BackButton.Dispose ();
				BackButton = null;
			}
		}
	}
}
