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
    public  class TabbedViewPagerModel
    {
        public FragmentType FragmentType { get; set; }
        public string TabTitle { get; set; }
        public object Data { get; set; }

    }
}