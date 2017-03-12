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
using Newtonsoft.Json;

namespace Floorball.Droid.Utils
{
    public static class BundleExtensions
    {
        public static void PutObject<T>(this Bundle bundle, string key,T o)
        {
            bundle.PutString(key, JsonConvert.SerializeObject(o));
        }

        public static T GetObject<T>(this Bundle bundle, string key)
        {
            return JsonConvert.DeserializeObject<T>(bundle.GetString(key));
        }

    }

    public static class IntentExtension
    {
        public static void PutObject<T>(this Intent intent, string key, T o)
        {
            intent.PutExtra(key, JsonConvert.SerializeObject(o));
        }

        public static T GetObject<T>(this Intent intent, string key)
        {
            return JsonConvert.DeserializeObject<T>(intent.GetStringExtra(key));
        }
    }
}