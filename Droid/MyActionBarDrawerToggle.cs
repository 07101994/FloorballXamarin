using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;

namespace Floorball.Droid
{
    public class MyActionBarDrawerToggle : ActionBarDrawerToggle
    {
        private Activity activity;
        private int mOpenedResource;
        private int mClosedResource;
        private DrawerLayout drawerLayout;
        

        private float last = 0;
        public bool IsMoving { get; set; }

        public MyActionBarDrawerToggle(Activity host, DrawerLayout drawerLayout, int openedResource, int closedResource)
        : base(host, drawerLayout, openedResource, closedResource)
        {
            activity = host;
            mOpenedResource = openedResource;
            mClosedResource = closedResource;
            this.drawerLayout = drawerLayout;
            IsMoving = false;
            
        }

        public override void OnDrawerOpened(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerOpened(drawerView);
                IsMoving = false;
                //activity.SupportActionBar.SetTitle(mOpenedResource);
                //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedOpen);
                //(activity as MainActivity).HomeEnabled = true;
            }
        }

        public override void OnDrawerClosed(View drawerView)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                base.OnDrawerClosed(drawerView);
                IsMoving = false;
                //Console.WriteLine("Bezárult.");
                //activity.SupportActionBar.SetTitle(mClosedResource);
                //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedOpen);

                //(activity as MainActivity).HomeEnabled = true;
            }
        }

        public override void OnDrawerSlide(View drawerView, float slideOffset)
        {
            int drawerType = (int)drawerView.Tag;

            if (drawerType == 0)
            {
                bool opening = slideOffset > last;
                bool closing = slideOffset < last;

                if (opening)
                {
                    if (!IsMoving)
                    {
                        //activity.SupportActionBar.SetTitle(mOpenedResource);
                        //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
                        IsMoving = true;
                    }
                }
                else if (closing)
                {
                    if (!IsMoving)
                    {
                        //activity.SupportActionBar.SetTitle(mClosedResource);
                        IsMoving = true;
                    }
                }

                last = slideOffset;


                //if (slideOffset == 0 && activity.SupportActionBar.NavigationMode == Android.Support.V7.App.ActionBar.NavigationModeStandard)
                //{
                //    // drawer closed
                //    //activity.SupportActionBar.NavigationMode = Android.Support.V7.App.ActionBar.NavigationModeTabs;
                //    //activity.InvalidateOptionsMenu();
                //    activity.SupportActionBar.SetTitle(mClosedResource);

                //}
                //else
                //{
                //    if (slideOffset != 0 && activity.SupportActionBar.NavigationMode == Android.Support.V7.App.ActionBar.NavigationModeTabs)
                //    {
                //        // started opening
                //        //activity.SupportActionBar.NavigationMode = Android.Support.V7.App.ActionBar.NavigationModeStandard;
                //        //activity.InvalidateOptionsMenu();
                //        activity.SupportActionBar.SetTitle(mOpenedResource);
                //    }
                //} 

                base.OnDrawerSlide(drawerView, slideOffset);
            }


        }

    }
}

