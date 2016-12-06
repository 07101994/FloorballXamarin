using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using Floorball.Signalr;
using UIKit;

namespace Floorball.iOS
{
	public partial class ActualViewController : UITableViewController
	{

		public RootViewController Root { get; set; }

		public IEnumerable<Match> SoonMatches { get; set; }

		public IEnumerable<Team> SoonTeams { get; set; }

		public IEnumerable<Match> LiveMatches { get; set; }

		public IEnumerable<Team> LiveTeams { get; set; }

		public bool HasLiveMatch { get; set; }

		public bool HasSoonMatch { get; set; }

		public ActualViewController() : base("ActualViewController", null)
		{
		}

		public ActualViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			//Init properties
			InitProperties();

			//Subscrbe for server eventss
			Subscribe();

			//Connect to server
			ConnectToServer();

		}

		async void ConnectToServer()
		{
			if (HasLiveMatch && FloorballClient.Instance.ConnectionState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
			{
				try
				{
					await FloorballClient.Instance.Connect(AppDelegate.SharedAppDelegate.Countries);

				}
				catch (Exception ex)
				{

				}     
			}
		}

		private void Subscribe()
		{

			FloorballClient.Instance.MatchStarted += Instance_MatchStarted;
			FloorballClient.Instance.MatchEnded += Instance_MatchEnded;;
			FloorballClient.Instance.NewEventAdded += Instance_NewEventAdded;;
			FloorballClient.Instance.MatchTimeUpdated += Instance_MatchTimeUpdated;;
		
		}

		private void InitProperties()
		{
			var matches = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetActualMatches();

			SoonMatches = matches.Where(m => m.State != StateEnum.Playing);

			if (SoonMatches.Count() == 0)
			{
				HasSoonMatch = false;
			}
			else
			{
				HasSoonMatch = true;
				SoonTeams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByMatches(SoonMatches);
			}

			LiveMatches = matches.Where(m => m.State == StateEnum.Playing);

			if (LiveMatches.Count() == 0)
			{
				HasLiveMatch = false;
		    }
			else
			{
				HasLiveMatch = true;
				LiveTeams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByMatches(LiveMatches);
			}


		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 2;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{

			UITableViewCell cell = tableView.DequeueReusableCell("HeaderCell");

			if (section == 0)
			{
				(cell.ViewWithTag(0) as UILabel).Text = "Live";
			} 
			else
			{
				(cell.ViewWithTag(0) as UILabel).Text = "Soon";;
			}

			return cell;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (section == 0)
			{
				return LiveMatches.Count();
			}

			return SoonMatches.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell("LiveMatchCell", indexPath);

			Match match;
			Team homeTeam;
			Team awayTeam;

			if (indexPath.Section == 0)
			{
				match = LiveMatches.ElementAt(indexPath.Row);
				homeTeam = LiveTeams.First(t => t.Id == match.HomeTeamId);
				awayTeam = LiveTeams.First(t => t.Id == match.AwayTeamId);
			}
			else
			{
				match = SoonMatches.ElementAt(indexPath.Row);
 				homeTeam = SoonTeams.First(t => t.Id == match.HomeTeamId);
				awayTeam = SoonTeams.First(t => t.Id == match.AwayTeamId);	
			}

			(cell.ViewWithTag(0) as UILabel).Text = match.Date.ToString();
			(cell.ViewWithTag(2) as UILabel).LineBreakMode = UILineBreakMode.WordWrap;
			(cell.ViewWithTag(2) as UILabel).Text = UIHelper.GetMatchTime(match.Time, match.State).Replace("\\n"," ");
			(cell.ViewWithTag(3) as UILabel).Text = homeTeam.Name;
			(cell.ViewWithTag(4) as UILabel).Text = match.GoalsH.ToString();
			(cell.ViewWithTag(5) as UILabel).Text = awayTeam.Name;
			(cell.ViewWithTag(6) as UILabel).Text = match.GoalsA.ToString();


			return cell;
		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}

		void Instance_MatchTimeUpdated(int id)
		{

			var index = LiveMatches.ToList().IndexOf(LiveMatches.First(m => m.Id == id));

			var match = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetMatchById(id);

			var newTime = UIHelper.GetMatchTime(match.Time, match.State);



			
		}

		void Instance_NewEventAdded(int id)
		{

		}

		void Instance_MatchEnded(int id)
		{

		}

		void Instance_MatchStarted(int id)
		{

		}

	}
}

