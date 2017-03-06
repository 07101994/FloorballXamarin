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
using Floorball.Droid.Models;
using Newtonsoft.Json;
using Android.Support.V7.Widget;
using Floorball.Droid.Adapters;
using Android.Support.V4.App;
using Floorball.LocalDB.Tables;
using Android.Graphics;
using System.IO;
using Floorball.Droid.Activities;

namespace Floorball.Droid.Fragments
{
    public class EventsFragment : Fragment
    {
        RecyclerView recyclerView;
        EventsAdapter adapter;

        public static EventsFragment Instance(List<MatchEventModel> events)
        {
            var fragment = new EventsFragment();

            Bundle args = new Bundle();
            args.PutString("events", JsonConvert.SerializeObject(events));

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            adapter = new EventsAdapter(JsonConvert.DeserializeObject<List<MatchEventModel>>(Arguments.GetString("events")));
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.Events, container, false);

            var activity = Activity as MatchActivity;

            root.FindViewById<TextView>(Resource.Id.homeTeamName).Text = activity.HomeTeam.Name;
            root.FindViewById<TextView>(Resource.Id.awayTeamName).Text = activity.AwayTeam.Name;

            root.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = activity.Match.GoalsH.ToString();
            root.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = activity.Match.GoalsA.ToString();

            root.FindViewById<TextView>(Resource.Id.actualTime).Text = activity.Match.Time.Hours == 1 ? "Vége" : activity.Match.Time.Minutes.ToString() + ":" + activity.Match.Time.Seconds.ToString();

            SetTeamImage(activity.HomeTeam, root.FindViewById<ImageView>(Resource.Id.homeTeamImage));
            SetTeamImage(activity.AwayTeam, root.FindViewById<ImageView>(Resource.Id.awayTeamImage));

            recyclerView = root.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(Context));
            recyclerView.SetAdapter(adapter);

            return root;
        }

        private void SetTeamImage(Team team, ImageView imageView)
        {
            try
            {
                var bitmap = BitmapFactory.DecodeStream(File.OpenRead(ImageManager.GetImagePath(team.ImageName)));

                if (bitmap == null)
                {
                    throw new Exception("Image not found!");
                }

                imageView.SetImageBitmap(bitmap);
            }
            catch (Exception)
            {
                imageView.SetImageResource(Resource.Drawable.ball);
                imageView.Alpha = 125;
            }
        }
    }
}