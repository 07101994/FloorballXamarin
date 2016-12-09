using System;
using System.Collections.Generic;
using System.Linq;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchEventsViewController : UITableViewController
	{

		public IEnumerable<Event> Events { get; set; }

		public Team HomeTeam { get; set; }

		public Team AwayTeam { get; set; }

		public MatchEventsViewController() : base("MatchEventsViewController", null)
		{
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

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableView, nint section)
		{
			return Events.Count();
		}

		public override UITableViewCell GetCell(UITableView tableView, Foundation.NSIndexPath indexPath)
		{
			UITableViewCell cell;

			var e = Events.ElementAt(indexPath.Row);

			if (e.TeamId == HomeTeam.Id)
			{
				cell = tableView.DequeueReusableCell("HomeEventCell", indexPath);
				(cell.ViewWithTag(1) as UILabel).Text = HomeTeam.Players.First(p => p.RegNum == e.PlayerId).ShortName;
			}
			else
			{ 
				cell = tableView.DequeueReusableCell("AwayEventCell", indexPath);
				(cell.ViewWithTag(1) as UILabel).Text = AwayTeam.Players.First(p => p.RegNum == e.PlayerId).ShortName;
			}

			(cell.ViewWithTag(0) as UILabel).Text = e.Time.Split(':')[1] + ":" + e.Time.Split(':')[2];

			cell = SetImage(cell, e);

			return cell;
		}

		private UITableViewCell SetImage(UITableViewCell cell, Event e)
		{
			if (e.Type == "P2")
			{
				(cell.ViewWithTag(2) as UIImageView).Image = new UIImage("2minutes");
			}
			else
			{
				if (e.Type == "P5")
				{
					(cell.ViewWithTag(2) as UIImageView).Image = new UIImage("5minutes");
				}
				else
				{
					if (e.Type == "G")
					{
						(cell.ViewWithTag(2) as UIImageView).Image = new UIImage("goal");
					}
					else
					{
						if (e.Type == "P10")
						{
							(cell.ViewWithTag(2) as UIImageView).Image = new UIImage("10minutes");
						}
						else
						{
							if (e.Type == "PV")
							{
								(cell.ViewWithTag(2) as UIImageView).Image = new UIImage("redcard");
							}
						}
					}
				}
			}

			return cell;
		}
}
}

