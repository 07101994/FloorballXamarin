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
using Floorball.Droid.Utils;

namespace Floorball.Droid
{
    [Application]
    public class FloorballAppication : Application
    {

        public override void OnCreate()
        {
			UnitOfWork.DBManager = new DBManager();
			UnitOfWork.ImageManager = new ImageManager();
        }

        public FloorballAppication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
        {
           
        }

        public static bool IsInBackround { get; set; }

    }
}