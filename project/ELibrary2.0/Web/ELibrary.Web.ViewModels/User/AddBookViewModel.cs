namespace ELibrary.Web.ViewModels.User
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddBookViewModel
    {
        public string BookId { get; set; }

        [Required(ErrorMessage = "Моля въведете име на книга!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Моля въведете име на автора!")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Моля въведете име на автора!")]
        public string CatalogNumber { get; set; }

        [Required(ErrorMessage = "Моля изберете жанр!")]
        public string GenreId { get; set; }

        public string Commentar { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string Logo { get; set; }

        public string Review { get; set; }
        
        public string WhereIsBook { get; set; }

    }
}
