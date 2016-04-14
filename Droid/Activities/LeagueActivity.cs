﻿using System;
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
using Floorball.Droid.Fragments;
using Android.Support.V4.View;
using Java.Lang;
using Android.Support.Design.Widget;
using FloorballServer.Models.Floorball;
using Floorball.REST;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "LeagueActivity")]
    public class LeagueActivity : Android.Support.V7.App.AppCompatActivity
    {

        LeaguePageAdapter pagerAdapter;
        public int LeagueId { get; set; }

        public List<TeamModel> Teams { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.League);

            ViewPager pager = FindViewById<ViewPager>(Resource.Id.leaguePager);
            pagerAdapter = new LeaguePageAdapter(SupportFragmentManager);
            pager.Adapter = pagerAdapter;

            TabLayout tabs = FindViewById<TabLayout>(Resource.Id.leaguetabs);
            tabs.SetupWithViewPager(pager);

            SupportActionBar.Title = Intent.GetStringExtra("leagueName");
            LeagueId = Intent.GetIntExtra("leagueId",0);

            Teams = new List<TeamModel>();
            TeamModel model = new TeamModel();
            model.Name = "PFSE";
            model.Get = 10;
            model.Scored = 80;
            model.Standing = 1;
            model.Points = 12;
            model.Match = 6;
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);
            Teams.Add(model);

            //Teams = RESTHelper.GetTeamsByLeague(LeagueId);
        }

        public class LeaguePageAdapter : FragmentPagerAdapter
        {
            public LeaguePageAdapter(Android.Support.V4.App.FragmentManager manager) : base(manager)
            {

            }

            public override int Count
            {
                get
                {
                    return 3;
                }
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                switch (position)
                {
                    case 0:
                        return new LeagueMatchesFragment();
                    case 1:
                        return new LeagueTableFragment();
                    case 2:
                        return new LeagueStatisticsFragment();
                    default:
                        return null;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                switch (position)
                {
                    case 0:
                        return new Java.Lang.String("Mérkőzések");
                    case 1:
                        return new Java.Lang.String("Tabella");
                    case 2:
                        return new Java.Lang.String("Statisztikák");
                    default:
                        return null;
                }
             
            }

            
        }


    }
}