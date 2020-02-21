namespace ELibrary.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllLibrariesViewModel
    {
        public AllLibrariesViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Email на билиотеката а-я");
            this.SortMethods.Add("Email на билиотеката я-а");
            this.SortMethodId = this.SortMethods[0];

            this.CountLibraiesOfPageList = new List<int>();
            this.CountLibraiesOfPageList.Add(10);
            this.CountLibraiesOfPageList.Add(15);
            this.CountLibraiesOfPageList.Add(20);

            this.CountLibraiesOfPage = this.CountLibraiesOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;
            this.SearchLibrary = new LibraryViewModel();
        }

        public LibraryViewModel SearchLibrary { get; set; }

        public string SortMethodId { get; set; }

        public List<string> SortMethods { get; set; }

        public IEnumerable<LibraryViewModel> Libraries { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountLibraiesOfPage { get; set; }

        public List<int> CountLibraiesOfPageList { get; set; }
    }
}
