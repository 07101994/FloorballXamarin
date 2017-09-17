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
using Floorball.Droid.Models;
using Floorball.Droid.Utils;

namespace Floorball.Droid.Activities
{
    [Activity(Label = "TeamActivity")]
    public class TeamActivity : FloorballActivity
    {

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

            //Initialize properties
            InitProperties();

            //Init activity properties
            InitActivityProperties();

            //Set team image
            SetTeamImage();

            //Attach tabbedfragment
            if (savedInstanceState == null)
            {
                var tabModels = new List<TabbedViewPagerModel>();
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.Players, TabTitle = Resources.GetString(Resource.String.teamPlayers), Data = Players.Select(p => new ListModel { Text = p.Name, Object = p }) } );
                tabModels.Add(new TabbedViewPagerModel { FragmentType = FragmentType.TeamMatches, TabTitle = Resources.GetString(Resource.String.teamMatches), Data = new MatchesModel { Matches = Matches, TeamId = Team.Id, Teams = UoW.TeamRepo.GetTeamsByMatches(Matches), Leagues = UoW.LeagueRepo.GetLeaguesByMatches(Matches) } });

                Android.Support.V4.App.Fragment fr = TabbedViewPagerFragment.Instance(tabModels);
                Android.Support.V4.App.FragmentTransaction ft = SupportFragmentManager.BeginTransaction();
                ft.Add(Resource.Id.content_frame, fr).Commit();
            }

        }

        private void SetTeamImage()
        {
            try
            {
                var bitmap = BitmapFactory.DecodeStream(File.OpenRead(UnitOfWork.ImageManager.GetImagePath(Team.ImageName)));

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

            Team = Intent.GetObject<Team>("team");
            Players = UoW.PlayerRepo.GetPlayersByTeam(Team.Id).ToList();
            Matches = UoW.MatchRepo.GetMatchesByTeam(Team.Id).OrderBy(m => m.LeagueId).ThenBy(m => m.Date).ToList();
        }

        protected override void InitActivityProperties()
        {
            FindViewById<TextView>(Resource.Id.coachName).Text = Team.Coach;
            FindViewById<TextView>(Resource.Id.stadiumName).Text = UoW.StadiumRepo.GetStadiumById(Team.StadiumId).Name;
            FindViewById<TextView>(Resource.Id.teamName).Text = Team.Name;
        }

    }
}