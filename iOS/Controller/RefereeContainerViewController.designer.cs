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
