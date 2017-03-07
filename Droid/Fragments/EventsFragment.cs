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
using Floorball.Droid.Utils;

namespace Floorball.Droid.Fragments
{
    public class EventsFragment : Fragment
    {
        RecyclerView recyclerView;
        EventsAdapter adapter;

        public Match Match { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public static EventsFragment Instance(IEnumerable<MatchEventModel> events, Match match, Team homeTeam, Team awayTeam)
        {
            var fragment = new EventsFragment();

            Bundle args = new Bundle();
            args.PutObject("events", events);
            args.PutObject("match", match);
            args.PutObject("homeTeam", homeTeam);
            args.PutObject("awayTeam", awayTeam);

            fragment.Arguments = args;

            return fragment;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            adapter = new EventsAdapter(Arguments.GetObject<IEnumerable<MatchEventModel>>("events"));
            Match = Arguments.GetObject<Match>("match");
            HomeTeam = Arguments.GetObject<Team>("homeTeam");
            AwayTeam = Arguments.GetObject<Team>("awayTeam");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View root = inflater.Inflate(Resource.Layout.Events, container, false);

            root.FindViewById<TextView>(Resource.Id.homeTeamName).Text = HomeTeam.Name;
            root.FindViewById<TextView>(Resource.Id.awayTeamName).Text = AwayTeam.Name;

            root.FindViewById<TextView>(Resource.Id.homeTeamScore).Text = Match.GoalsH.ToString();
            root.FindViewById<TextView>(Resource.Id.awayTeamScore).Text = Match.GoalsA.ToString();

            root.FindViewById<TextView>(Resource.Id.actualTime).Text = Match.Time.Hours == 1 ? "Vége" : Match.Time.Minutes.ToString() + ":" + Match.Time.Seconds.ToString();

            SetTeamImage(HomeTeam, root.FindViewById<ImageView>(Resource.Id.homeTeamImage));
            SetTeamImage(AwayTeam, root.FindViewById<ImageView>(Resource.Id.awayTeamImage));

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