namespace ELibrary.Services.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Web.ViewModels.Library;

    public class AddBookService : IAddBookService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AddBookService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public string AddBook(AddBookViewModel model, string userId)
        {
            var author = model.Author;
            var title = model.Title;
            var genreId = model.GenreId;
            var catalogNumber = model.CatalogNumber;

            var message = this.IsHasNullData(model);
            if (string.IsNullOrEmpty(message))
            {
                message = this.CheckDublicateBookAdd(title, author, catalogNumber, userId);
                if (message == null)
                {
                    var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
                    var genreObj = this.context.Genres.FirstOrDefault(g =>
                         g.Id == genreId
                         && g.DeletedOn == null);

                    var newBook = this.CreateNewBook(model, user, userId, genreObj);

                    this.context.Books.Add(newBook);
                    genreObj.Books.Add(newBook);
                    this.context.SaveChanges();
                    message = "Успешно добавена книга!";
                    this.messageService.AddNotificationAtDB(userId, message);
                }
            }

            return message;
        }

        public Dictionary<string, object> EditBook(AddBookViewModel model, string userId)
        {

            var author = model.Author;
            var title = model.Title;
            var genreId = model.GenreId;
            var bookId = model.BookId;

            var catalogNumber = model.CatalogNumber;
            var message = this.IsHasNullData(model);
            var result = new Dictionary<string, object>();
            result.Add("model", model);
            if (string.IsNullOrEmpty(message))
            {
                message = this.CheckDublicateBookEdit(title, author, catalogNumber, userId, bookId);

                if (message == null)
                {
                    var genreObj = this.context.Genres.FirstOrDefault(g =>
                      g.Id == genreId
                      && g.DeletedOn == null);
                    var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
                    model.Genres = this.genreService.GetAllGenres();
                    if (book != null)
                    {
                        book.Author = author;
                        book.CatalogNumber = catalogNumber;
                        book.Logo = model.LogoLocation;
                        book.Price = model.Price;
                        book.Review = model.Review;
                        book.WhereIsBook = model.WhereIsBook;
                        book.GenreId = genreId;
                        book.Title = title;
                        book.Genre = genreObj;
                        genreObj.Books.Add(book);
                        this.context.SaveChanges();
                        message = "Успешно редактирана книга!";
                        this.messageService.AddNotificationAtDB(userId, message);
                    }
                }

            }
            result.Add("message", message);
            return result;
        }


        public AddBookViewModel GetBookDataById(string bookId)
        {
            var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
            var genres = this.genreService.GetAllGenres();
            var model = new AddBookViewModel()
            {
                BookId = bookId,
                Author = book.Author,
                Title = book.Title,
                GenreId = book.GenreId,
                Genres = genres,
                CatalogNumber = book.CatalogNumber,
                LogoLocation = book.Logo,
                Price = book.Price,
                Review = book.Review,
                WhereIsBook = book.WhereIsBook,
            };
            return model;
        }

        public AddBookViewModel PreparedAddBookPage()
        {
            var genres = this.genreService.GetAllGenres();

            var model = new AddBookViewModel()
            {
                Genres = genres,
            };
            return model;
        }


        internal string IsHasNullData(AddBookViewModel model)
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrWhiteSpace(model.Title) || model.Title.Length < 3)
            {
                result.Append("Името на книгата трябва да съдържа поне 2 символа!");
            }

            if (string.IsNullOrEmpty(model.Author) || string.IsNullOrWhiteSpace(model.Author) || model.Author.Length < 4)
            {
                result.Append("Името на автора трябва да съдържа поне 3 символа!");
            }

            if (string.IsNullOrEmpty(model.CatalogNumber) || string.IsNullOrWhiteSpace(model.CatalogNumber) || model.CatalogNumber.Length < 4)
            {
                result.Append("Каталожният номер на книгата трябва да съдържа поне 3 символа!");
            }

            if (string.IsNullOrEmpty(model.Review) || string.IsNullOrWhiteSpace(model.Review) || model.Review.Length < 11)
            {
                result.Append("Описанието на книгата трябва да съдържа поне 10 символа!");
            }

            return result.ToString().Trim();
        }

        internal string CheckDublicateBookAdd(string title, string author, string catalogNumber, string userId)
        {
            var bookCheker1 = this.context.Books.Where(b =>
                   b.Title == title
                   && b.Author == author
                   && b.UserId == userId
                   && b.DeletedOn == null
                   && b.CatalogNumber.Equals(catalogNumber) == true)
               .ToList();
            if (bookCheker1.Count == 0)
            {
                var bookCheker2 = this.context.Books.FirstOrDefault(b =>
                  b.CatalogNumber == catalogNumber
                  && b.UserId == userId
                  && b.DeletedOn == null);
                if (bookCheker2 != null)
                {
                    return "Каталожният номер доблира каталожния номер на друга книга!";
                }

                return null;
            }

            return "Вече има такава книга в библиотеката Ви!";
        }

        internal string CheckDublicateBookEdit(string title, string author, string catalogNumber, string userId, string bookId)
        {
            var bookCheker1 = this.context.Books.Where(b =>
                       b.Id != bookId
                       && b.Title == title
                       && b.Author == author
                       && b.UserId == userId
                       && b.CatalogNumber.Equals(catalogNumber) == true
                       && b.DeletedOn == null)
                   .ToList();
            if (bookCheker1.Count == 0)
            {
                var bookCheker2 = this.context.Books.FirstOrDefault(b =>
                       b.Id != bookId
                       && b.CatalogNumber == catalogNumber
                       && b.DeletedOn == null);
                if (bookCheker2 != null)
                {
                    return "Каталожният номер доблира каталожния номер на друга книга!";
                }

                return null;
            }

            return "Вече има такава книга в библиотеката Ви!";
        }

        internal Book CreateNewBook(AddBookViewModel model, ApplicationUser user, string userId, Genre genreObj)
        {
            var newBook = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                GenreId = model.GenreId,
                Genre = genreObj,
                UserId = userId,
                CatalogNumber = model.CatalogNumber,
                User = user,
                Logo = model.LogoLocation,
                Price = model.Price,
                Review = model.Review,
                WhereIsBook = model.WhereIsBook,
            };
            return newBook;
        }
    }
}
