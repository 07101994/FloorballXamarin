using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball;
using Floorball.REST.RESTManagers;
using FloorballAdminiOS.Interactor;

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

		public abstract List<EntityTableViewModel> GetTableViewModel();

		public abstract string GetTableHeader(UpdateType crud);

		public abstract Task SetDataFromServer();

		public abstract Task Save(List<EntityTableViewModel> model);

    }
}
