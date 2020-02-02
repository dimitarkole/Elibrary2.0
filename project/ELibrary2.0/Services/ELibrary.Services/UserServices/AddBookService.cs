namespace ELibrary.Services.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.ViewModels.User;

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
            var result = this.CheckDublicateBookAdd(title, author, catalogNumber, userId);
            if (result == null)
            {
                var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
                var genreObj = this.context.Genres.FirstOrDefault(g =>
                     g.Id == genreId
                     && g.DeletedOn == null);

                var newBook = new Book()
                {
                    Title = title,
                    Author = author,
                    GenreId = genreId,
                    Genre = genreObj,
                    UserId = userId,
                    CatalogNumber = catalogNumber,
                    User = user,
                    Currency = model.Currency,
                    Logo = model.Logo,
                    Price = model.Price,
                    Review = model.Review,
                    WhereIsBook = model.WhereIsBook,
                };
                this.context.Books.Add(newBook);
                genreObj.Books.Add(newBook);
                this.context.SaveChanges();
                result = "Successfull added book!";
                this.messageService.AddNotificationAtDB(userId, result);
            }

            return result;
        }

        public List<object> EditBook(AddBookViewModel model, string userId)
        {
            var author = model.Author;
            var title = model.Title;
            var genreId = model.GenreId;
            var bookId = model.BookId;

            var catalogNumber = model.CatalogNumber;
            var checkResult = this.CheckDublicateBookEdit(title, author, catalogNumber, userId, bookId);
            var result = new List<object>();
            result.Add(model);
            if (checkResult == null)
            {
                var genreObj = this.context.Genres.FirstOrDefault(g =>
                  g.Id == genreId
                  && g.DeletedOn == null);
                var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
                model.Genres = this.genreService.GetAllGenres();
                model.BookId = bookId;

                if (book != null)
                {
                    checkResult = this.ChackeInputData(title, author, catalogNumber);
                    if (checkResult == string.Empty)
                    {
                        book.Author = author;
                        book.CatalogNumber = catalogNumber;
                        book.Currency = model.Currency;
                        book.Logo = model.Logo;
                        book.Price = model.Price;
                        book.Review = model.Review;
                        book.WhereIsBook = model.WhereIsBook;
                        book.GenreId = genreId;
                        book.Title = title;
                        book.Genre = genreObj;
                        book.EBookFile = model.EFormatString;
                        genreObj.Books.Add(book);
                        this.context.SaveChanges();
                        checkResult = "Successfull edited book!!";
                        this.messageService.AddNotificationAtDB(userId, checkResult);
                    }

                }
            }
            result.Add(checkResult);
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
                Logo = book.Logo,
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

        private string CheckDublicateBookAdd(string title, string author, string catalogNumber, string userId)
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
                    return "Каталожният номер съвпада с каталожния номер на друга книга!";
                }

                return null;
            }

            return "Книганата същесвува в библиотеката Ви!";
        }

        private string CheckDublicateBookEdit(string title, string author, string catalogNumber, string userId, string bookId)
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
                if (bookCheker2 == null)
                {
                    return "The catalog number is duplicated!";
                }

                return null;
            }

            return "There is such book at your library!";
        }

        private string ChackeInputData(string title, string author, string catalogNumber)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title) || title.Length < 5)
            {
                errors.AppendLine("Името на книгата трябва да има поне 5 символа!");
            }

            if (string.IsNullOrEmpty(author) || string.IsNullOrWhiteSpace(author) || author.Length < 5)
            {
                errors.AppendLine("Името на автора трябва да има поне 5 символа!");
            }

            if (string.IsNullOrEmpty(catalogNumber) || string.IsNullOrWhiteSpace(catalogNumber) || author.Length < 3)
            {
                errors.AppendLine("Каталожният номер трябва да има поне 3 символа!");
            }

            return errors.ToString().Trim();
        }
    }
}
