using System;
using System.Collections.Generic;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace Floorball.iOS
{
	public partial class LeagueChooserViewController : UITableViewController
	{
		public List<ChooserModel> Leagues { get; set; }

		public string Key { get; set; }

		public NSUserDefaults UserDefaults
		{
			get;
			set;
		}

		public LeagueChooserViewController() : base("LeagueChooserViewController", null)
		{
		}

		public LeagueChooserViewController(IntPtr handle) : base(handle)
		{
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Leagues.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LeagueChooserCell", indexPath);

			cell.TextLabel.Text = Leagues[indexPath.Row].Name;

			var switchView = new UISwitch();

			switchView.SetState(Leagues[indexPath.Row].Value, true);
			switchView.Tag = indexPath.Row;

			switchView.ValueChanged += SwitchValueChanged;

			cell.AccessoryView = switchView;

			return cell;
		}

		public void SwitchValueChanged(object sender, EventArgs e)
		{

			UserDefaults = NSUserDefaults.StandardUserDefaults;

			List<int> userLeaguesIds = GetIds(Key);

			var switchView = sender as UISwitch;
			var leagueId = Leagues[Convert.ToInt16(switchView.Tag)].Id;

			if (switchView.On)
			{
				userLeaguesIds.Add(leagueId);
				//if (Key == "leagues")
				//{
				//	AddAndSet(leagueId, "onehour");
				//	AddAndSet(leagueId, "event");
				//	AddAndSet(leagueId, "today");

				//}
			}
			else
			{
				userLeaguesIds.Remove(leagueId);

				if (Key == "leagues")
				{
                    RemoveAndSet(leagueId, "onehour");
					RemoveAndSet(leagueId, "event");
					RemoveAndSet(leagueId, "today");
				
				}
			}

			UserDefaults.SetString(JsonConvert.SerializeObject(userLeaguesIds), Key);

			UserDefaults.Synchronize();
		
		}

		private List<int> GetIds(string key)
		{
			List<int> ids;

			try
			{
				ids = JsonConvert.DeserializeObject<List<int>>(UserDefaults.StringForKey(key));
			}
			catch (Exception)
			{
				ids = new List<int>();					
			}

			return ids;
		}

		public void AddAndSet(int id, string key)
		{
			var ids = GetIds(key);

			ids.Add(id);
			UserDefaults.SetString(JsonConvert.SerializeObject(ids), key);

		}

		public void RemoveAndSet(int id, string key)
		{
			var ids = GetIds(key);

			ids.Remove(id);
			UserDefaults.SetString(JsonConvert.SerializeObject(ids), key);

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public class ChooserModel
		{
			public string Name
			{
				get;
				set;
			}

			public bool Value
			{
				get;
				set;
			}

			public int Id
			{
				get;
				set;
			}

		}
	}

}

