using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Floorball.Droid.Activities;
using FloorballServer.Models.Floorball;
using Android.Support.V7.Widget;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Adapters;
using Newtonsoft.Json;
using Floorball.Droid.Models;

namespace Floorball.Droid.Fragments
{
    public abstract class MatchesFragment : Fragment
    {

        RecyclerView recyclerView;
        protected MatchesAdapter adapter;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            adapter.ClickedObject += Adapter_ClickedObject;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_ClickedObject(object sender, object e)
        {
            Intent intent = new Intent(Context, typeof(MatchActivity));
            intent.PutExtra("id", (e as MatchResultModel).Id);
            StartActivity(intent);
        }
    }
}