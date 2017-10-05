﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using FloorballAdminiOS.Interactor.Entity;
using FloorballServer.Models.Floorball;

namespace FloorballAdminiOS.UI.Entity.Player
{
    public class PlayerPresenter : EntityPresenter<EntityScreen>
    {
        PlayerInteractor playerInteractor;

        PlayerModel player;

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			playerInteractor = new PlayerInteractor();
			Url = "/api/floorball/players";
		}

        public override void ClearModel()
        {
            Model.Clear();
        }

        public override void DetachScreen()
		{
			base.DetachScreen();
		}

        public override string GetTableHeader(UpdateType crud)
        {
            return crud == UpdateType.Create ? "Add Player" : "Update Player";
        }

        public override List<EntityTableViewModel> SetTableViewModel()
        {
			
            Model.Add(new EntityTableViewModel { Label = "First Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = player == null ? "" : player.FirstName });
            Model.Add(new EntityTableViewModel { Label = "Last Name", CellType = TableViewCellType.TextField, IsVisible = true, Value = player == null ? "" : player.SecondName });
            Model.Add(new EntityTableViewModel { Label = "Birth Date", CellType = TableViewCellType.Label, IsVisible = true, Value = player == null ? "" : player.BirthDate.ToString() });
			Model.Add(new EntityTableViewModel { CellType = TableViewCellType.DatePicker, IsVisible = false, Value = "" });

			return Model;
        }

        protected async override Task Save()
        {

            await playerInteractor.AddEntity(Url, "Error during adding player!", player);

			Model.Clear();
        }

        public async override Task SetDataFromServer(UpdateType crud)
        {
            List<Task> tasks = new List<Task>();

            Task<PlayerModel> playerTask = null;

            if (crud == UpdateType.Update)
            {
                playerTask = playerInteractor.GetEntityById(Url, "Error during getting player", "1");    
                tasks.Add(playerTask);
            }

            await Task.WhenAll(tasks);

            if (crud == UpdateType.Update)
            {
                player = playerTask.Result;
            }

            SetTableViewModel();

        }

        protected override void Validate()
        {
			player = new PlayerModel()
			{

			};
        }
    }
}
