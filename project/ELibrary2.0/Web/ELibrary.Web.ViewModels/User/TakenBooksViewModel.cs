namespace ELibrary.Web.ViewModels.User
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TakenBooksViewModel
    {
        public TakenBooksViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Title a-z");
            this.SortMethods.Add("Title z-a");
            this.SortMethods.Add("Author a-z");
            this.SortMethods.Add("Author z-a");
            this.SortMethods.Add("Genre a-z");
            this.SortMethods.Add("Genre z-a");
            this.SortMethodId = this.SortMethods[0];

            this.CountBooksOfPageList = new List<int>();
            this.CountBooksOfPageList.Add(10);
            this.CountBooksOfPageList.Add(15);
            this.CountBooksOfPageList.Add(20);

            this.CountBooksOfPage = this.CountBooksOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;

            this.SearchTakenBook = new TakenBookViewModel();
        }

        public TakenBookViewModel SearchTakenBook { get; set; }

        public string SortMethodId { get; set; }

        public List<string> SortMethods { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        public IEnumerable<TakenBookViewModel> Books { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountBooksOfPage { get; set; }

        public List<int> CountBooksOfPageList { get; set; }
    }
}
