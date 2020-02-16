namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllAddedGenresViewModel
    {
        public AllAddedGenresViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Име а-я");
            this.SortMethods.Add("Име я-а");
            this.SortMethodId = this.SortMethods[0];

            this.CountGenresOfPageList = new List<int>();
            this.CountGenresOfPageList.Add(10);
            this.CountGenresOfPageList.Add(15);
            this.CountGenresOfPageList.Add(20);

            this.CountGenresOfPage = this.CountGenresOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;
            this.SearchBook = new AddedGenreViewModel();
            this.Genres = new List<AddedGenreViewModel>();
        }

        public AddedGenreViewModel SearchBook { get; set; }

        public string SortMethodId { get; set; }

        public List<string> SortMethods { get; set; }

        public IEnumerable<AddedGenreViewModel> Genres { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountGenresOfPage { get; set; }

        public List<int> CountGenresOfPageList { get; set; }
    }
}
