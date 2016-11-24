using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.LocalDB;
using Foundation;
using SidebarNavigation;
using UIKit;

namespace Floorball.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		
		public SortedSet<CountriesEnum> Countries { get; set; }
		public DateTime LastSyncDate { get; set; }
		public UnitOfWork UoW { get; set; }

		public override UIWindow Window { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			// If not required for your application you can safely delete this method

			var settings = NSUserDefaults.StandardUserDefaults;

			//Get countries
			Countries = GetCountriesFromSettings(settings);

			//Get Last Sync Date
			LastSyncDate = GetLastSyncDate(settings);

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			//Check first launch
			if (LastSyncDate.CompareTo(new DateTime(2000, 12, 12)) == 0)
			{
				InitAppAsync(settings);
			}
			else
			{
				UpdateAppAsync(settings);
			}

			return true;
		}

		private async void UpdateAppAsync(NSUserDefaults settings)
		{
			try
			{

				//Check is there any remote database updates and update local DB
				Task<bool> isUpdated = Updater.Instance.UpdateDatabaseFromServer(LastSyncDate);

				ShowControllerFromSoryBoard("Root");
				Window.MakeKeyAndVisible();

				if (await isUpdated) 
				{
					LastSyncDate = Updater.Instance.LastSyncDate;
					//update last sync date
					settings.SetString(LastSyncDate.ToString(), "lastSyncDate");
				
				} 
				else
				{
					throw new Exception("Error during updating from database!");
				}

			}
			catch (Exception)
			{

			}
		}

		private void ShowControllerFromSoryBoard(string controllerID)
		{

			var storyBoard = UIStoryboard.FromName("Main", null);
			var viewController = storyBoard.InstantiateViewController(controllerID);

			Window.RootViewController = viewController;

		}

		private async void InitAppAsync(NSUserDefaults settings)
		{

			try
			{
				Task<DateTime> lastSyncDateTask;

				//Init the whole local DB
				lastSyncDateTask = Manager.InitLocalDatabase();

				//Show app initializing
				ShowControllerFromSoryBoard("Init");
				Window.MakeKeyAndVisible();
				

				//Initializing finished
				LastSyncDate = await lastSyncDateTask;
				Updater.Instance.LastSyncDate = LastSyncDate;

				//update last sync date
				settings.SetString(LastSyncDate.ToString(), "lastSyncDate");

				//Set the root view controller
				ShowControllerFromSoryBoard("Root");
				
			}
			catch (Exception)
			{

			}

		}

		public override void OnResignActivation(UIApplication application)
		{
			// Invoked when the application is about to move from active to inactive state.
			// This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
			// or when the user quits the application and it begins the transition to the background state.
			// Games should use this method to pause the game.
		}

		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
		}

		public override void WillEnterForeground(UIApplication application)
		{
			// Called as part of the transiton from background to active state.
			// Here you can undo many of the changes made on entering the background.
		}

		public override void OnActivated(UIApplication application)
		{
			// Restart any tasks that were paused (or not yet started) while the application was inactive. 
			// If the application was previously in the background, optionally refresh the user interface.
		}

		public override void WillTerminate(UIApplication application)
		{
			// Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
		}

		public static AppDelegate SharedAppDelegate()
		{

			return UIApplication.SharedApplication.Delegate as AppDelegate;

		}

		private SortedSet<CountriesEnum> GetCountriesFromSettings(NSUserDefaults settings)
		{
			var countries = new SortedSet<CountriesEnum> ();

			var possibleCountires = new List<string>() { "HU", "SW", "SE", "FL", "CZ"};


			foreach (var c in possibleCountires)
			{
				if (settings.BoolForKey(c))
				{
					countries.Add(c.ToEnum<CountriesEnum>());
				}
			}


			return countries;
		}

		private DateTime GetLastSyncDate(NSUserDefaults settings)
		{
			var lastSyncDate = settings.StringForKey("lastSyncDate");


			if (lastSyncDate != "") 
			{
				return DateTime.Parse(lastSyncDate);
			}

			return new DateTime(2000,12,12);
		}
	}
}

