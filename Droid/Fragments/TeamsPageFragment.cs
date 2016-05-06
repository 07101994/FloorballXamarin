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
using Floorball.REST;
using FloorballServer.Models.Floorball;

namespace Floorball.Droid.Fragments
{
    public class TeamsPageFragment : Fragment
    {
        List<string> years;

        public List<TeamModel> Teams { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            Teams = (Activity as MainActivity).Teams;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.TeamsPageFragment, container, false);

            Button yearsButton = root.FindViewById<Button>(Resource.Id.yearsbutton);
            years = RESTHelper.GetAllYear();

            yearsButton.Text = years.Last();

            yearsButton.Click += delegate
            {

                //ListDialogFragment listDialogFragment = new ListDialogFragment(years);
                //listDialogFragment.Show(Activity.SupportFragmentManager, "listdialog");

            };

            //TODO: lista a csapatokról


            return root;
        }
    }
}