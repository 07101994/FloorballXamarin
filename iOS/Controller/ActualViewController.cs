using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using Floorball.LocalDB.Tables;
using Floorball.Signalr;
using UIKit;
using Floorball.iOS;

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

		public List<int> SoonIndexes { get; set; }

		public List<int> LiveIndexes { get; set; }

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

			NavigationItem.TitleView = UIHelper.MakeImageWithLabel("logo","Floorball");

			//Init properties
			InitProperties();

			//Subscrbe for server eventss
			Subscribe();

			//Connect to server
			//ConnectToServer();

			TableView.TableFooterView = new UIView(CGRect.Empty);
			TableView.BackgroundView = null;
			TableView.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

		}

		async void ConnectToServer()
		{
			if (HasLiveMatch && FloorballClient.Instance.ConnectionState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
			{
				try
				{
					await FloorballClient.Instance.Connect(null);

				}
				catch (Exception)
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

		public void InitProperties()
		{
			var matches = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetActualMatches(AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetAllLeague());

			SoonMatches = matches.Where(m => m.State != StateEnum.Playing).OrderBy(m => m.LeagueId);

			if (SoonMatches.Count() == 0)
			{
				HasSoonMatch = false;
			}
			else
			{
				HasSoonMatch = true;
				SoonTeams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByMatches(SoonMatches);

				SoonIndexes = CreateIndexes(SoonMatches);

			}

			LiveMatches = matches.Where(m => m.State == StateEnum.Playing).OrderBy(m => m.LeagueId);

			if (LiveMatches.Count() == 0)
			{
				HasLiveMatch = false;
		    }
			else
			{
				HasLiveMatch = true;
				LiveTeams = AppDelegate.SharedAppDelegate.UoW.TeamRepo.GetTeamsByMatches(LiveMatches);

				LiveIndexes = CreateIndexes(LiveMatches);

			}

		}

		private List<int> CreateIndexes(IEnumerable<Match> matches)
		{
			var indexes = new List<int>();

			indexes.Add(0);

			for (int i = 1; i<matches.Count(); i++)
			{
				if (matches.ElementAt(i - 1).LeagueId != matches.ElementAt(i).LeagueId)
				{
					indexes.Add(i + indexes.Count);
				}
			}

			return indexes;
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

		public override nfloat GetHeightForHeader(UITableView tableView, nint section)
		{
			return 40;
		}

		public override UIView GetViewForHeader(UITableView tableView, nint section)
		{

			UITableViewCell cell = tableView.DequeueReusableCell("HeaderCell");

			cell.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

			if (section == 0)
			{
				(cell.ViewWithTag(200) as UILabel).Text = "Live";
			} 
			else
			{
				(cell.ViewWithTag(200) as UILabel).Text = "Soon";;
			}

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				if (!HasLiveMatch)
				{
					return 40;
				}
				else
				{
					if (LiveIndexes.Contains(indexPath.Row))
					{
						return 40;
					}
					return 120;

				}

			} 
			else
			{
				if (!HasSoonMatch)
				{
					return 40;
				} 
				else
				{
					if (SoonIndexes.Contains(indexPath.Row))
					{
						return 40;
					}
					return 120;

				}
			}
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			if (section == 0)
			{
				if (!HasLiveMatch)
				{
					return 1;
				}

				return LiveMatches.Count() + LiveMatches.DistinctBy(m => m.LeagueId).Count();
			}

			if (!HasSoonMatch)
			{
				return 1;
			}

			return SoonMatches.Count() + SoonMatches.DistinctBy(m => m.LeagueId).Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			if ((indexPath.Section == 0 && !HasLiveMatch) || (indexPath.Section == 1 && !HasSoonMatch))
			{
				var cell = tableView.DequeueReusableCell("NoMatchCell");

				cell.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

				return cell;
			}


			Match match;
			Team homeTeam;
			Team awayTeam;

			if (indexPath.Section == 0)
			{

				if (LiveIndexes.Contains(indexPath.Row))
				{

					return CreateLeagueNameCell(tableView,indexPath, LiveIndexes);
				}
				else
				{

					LiveMatchCell cell = tableView.DequeueReusableCell("LiveMatchCell", indexPath) as LiveMatchCell;

					cell.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

					var color = cell.ViewWithTag(500).BackgroundColor;

					//cell.ViewWithTag(500).BackgroundColor = cell.ViewWithTag(500).BackgroundColor.ColorWithAlpha((float)0.5);r

					match = LiveMatches.ElementAt(indexPath.Row - LiveIndexes.Where(i => i < indexPath.Row).Count());
					homeTeam = LiveTeams.First(t => t.Id == match.HomeTeamId);
					awayTeam = LiveTeams.First(t => t.Id == match.AwayTeamId);

					if (match.State == StateEnum.Playing)
					{
						AnimateView(cell.ViewWithTag(800), true);
					}

					return CreateActualTile(cell, match, homeTeam, awayTeam);

				}


			}
			else
			{

				if (SoonIndexes.Contains(indexPath.Row))
				{
					return CreateLeagueNameCell(tableView, indexPath, SoonIndexes);
					
				}
				else
				{
					LiveMatchCell cell = tableView.DequeueReusableCell("LiveMatchCell", indexPath) as LiveMatchCell;

					cell.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

					match = SoonMatches.ElementAt(indexPath.Row - SoonIndexes.Where(i => i < indexPath.Row).Count());
	 				homeTeam = SoonTeams.First(t => t.Id == match.HomeTeamId);
					awayTeam = SoonTeams.First(t => t.Id == match.AwayTeamId);

					return CreateActualTile(cell, match, homeTeam, awayTeam);

				}

			}

		}

		private UITableViewCell CreateLeagueNameCell(UITableView tableView, Foundation.NSIndexPath indexPath, List<int> indexes)
		{
			var cell = tableView.DequeueReusableCell("LeagueNameCell", indexPath);

			cell.BackgroundColor = UIColor.Clear.FromHex(LightGreen.hex);

			var match = LiveMatches.DistinctBy(m => m.LeagueId).ElementAt(indexes.IndexOf(indexPath.Row));

			var league = AppDelegate.SharedAppDelegate.UoW.LeagueRepo.GetLeagueById(match.LeagueId);

			(cell.ViewWithTag(101) as UIImageView).Image = UIImage.FromBundle(league.Country.ToString().ToLower());
			(cell.ViewWithTag(102) as UILabel).Text = league.Name;

			return cell;
		}

		private LiveMatchCell CreateActualTile(LiveMatchCell cell, Match match, Team homeTeam, Team awayTeam)
		{

			cell.Date.Text = match.Date.ToString();
			cell.Time.LineBreakMode = UILineBreakMode.WordWrap;
			cell.Time.Text = Floorball.UIHelper.GetMatchTime(match.Time, match.State).Replace("\\n", " ");
			cell.HomeTeam.Text = homeTeam.Name;
			cell.HomeScore.Text = match.ScoreH.ToString();
			cell.AwayTeam.Text = awayTeam.Name;
			cell.AwayScore.Text = match.ScoreA.ToString();

			return cell;
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, Foundation.NSObject sender)
		{
			switch (segue.Identifier)
			{

				case "MatchFromActual":

					var vc = segue.DestinationViewController as MatchViewController;

					if (TableView.IndexPathForSelectedRow.Section == 0)
					{
						vc.Match = LiveMatches.ElementAt(TableView.IndexPathForSelectedRow.Row - LiveIndexes.Where(i => i < TableView.IndexPathForSelectedRow.Row).Count());
					} 
					else
					{
						vc.Match = SoonMatches.ElementAt(TableView.IndexPathForSelectedRow.Row - SoonIndexes.Where(i => i < TableView.IndexPathForSelectedRow.Row).Count());
					}

					break;


				default:
					break;
			}
		}

		private void AnimateView(UIView view, bool direction)
		{

			UIView.Animate(1.0, () => MakeAnimation(view, direction), () => AnimateView(view, !direction));

		}

		void MakeAnimation (UIView view, bool direction)
		{
			if (direction)
			{
				view.Layer.BackgroundColor = UIColor.Clear.FromHex(Green.hex).CGColor;
			} 
			else
			{
				view.Layer.BackgroundColor = UIColor.Clear.FromHex(Red.hex).CGColor;

			}
		}

		partial void MenuPressed(UIBarButtonItem sender)
		{
			Root.SideBarController.ToggleMenu();
		}

		void Instance_MatchTimeUpdated(int id)
		{

			var index = LiveMatches.ToList().IndexOf(LiveMatches.First(m => m.Id == id));

			var match = AppDelegate.SharedAppDelegate.UoW.MatchRepo.GetMatchById(id);

			var newTime = Floorball.UIHelper.GetMatchTime(match.Time, match.State);

			
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

