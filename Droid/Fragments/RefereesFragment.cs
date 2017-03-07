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
using Android.Support.V7.Widget;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class RefereesFragment : MainFragment
    {
        RecyclerView recyclerView;
        RefereesAdapter adapter;

        public IEnumerable<Referee> Referees { get; set; }

        public IEnumerable<Referee> ActualReferees { get; set; }

        public static RefereesFragment Instance()
        {
            return new RefereesFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            Referees = UoW.RefereeRepo.GetAllReferee().OrderBy(p => p.Name).ToList();
            ActualReferees = Referees;

            adapter = new RefereesAdapter(ActualReferees.ToList());
            adapter.ClickedObject += Adapter_Clicked;
        }

        private void Adapter_Clicked(object sender, object r)
        {
            Intent intent = new Intent(Context, typeof(RefereeActivity));
            intent.PutObject("referee", r);
            StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.SearchListFragment,container,false);

            root.FindViewById<TextView>(Resource.Id.fragmentName).Text = Resources.GetString(Resource.String.referees);

            EditText searchBox = root.FindViewById<EditText>(Resource.Id.playerSearch);
            searchBox.TextChanged += SearchBoxTextChanged;

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);

            return root;
        }


        private void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Text.ToString() == "")
            {
                ActualReferees = Referees;
            }
            else
            {
                ActualReferees = Referees.Where(p => p.Name.ToLower().Contains(e.Text.ToString().ToLower()));
            }

            adapter.Swap(ActualReferees.ToList());
        }

        public override void listItemSelected(string s)
        {
            throw new NotImplementedException();
        }

    }
}