using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloorballAdminiOS.UI.EntitySearch
{
    public class SearchCell
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string RightDetail { get; set; }

    }

    public abstract class EntitySearchPresenter<T> : Presenter<T>
    {
        public List<List<SearchCell>> SearchModel { get; set; }
        public List<List<SearchCell>> FilteredSearchModel { get; set; }

        public List<string> Titles { get; set; }
        public List<string> FilteredTitles { get; set; }

        public abstract Task GetEntitiesFromServer();

        public override void AttachScreen(T screen)
        {
            base.AttachScreen(screen);

			SearchModel = new List<List<SearchCell>>();
            FilteredSearchModel = new List<List<SearchCell>>();
			Titles = new List<string>();
        }

		public void Search(string searchString)
		{

            if (string.IsNullOrWhiteSpace(searchString) || searchString.Length < 2)
            {
                FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
                FilteredTitles = new List<string>(Titles);
                return;
            }

            FilteredSearchModel = new List<List<SearchCell>>();
            FilteredTitles = new List<string>();

            var i = 0;

            foreach (var searchModels in SearchModel)
            {
                var filteredList = new List<SearchCell>();

                foreach (var searchModel in searchModels)
                {
                    if (searchModel.Title.StartsWith(searchString, StringComparison.Ordinal))
                    {
                        filteredList.Add(searchModel);
                    }
                }

                if (filteredList.Count != 0)
                {
                    FilteredSearchModel.Add(filteredList);
                    FilteredTitles.Add(Titles[i]);
                }

            }

        }

    }
}
