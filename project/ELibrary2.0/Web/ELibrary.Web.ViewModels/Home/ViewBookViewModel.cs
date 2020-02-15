namespace ELibrary.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ViewBookViewModel
    {
        public ViewBookViewModel()
        {
            this.Title = null;
            this.Author = null;
            this.GenreName = null;
            this.GenreId = null;
            this.BookId = null;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string GenreName { get; set; }

        public string CatalogNumber { get; set; }

        public string Logo { get; set; }

        public string GenreId { get; set; }

        public string BookId { get; set; }

        public string Review { get; set; }
    }
}
