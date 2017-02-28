using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Floorball.Droid.Models;

namespace Floorball.Droid.Adapters
{
    public class PlayerStatsAdapter : RecyclerView.Adapter
    {

        private List<ListItem> listItems;
        private List<HeaderModel> headers;
        private List<object> contents;

        public override int ItemCount
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            throw new NotImplementedException();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            throw new NotImplementedException();
        }
    }
}