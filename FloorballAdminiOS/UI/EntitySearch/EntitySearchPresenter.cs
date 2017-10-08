using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FloorballAdminiOS.UI.EntitySearch
{
    public class SearchCell
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string RightDetail { get; set; }

    }

    public class SearchTitle
    {
        public string MainTitle { get; set; }
        public string Subtitle { get; set; }
    }

    public abstract class EntitySearchPresenter<T> : Presenter<T>
    {
        public List<List<SearchCell>> SearchModel { get; set; }
        public List<List<SearchCell>> FilteredSearchModel { get; set; }

        public List<SearchTitle> Titles { get; set; }
        public List<SearchTitle> FilteredTitles { get; set; }

        public abstract Task GetEntitiesFromServer();

        public override void AttachScreen(T screen)
        {
            base.AttachScreen(screen);

			SearchModel = new List<List<SearchCell>>();
            FilteredSearchModel = SearchModel;
			Titles = new List<SearchTitle>();
            FilteredTitles = Titles;
        }

		public void Search(string searchString)
		{

            if (string.IsNullOrWhiteSpace(searchString) || searchString.Length < 2)
            {
                FilteredSearchModel = new List<List<SearchCell>>(SearchModel);
                FilteredTitles = new List<SearchTitle>(Titles);
                return;
            }

            FilteredSearchModel = new List<List<SearchCell>>();
            FilteredTitles = new List<SearchTitle>();

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
