using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Floorball.Droid.Activities;
using Floorball.Droid.Utils;
using Floorball.Droid.Models;
using Newtonsoft.Json;

namespace Floorball.Droid.Fragments
{

    public class ListFragment : MainFragment
    {
        public string Type { get; set; }

        private RecyclerView recyclerView;
        private ListAdapter adapter;

        public static ListFragment Instance(IEnumerable<ListModel> list, string type)
        {
            var fragment = new ListFragment();
            Bundle args = new Bundle();
            args.PutString("type", type);
            args.PutObject("list", list);

            fragment.Arguments = args;

            return fragment;
        }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new ListAdapter(Arguments.GetObject<IEnumerable<ListModel>>("list"));
            Type = Arguments.GetString("type");

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.RecycleView, container, false);

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            adapter.ClickedObject += Adapter_Clicked;
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void Adapter_Clicked(object sender, object e)
        {
            Intent intent = null;

            var model = e as ListModel;

            switch (Type)
            {
                case "leagues":
                    intent = new Intent(Activity, typeof(LeaguesActivity));
                    intent.PutObject("year", new DateTime(Convert.ToInt16(model.Object),1,1));
                    break;
                case "teams":
                    intent = new Intent(Activity, typeof(TeamsActivity));
                    intent.PutObject("year", new DateTime(Convert.ToInt16(model.Object), 1, 1));
                    break;
                case "referees":
                    intent = new Intent(Activity, typeof(RefereeActivity));
                    intent.PutObject("referee", model.Object);
                    break;
                case "players":
                    intent = new Intent(Activity, typeof(PlayerActivity));
                    intent.PutObject("player", model.Object);
                    break;
                default:
                    break;
            }

            StartActivity(intent);
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }
    }
}