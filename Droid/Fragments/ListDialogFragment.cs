using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.App;

namespace Floorball.Droid.Fragments
{
    public class ListDialogFragment : Android.Support.V4.App.DialogFragment
    {
        public ListView DialogList { get; set; }

        public List<string> Years { get; set; }


        public ListDialogFragment(List<string> years)
        {
            Years = years;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.ListDialogFragment, null, false);

            DialogList = view.FindViewById<ListView>(Resource.Id.listdialog);

            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            DialogList.ItemClick += (s, e) =>
            {
                (Activity as MainActivity).ListItemSelected(Years[e.Position]);
                Dismiss();
                
            };

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(Activity,Android.Resource.Layout.SimpleListItem1,Years);
            DialogList.Adapter = adapter;

        }

        
    }
}