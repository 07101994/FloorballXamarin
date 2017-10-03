using System;
using System.Threading.Tasks;
using Floorball.REST.RESTManagers;
using FloorballAdminiOS.Interactor;

namespace FloorballAdminiOS.UI.Entity
{
    public class EntityPresenter<T> : Presenter<EntityScreen>
    {

        public T Model { get; set; }

        EntityInteractor<T> entityInteractor;

        public String Url { get; set; }

        public EntityPresenter(string url)
        {
            Url = url;
        }

		public override void AttachScreen(EntityScreen screen)
		{
			base.AttachScreen(screen);

			entityInteractor = new EntityInteractor<T>();

		}

		public override void DetachScreen()
		{
			base.DetachScreen();

		}

        public async Task AddEntity(string errorMsg)
        {
            await entityInteractor.AddEntity(Url, errorMsg, Model);
        }

        public async Task UpdateEntity()
        {
            
        }

        public async Task GetEntity(string errorMsg, string id)
        {
            await entityInteractor.GetEntityById(Url + "/{id}", errorMsg, id);
        }


    }
}
