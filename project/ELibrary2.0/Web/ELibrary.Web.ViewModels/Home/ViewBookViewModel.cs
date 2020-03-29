namespace ELibrary.Web.ViewModels.Home
{
    using ELibrary.Web.ViewModels.User;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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
            this.ReviewsOfBookViewModels = new List<ReviewsOfBookViewModel>();
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string GenreName { get; set; }

        public string CatalogNumber { get; set; }

        public string Logo { get; set; }

        public string GenreId { get; set; }

        public string BookId { get; set; }

        public string Review { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserEmailName { get; set; }

        public string UploadUserNames { get; set; }

        public string UploadUserId { get; set; }

        public List<ReviewsOfBookViewModel> ReviewsOfBookViewModels { get; set; }

        [Display(Name = "Review Text")]
        [StringLength(5000, MinimumLength = 5)]
        public string NewReveiew { get; set; }

    }
}
