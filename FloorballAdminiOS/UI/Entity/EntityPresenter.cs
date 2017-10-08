using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using Floorball.REST.RESTManagers;
using FloorballAdminiOS.Interactor;
using FloorballPCL;
using FloorballPCL.Exceptions;

namespace FloorballAdminiOS.UI.Entity
{
    public abstract class EntityPresenter<T> : Presenter<T>
    {

        public int EntityId { get; set; }

        public List<EntityTableViewModel> Model { get; set; }

        public EntityPresenter(ITextManager textManager) : base(textManager)
        {
            Model = new List<EntityTableViewModel>();
        }

        public string Url { get; set; }

		public abstract List<EntityTableViewModel> SetTableViewModel();

		public abstract string GetTableHeader(UpdateType crud);

		public abstract Task SetDataFromServer(UpdateType crud);

        public abstract void ClearModel();

        protected abstract Task Save(UpdateType crud);

        protected abstract void Validate();

        public async Task ValidateAndSave(UpdateType crud)
        {
			try
			{
				Validate();
			}
			catch (ValidationException)
			{
				throw;
			}
			catch (Exception ex)
			{
                throw new ValidationException("Validation error", "Some values not correct", ex);
			}

            await Save(crud);

            ClearModel();
        }



    }
}
