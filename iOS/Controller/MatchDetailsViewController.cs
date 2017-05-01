using System;
using System.Collections.Generic;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class MatchDetailsViewController : UIViewController
	{

		public Match Match { get; set; }

		public League League { get; set; }

		public IEnumerable<Referee> Referees { get; set; }

		public Stadium Stadium { get; set; }

		public MatchDetailsViewController() : base("MatchDetailsViewController", null)
		{
		}

		public MatchDetailsViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitOutlets();

		}

		private void InitOutlets()
		{

			LeagueName.Text = League.Name;

			Country.Image = UIImage.FromBundle(League.Country.ToString().ToLower());

			Date.Text = Match.Date.ToString();
			StadiumName.Text = Stadium.Name;
			StadiumAddress.Text = Stadium.Address;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

