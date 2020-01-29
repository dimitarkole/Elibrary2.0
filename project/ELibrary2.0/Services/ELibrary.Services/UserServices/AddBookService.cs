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
            var result = this.CheckDublicateBook(title, author, catalogNumber, userId);
            if (result == null)
            {
                var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
                var genreObj = this.context.Genres.FirstOrDefault(g =>
                     g.Id == genreId
                     && g.DeletedOn == null);

                var newBook = new Book()
                {
                    Tittle = title,
                    Author = author,
                    GenreId = genreId,
                    Genre = genreObj,
                    UserId = userId,
                    CatalogNumber = catalogNumber,
                    Commentar = model.Commentar,
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
                result = "Успешно добавена книганата!";
                this.messageService.AddNotificationAtDB(userId, result);

            }

            return result;
        }

        public List<object> EditBook(AddBookViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public AddBookViewModel GetBookDataById(string bookId)
        {
            throw new NotImplementedException();
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

        private string CheckDublicateBook(string title, string author, string catalogNumber, string userId)
        {
            var bookCheker1 = this.context.Books.Where(b =>
                   b.Tittle == title
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
                if (bookCheker2 == null)
                {
                    return "Каталожният номер съвпада с каталожния номер на друга книга!";
                }

                return null;
            }

            return "Книганата същесвува в библиотеката Ви!";
        }

        private string ChackeInputData(string bookName, string author, string catalogNumber)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(bookName) || string.IsNullOrWhiteSpace(bookName) || bookName.Length < 5)
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
