using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.Droid.Models;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchStatsViewController : UITableViewController
	{

		public IEnumerable<Player> HomePlayers { get; set; }

		public IEnumerable<Player> AwayPlayers { get; set; }

		public IEnumerable<Event> Events { get; set; }

		private List<PlayerWithEventsModel> HomePlayersWithEvents { get; set; }

		private List<PlayerWithEventsModel> AwayPlayersWithEvents { get; set; }

		public MatchStatsViewController() : base("MatchStatsViewController", null)
		{
		}

		public MatchStatsViewController(IntPtr handle) : base(handle)
		{



		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			HomePlayersWithEvents = CreatePlayerEventsModel(HomePlayers, Events);
			AwayPlayersWithEvents = CreatePlayerEventsModel(AwayPlayers, Events);

		}

		private List<PlayerWithEventsModel> CreatePlayerEventsModel(IEnumerable<Player> players, IEnumerable<LocalDB.Tables.Event> events)
		{
			List<PlayerWithEventsModel> model = new List<PlayerWithEventsModel>();

			foreach (var player in players)
			{
				model.Add(new PlayerWithEventsModel { Name = player.ShortName, Events = events.Where(e => e.PlayerId == player.RegNum) });
			}

			return model.OrderBy(m => m.Goals).ThenBy(m => m.Assists).ThenBy(m => m.P2).ThenBy(m => m.P5).ThenBy(m => m.P10).ThenBy(m => m.PV).ThenBy(m => m.Number).ToList();
			                                                                                                                                                               
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return HomePlayersWithEvents.Count > AwayPlayersWithEvents.Count ? HomePlayersWithEvents.Count : AwayPlayersWithEvents.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("MatchPlayerStatCell", indexPath);

			cell = AddPlayer(cell, 900, HomePlayersWithEvents, indexPath.Row);
			cell = AddPlayer(cell, 901, AwayPlayersWithEvents, indexPath.Row);

			(cell.ViewWithTag(500) as UILabel).Text = (indexPath.Row + 1).ToString();

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			return 110;
		}

		private UITableViewCell AddPlayer(UITableViewCell cell, int containerTag, List<PlayerWithEventsModel> players, int index)
		{

			if (players.Count > index)
			{
				var player = players[index];

				(cell.ViewWithTag(containerTag).ViewWithTag(101) as UILabel).Text = player.Name;

				cell = AddStat(cell, containerTag, 1, player.Goals);
				cell = AddStat(cell, containerTag, 2, player.Assists);
				cell = AddStat(cell, containerTag, 3, player.P2);
				cell = AddStat(cell, containerTag, 4, player.P5);
				cell = AddStat(cell, containerTag, 5, player.P10);
				cell = AddStat(cell, containerTag, 6, player.PV);

			}
			else
			{
				cell.ViewWithTag(containerTag).Hidden = true;
			}

			return cell;
		}

		private UITableViewCell AddStat(UITableViewCell cell, int containerTag, int tag, int count)
		{

			if ( count > 0 )
			{
				cell.ViewWithTag(containerTag).ViewWithTag(200 + tag).Hidden = false;
				(cell.ViewWithTag(containerTag).ViewWithTag(300 + tag) as UILabel).Text = count.ToString();
				cell.ViewWithTag(containerTag).ViewWithTag(300 + tag).Hidden = false;
			} 
			else
			{
				cell.ViewWithTag(containerTag).ViewWithTag(200 + tag).Hidden = true;
				cell.ViewWithTag(containerTag).ViewWithTag(300 + tag).Hidden = true;
			}

			return cell;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

	}
}

