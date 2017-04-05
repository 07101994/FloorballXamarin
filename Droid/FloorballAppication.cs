using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Floorball.Droid
{
    [Application]
    public class FloorballAppication : Application
    {

        public FloorballAppication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
        {

        }

        public static bool IsInBackround { get; set; }

    }
}