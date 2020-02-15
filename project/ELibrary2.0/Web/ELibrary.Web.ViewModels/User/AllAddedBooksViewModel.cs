namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.CommonResurces;

    public class AllAddedBooksViewModel
    {
        public AllAddedBooksViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Заглавие а-я");
            this.SortMethods.Add("Заглавие я-а");
            this.SortMethods.Add("Автор а-я");
            this.SortMethods.Add("Автор я-а");
            this.SortMethods.Add("Жанр а-я");
            this.SortMethods.Add("Жанр я-а");
            this.SortMethodId = this.SortMethods[0];

            this.CountBooksOfPageList = new List<int>();
            this.CountBooksOfPageList.Add(10);
            this.CountBooksOfPageList.Add(15);
            this.CountBooksOfPageList.Add(20);

            this.CountBooksOfPage = this.CountBooksOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;
            this.SearchBook = new BookViewModel();
        }

        public BookViewModel SearchBook { get; set; }

        public string SortMethodId { get; set; }

        public string LogoLocation { get; set; }


        public List<string> SortMethods { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        public IEnumerable<BookViewModel> Books { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountBooksOfPage { get; set; }

        public List<int> CountBooksOfPageList { get; set; }
    }
}
