﻿using System;
using System.Collections.Generic;
using Floorball.LocalDB.Tables;
using UIKit;

namespace Floorball.iOS
{
	public partial class TeamsViewController : UITableViewController
	{

		public IEnumerable<Team> Teams { get; set; }

		public TeamsViewController() : base("TeamsViewController", null)
		{
		}

		public TeamsViewController(IntPtr handle) : base(handle)
		{

		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.


			InitProperties();

		}

		private void InitProperties()
		{
			
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

	}
}

