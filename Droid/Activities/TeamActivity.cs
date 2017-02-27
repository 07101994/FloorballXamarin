using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Floorball.Droid.Fragments;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using Android.App;
using FloorballServer.Models.Floorball;
using Newtonsoft.Json;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using Android.Graphics;
using System.IO;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "TeamActivity")]
    public class TeamActivity : FloorballActivity
    {

        TeamPageAdapter pagerAdapter;

        public Team Team { get; set; }

        public List<Player> Players { get; set; }

        public List<Match> Matches { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TeamActivity);

            //Initialize toolbar
            InitToolbar();

            //Init activity properties
            InitActivityProperties();

            //Initialize properties
            InitProperties();

            FindViewById<TextView>(Resource.Id.coachName).Text = Team.Coach;
            FindViewById<TextView>(Resource.Id.stadiumName).Text = UoW.StadiumRepo.GetStadiumById(Team.StadiumId).Name;
            FindViewById<TextView>(Resource.Id.teamName).Text = Team.Name;

            try
            {
                var bitmap = BitmapFactory.DecodeStream(File.OpenRead(ImageManager.GetImagePath(Team.ImageName)));

                if (bitmap == null)
                {
                    throw new System.Exception("Image not found!");
                }

                FindViewById<ImageView>(Resource.Id.teamImage).SetImageBitmap(bitmap);
            }
            catch (System.Exception)
            {
                var teamImageView = FindViewById<ImageView>(Resource.Id.teamImage);
                teamImageView.SetImageResource(Resource.Drawable.ball);
                teamImageView.Alpha = 125;
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:

                    Finish();

                    return true;

                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void InitProperties()
        {
            base.InitProperties();

            Team = JsonConvert.DeserializeObject<Team>(Intent.GetStringExtra("team"));
            Players = UoW.PlayerRepo.GetPlayersByTeam(Team.Id).ToList();
            Matches = UoW.MatchRepo.GetMatchesByTeam(Team.Id).OrderBy(m => m.LeagueId).ThenBy(m => m.Date).ToList();
        }

        protected override void InitActivityProperties()
        {
            ViewPager pager = FindViewById<ViewPager>(Resource.Id.pager);
            pagerAdapter = new TeamPageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            FindViewById<TabLayout>(Resource.Id.tabs).SetupWithViewPager(pager);
        }

        public class TeamPageAdapter : Android.Support.V4.App.FragmentPagerAdapter
        {
            public TeamPageAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
            {

            }

            public override int Count
            {
                get
                {
                    return 2;
                }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:
                        return new PlayerListFragment();
                    case 1:
                        return new MatchListFragment();
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Játékosok");
                    case 1:
                        return new Java.Lang.String("Mérkőzések");
                    default:
                        return null;
                }

            }


        }
    }
}