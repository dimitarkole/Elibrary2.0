namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BookViewModel
    {
        public BookViewModel()
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
    }
}
