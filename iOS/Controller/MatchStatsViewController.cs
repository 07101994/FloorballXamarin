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


		public List<PlayerWithEventsModel> HomePlayersWithEvents { get; set; }

		public List<PlayerWithEventsModel> AwayPlayersWithEvents { get; set; }

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

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

