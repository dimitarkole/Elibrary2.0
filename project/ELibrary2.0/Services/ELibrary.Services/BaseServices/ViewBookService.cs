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

        private INotificationService notificationService;

        public ViewBookService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService notificationService)
        {
            this.context = context;
            this.genreService = genreService;
            this.notificationService = notificationService;
        }

        public Dictionary<string, object> AddReview(ViewBookViewModel model, string bookId, string userId)
        {
            var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);

            BookReview newBookReview = new BookReview()
            {
                Review = model.NewReveiew,
                BookId = bookId,
                UserId = userId,
            };

            this.context.BookReviews.Add(newBookReview);
            this.context.SaveChanges();
            Dictionary<string, object> result = new Dictionary<string, object>();
            var message = $"Успешно дадено мнение за книгата {book.Title}, написана от {book.Author}";
            ///this.notificationService.AddNotificationAtDB(message, userId);
            result.Add("message", message);
            result.Add("model", this.PreparedPage(bookId));
            return result;
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
            var reviewsOfBookViewModel = this.context.BookReviews
                .Where(r => r.BookId == book.Id)
                .OrderBy(r => r.CreatedOn)
                .Select(r => new ReviewsOfBookViewModel()
                {
                    Text = r.Review,
                }).ToList();
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
                ReviewsOfBookViewModels = reviewsOfBookViewModel,
                //Reveiews = reviews,
            };
            return model;
        }

        public Dictionary<string, object> ReserveTheBook(string bookId, string userId)
        {
            var message = this.ReserveTheBookAtDB(bookId, userId);
            var model = this.PreparedPage(bookId);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("model", model);
            result.Add("message", message);
            return result;
        }

        private string ReserveTheBookAtDB(string bookId, string userId)
        {
            var checkGetBook = this.context.GetBooks
                .FirstOrDefault(gb => gb.DeletedOn == null &&
                    gb.BookId == bookId && gb.ReturnedOn == null);
            var result = "Книгата е взета за четене от друг читател!";

            if (checkGetBook == null)
            {
                var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
                var libraryId = book.UserId;
                var library = this.context.Users.FirstOrDefault(u => u.Id == libraryId);
                var reader = this.context.Users.FirstOrDefault(u => u.Id == userId);
                result = $"Читател {reader.FirstName} {reader.LastName} {reader.Email}"
                    + $" иска да запази книгата {book.Title} от {book.Author} с каталожен номер {book.CatalogNumber}"
                    + " за четене";
                this.notificationService.AddNotificationAtDB(libraryId, result);
                result = $"Успешно изпратена заявка за запазване на книгата " +
                    $"{book.Title} от {book.Author} с каталожен номер {book.CatalogNumber}" +
                    $" намираща в {library.FirstName} {library.LastName}";
                this.notificationService.AddNotificationAtDB(userId, result);
            }

            return result;
        }
    }
}
