using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using Floorball.REST.RESTManagers;
using FloorballAdminiOS.Interactor;
using FloorballPCL.Exceptions;

namespace FloorballAdminiOS.UI.Entity
{
    public abstract class EntityPresenter<T> : Presenter<T>
    {

        public List<EntityTableViewModel> Model { get; set; }

        public EntityPresenter()
        {
            Model = new List<EntityTableViewModel>();
        }

        public string Url { get; set; }

		//public T Model { get; set; }

		//EntityInteractor<T> entityInteractor;

		public abstract List<EntityTableViewModel> SetTableViewModel();

		public abstract string GetTableHeader(UpdateType crud);

		public abstract Task SetDataFromServer(UpdateType crud);

        public abstract void ClearModel();

        protected abstract Task Save();

        protected abstract void Validate();

        public async Task ValidateAndSave()
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

			ClearModel();

            await Save();
        }



    }
}
