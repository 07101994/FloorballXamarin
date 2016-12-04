using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class RefereesViewController : UITableViewController
	{

		public IEnumerable<Referee> Referees { get; set; }

		public UnitOfWork UoW { get; set; }

		public RefereesViewController() : base("RefereesViewController", null)
		{
		}

		public RefereesViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			UoW = new UnitOfWork();
		}


		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Referees.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("RefereeCell", indexPath);

			cell.TextLabel.Text = Referees.ElementAt(indexPath.Row).Name;

			return cell;
		}


		public override void RowSelected(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var referee = Referees.ElementAt(indexPath.Row);
			var leagues = UoW.LeagueRepo.GetLeaguesByReferee(referee.Id);

			var vc = Storyboard.InstantiateViewController("RefereeViewController") as RefereeViewController;
			vc.Referee = referee;
			vc.Leagues = leagues;
			vc.StatsByLeague = CreateStatsByLeague(leagues);

			ParentViewController.NavigationController.PushViewController(vc, true);
		}


		private List<RefereeStatModel> CreateStatsByLeague(IEnumerable<League> leagues)
		{

			List<RefereeStatModel> stats = new List<RefereeStatModel>();


			foreach (var league in leagues)
			{
				List<Event> events = UoW.EventRepo.GetEventsByLeague(league.Id).ToList();
				stats.Add(new RefereeStatModel() { 
					NumberOfMatches = events.Count,
					TwoMinutesPenalties = events.Where(e => e.Type == "P2").Count(),
					FiveMinutesPenalties = events.Where(e => e.Type == "P5").Count(),
					TenMinutesPenalties = events.Where(e => e.Type == "P10").Count(),
					FinalPenalties = events.Where(e => e.Type == "PV").Count()
				
				});

			}

			return stats;
		}
	
	}
}

