namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Models;
    using ELibrary.Web.ViewModels.CommonResurces;
    using ELibrary.Web.ViewModels.Library;

    public class StatsUserViewModel
    {
        public StatsUserViewModel()
        {
            this.SearchBook = new Book();
        }

        public Book SearchBook { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        public ChartGettenBookSinceSixМonth ChartGettenBookSinceSixМonth { get; set; }

        public ChartViewModel ChartGenres { get; set; }

    }
}
