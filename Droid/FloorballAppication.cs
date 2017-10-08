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
using FloorballPCL;

namespace Floorball.Droid
{
    [Application]
    public class FloorballAppication : Application
    {

        public static ITextManager TextManager { get; set; }

        public override void OnCreate()
        {
			UnitOfWork.DBManager = new DBManager();
			UnitOfWork.ImageManager = new ImageManager();

            TextManager = new TextManager(Context);

        }

        public FloorballAppication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference,transfer)
        {
           
        }

        public static bool IsInBackround { get; set; }

    }
}