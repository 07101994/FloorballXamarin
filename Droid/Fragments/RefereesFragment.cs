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
using Floorball.LocalDB.Tables;
using Floorball.LocalDB;
using Floorball.Droid.Adapters;
using Android.Text;
using Newtonsoft.Json;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{
    public class RefereesFragment : MainFragment
    {

        ListView refereeListView;

        public IEnumerable<Referee> Referees { get; set; }

        public IEnumerable<Referee> ActualReferees { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View root = inflater.Inflate(Resource.Layout.SearchListFragment,container,false);

            return root;
        }

        public override void OnStart()
        {
            base.OnStart();

            Referees = Manager.GetAllReferee().OrderBy(p => p.Name).ToList();
            ActualReferees = Referees;

            refereeListView = Activity.FindViewById<ListView>(Resource.Id.playersList);
            refereeListView.Adapter = new RefereesAdapter(Context, ActualReferees.ToList());
            refereeListView.ItemClick += RefereeListViewItemClick;

            EditText searchBox = Activity.FindViewById<EditText>(Resource.Id.playerSearch);
            searchBox.TextChanged += SearchBoxTextChanged;


        }

        private void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void RefereeListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(Context, typeof(RefereeActivity));
            intent.PutExtra("referee", JsonConvert.SerializeObject(ActualReferees.ElementAt(e.Position)));
            StartActivity(intent);
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }

    }
}