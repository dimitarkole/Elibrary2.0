namespace ELibrary.Services.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Home;

    public class ViewBookService : IViewBookService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public ViewBookService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

    /*    public ViewBookViewModel AddReview(string bookId)
        {
            throw new NotImplementedException();
        }
        */
        public ViewBookViewModel PreparedPage(string bookId)
        {
            var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
            var genre = this.context.Genres.FirstOrDefault(g => g.Id == book.GenreId);
            var user = this.context.Users.FirstOrDefault(u => u.Id == book.UserId);
            /*var reviews = this.context.BookReviews
                .Where(r => r.BookId == bookId)
                .OrderBy(r => r.CreatedOn)
                .Select(r => new BookReviewsViewModel()
                    {
                        Review = r.Review,
                        UserName = r.User.FirstName + " " + r.User.LastName + " " + r.User.UserName,
                    })
                .ToList();*/
            ViewBookViewModel model = new ViewBookViewModel()
            {
                Author = book.Author,
                BookId = book.Id,
                CatalogNumber = book.CatalogNumber,
                GenreId = book.GenreId,
                GenreName = genre.Name,
                Logo = book.Logo,
                Review = book.Review,
                Title = book.Title,
                UserEmailName = user.Email,
                UserFirstName = user.FirstName,
                UserLastName = user.LastName,
                //Reveiews = reviews,
            };
            return model;
        }
    }
}
