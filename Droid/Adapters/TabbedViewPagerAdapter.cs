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
using Newtonsoft.Json;
using Java.Lang;
using Floorball.Droid.Fragments;
using Newtonsoft.Json.Linq;
using Floorball.LocalDB.Tables;
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    public class TabbedViewPagerAdapter : FragmentStatePagerAdapter
    {
        public List<TabbedViewPagerModel> Model { get; set; }

        public TabbedViewPagerAdapter(FragmentManager manager, string model) : base(manager) 
        {
            Model = JsonConvert.DeserializeObject<List<TabbedViewPagerModel>>(model);   
        }

        public override int Count
        {
            get
            {
                return Model.Count;
            }
        }

        public override Fragment GetItem(int position)
        {
            Fragment fr = null;

            var data = Model[position].Data as JContainer;
            
            switch (Model[position].FragmentType)
            {
                case FragmentType.Leagues:
                    fr = LeaguesFragment.Instance(data.ToObject<LeaguesModel>());
                    break;
                case FragmentType.Teams:
                    var teamsModel = data.ToObject<TeamsModel>();
                    fr = TeamsFragment.Instance(teamsModel.Teams, teamsModel.Leagues);
                    break;
                case FragmentType.Players:
                    fr = new Fragment();
                    break;
                case FragmentType.Matches:
                    fr = new Fragment();
                    break;
                case FragmentType.Stats:
                    fr = new Fragment();
                    break;
                case FragmentType.Table:
                    fr = new Fragment();
                    break;
                default:
                    break;
            }

            return fr;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.String(Model[position].TabTitle);
        }
    }
}