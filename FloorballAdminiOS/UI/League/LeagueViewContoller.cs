using System;
using System.Collections.Generic;
using Floorball;
using FloorballAdminiOS.UI.Entity;
using FloorballServer.Models.Floorball;
using Foundation;
using UIKit;

namespace FloorballAdminiOS.UI.League
{
    public partial class LeagueViewContoller : EntityViewController
    {

        public EntityPresenter<LeagueModel> EntityPresenter { get; set; }

        public LeagueViewContoller() : base("LeagueViewContoller", null)
        {
        }

		public LeagueViewContoller(IntPtr handle) : base(handle)
        {
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            AddTableViewHeader(Crud == UpdateType.Create  ? "Add League" : "Update League");

            EntityPresenter = new EntityPresenter<LeagueModel>();

            Model.Add(new EntityTableViewModel { Label = "League Name", CellType = TableViewCellType.TextField, IsVisible = true, Model = "" });
            Model.Add(new EntityTableViewModel { Label = "Gendre", CellType = TableViewCellType.SegmenControl, IsVisible = true, Model = new SegmentControlModel{Segments = new List<Tuple<string, string>>{ new Tuple<string, string>("men","men"), new Tuple<string, string>("women", "women") }} });
            Model.Add(new EntityTableViewModel { Label = "Year", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
            Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = UIHelper.GetNumbers(2012,2018)});
			Model.Add(new EntityTableViewModel { Label = "Year2", CellType = TableViewCellType.Label, IsVisible = true, Model = "" });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.Picker, IsVisible = false, Model = UIHelper.GetNumbers(2023, 2030) });

            TableView.ReloadData();

        }


        void PickerSelected(string val)
        {
            //TODO            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        protected override void Save()
        {
            
        }
    }
}

