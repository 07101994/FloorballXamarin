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
    public class NavigationModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int Id { get; set; }

    }

    public abstract class EntityPresenter<T> : Presenter<T>
    {

        public int EntityId { get; set; }

        public List<List<EntityTableViewModel>> Model { get; set; }

		public List<List<NavigationModel>> NavigationModels { get; set; }
		public List<List<NavigationModel>> FilteredNavigationModels { get; set; }

        public EntityPresenter(ITextManager textManager) : base(textManager)
        {
            Model = new List<List<EntityTableViewModel>>();
            NavigationModels = new List<List<NavigationModel>>();
            FilteredNavigationModels = new List<List<NavigationModel>>();
        }

        public string Url { get; set; }

		public abstract List<List<EntityTableViewModel>> SetTableViewModel();

		public abstract string GetTableHeader(UpdateType crud);

		public abstract Task SetDataFromServer(UpdateType crud);

        public abstract void ClearModel();

        protected abstract Task Save(UpdateType crud);

        protected abstract void Validate();

        public virtual string GetNavigationTextSelected(int rowNumber) { return ""; }
        public virtual string GetNavigationTextNonSelected(int rowNumber) { return ""; }

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

		public void Search(string searchString)
		{

			if (string.IsNullOrWhiteSpace(searchString) || searchString.Length < 2)
			{
				FilteredNavigationModels = new List<List<NavigationModel>>();
                FilteredNavigationModels.Add(new List<NavigationModel>(NavigationModels[0]));
                FilteredNavigationModels.Add(new List<NavigationModel>(NavigationModels[1]));
				return;
			}

			FilteredNavigationModels = new List<List<NavigationModel>>();


            foreach (var navigationModels in NavigationModels)
			{
				var filteredList = new List<NavigationModel>();

                foreach (var navigationModel in navigationModels)
				{
					if (navigationModel.Title.StartsWith(searchString, StringComparison.Ordinal))
					{
						filteredList.Add(navigationModel);
					}
				}

				FilteredNavigationModels.Add(filteredList);

			}

		}

    }
}
