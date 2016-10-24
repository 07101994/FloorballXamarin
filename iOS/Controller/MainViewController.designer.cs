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
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		UIKit.UIButton Button { get; set; }

		[Outlet]
		UIKit.UIButton Button2 { get; set; }

		[Action ("button2TouchUpInside:")]
		partial void button2TouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (Button != null) {
				Button.Dispose ();
				Button = null;
			}

			if (Button2 != null) {
				Button2.Dispose ();
				Button2 = null;
			}
		}
	}
}
