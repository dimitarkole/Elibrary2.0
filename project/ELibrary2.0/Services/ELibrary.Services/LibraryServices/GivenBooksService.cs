﻿using ELibrary.Data;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Services.Contracts.LibraryServices;
using ELibrary.Web.ViewModels.CommonResurces;
using ELibrary.Web.ViewModels.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELibrary.Services.LibraryServices
{
    public class GivenBooksService : IGivenBooksService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService notificationService;


        public GivenBooksService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService notificationService)
        {
            this.context = context;
            this.genreService = genreService;
            this.notificationService = notificationService;
        }

        public GivenBooksViewModel ChangeActivePage(
            GivenBooksViewModel model,
            string userId,
            int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetGevenBooks(model, userId);
        }

        public List<object> DeletingBook(GivenBooksViewModel model, string userId, string givenBookId)
        {
            var givenBook = this.context.GetBooks
             .FirstOrDefault(gb => gb.Id == givenBookId);
            List<object> result = new List<object>();
            result.Add(this.GetGevenBooks(model, userId));

            if (givenBook != null)
            {
                givenBook.DeletedOn = DateTime.UtcNow;
                givenBook.ReturnedOn = DateTime.UtcNow;

                this.context.SaveChanges();

                result.Add("Успешно изтрита дадена книга!");
            }
            else
            {
                result.Add("Няма такава дадена книга!");
            }

            return result;
        }

        public GivenBooksViewModel GetGevenBooks(GivenBooksViewModel model, string userId)
        {
            var firstName = model.SearchGivenBook.FirstName;
            var lastName = model.SearchGivenBook.LastName;
            var email = model.SearchGivenBook.Email;
            var title = model.SearchGivenBook.Title;
            var author = model.SearchGivenBook.Author;
            var genreId = model.SearchGivenBook.GenreId;
            var sortMethodId = model.SortMethodId;
            var countBooksOfPage = model.CountBooksOfPage;
            var currentPage = model.CurrentPage;
            var catalogNumber = model.SearchGivenBook.CatalogNumber;

            var givenBooks = this.context.GetBooks
                .Where(gb =>
                    gb.DeletedOn == null
                    && gb.Book.UserId == userId)
                .Select(gb => new GivenBookViewModel()
                {
                    Author = gb.Book.Author,
                    Id = gb.Id,
                    Title = gb.Book.Title,
                    GenreName = gb.Book.Genre.Name,
                    GenreId = gb.Book.GenreId,
                    CatalogNumber = gb.Book.CatalogNumber,
                    FirstName = gb.User.FirstName,
                    LastName = gb.User.LastName,
                    UserName = gb.User.UserName,
                    Logo = gb.Book.Logo,
                    Email = gb.User.Email,
                    ReturnedOn = gb.ReturnedOn,
                    CreatedOn = gb.CreatedOn,
                });

            givenBooks = this.SelectBooks(
                title,
                author,
                genreId,
                firstName,
                lastName,
                email,
                catalogNumber,
                givenBooks);

            givenBooks = this.SortBooks(sortMethodId, givenBooks);

            var genres = this.genreService.GetAllGenres()
                 .OrderByDescending(x => x.Name).ToList();

            var genre = new GenreListViewModel()
            {
                Id = null,
                Name = "Избери жанр",
            };

            genres.Add(genre);
            genres.Reverse();
            int maxCountPage = givenBooks.Count() / countBooksOfPage;
            if (givenBooks.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewBook = givenBooks.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage);

            var searchGivenBook = new GivenBookViewModel()
            {
                CatalogNumber = catalogNumber,
                Author = author,
                Title = title,
                GenreId = genreId,
            };

            var returnModel = new GivenBooksViewModel()
            {
                Books = viewBook,
                SearchGivenBook = searchGivenBook,
                SortMethodId = sortMethodId,
                Genres = genres,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
                CountBooksOfPage = countBooksOfPage,
            };
            return returnModel;
        }

        public GivenBooksViewModel PreparedPage(string userId)
        {
            var model = new GivenBooksViewModel();
            var returnModel = this.GetGevenBooks(model, userId);
            return returnModel;
        }

        public List<object> ReturningBook(
            GivenBooksViewModel model,
            string userId,
            string givenBookId)
        {
            var givenBook = this.context.GetBooks
                .FirstOrDefault(gb => gb.Id == givenBookId);
            List<object> result = new List<object>();
            result.Add(this.GetGevenBooks(model, userId));

            if (givenBook != null)
            {
                givenBook.ReturnedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
                var message = $"Успеешно върната книга от - {user.FirstName} {user.LastName} - {user.Email}!";

                result.Add(message);
                this.notificationService.AddNotificationAtDB(userId, message);
                this.notificationService.AddNotificationAtDB(givenBook.UserId, message);
            }
            else
            {
                result.Add("Избраната книга за връщане не е намерена!");
            }

            return result;
        }

        public List<object> SendMessageForReturningBook(
           GivenBooksViewModel model,
           string userId,
           string givenBookId)
        {
            var givenBook = this.context.GetBooks
                .FirstOrDefault(gb => gb.Id == givenBookId);
            List<object> result = new List<object>();
            result.Add(this.GetGevenBooks(new GivenBooksViewModel(), userId));

            if (givenBook != null)
            {
                var book = this.context.Books.FirstOrDefault(b => b.Id == givenBook.BookId);

                var message = $"Моля върнете книгата - {book.Title} {book.Author} с каталожен номер {book.CatalogNumber}!";
                this.notificationService.AddNotificationAtDB(givenBook.UserId, message);

                result.Add("Успешно изпратено съобщение за връщане на книга!");
            }
            else
            {
                result.Add("Неуспешно изпратено съобщение за връщане на книга!");
            }

            return result;
        }

        private IQueryable<GivenBookViewModel> SelectBooks(
          string title,
          string author,
          string genreId,
          string firstName,
          string lastName,
          string email,
          string catalogNumber,
          IQueryable<GivenBookViewModel> givenBooks)
        {
            if (catalogNumber != null)
            {
                givenBooks = givenBooks.Where(b => b.CatalogNumber.Contains(catalogNumber));
            }

            if (title != null)
            {
                givenBooks = givenBooks.Where(b => b.Title.Contains(title));
            }

            if (author != null)
            {
                givenBooks = givenBooks.Where(b => b.Author.Contains(author));
            }

            if (genreId != null)
            {
                givenBooks = givenBooks.Where(b => b.GenreId == genreId);
            }

            if (firstName != null)
            {
                givenBooks = givenBooks.Where(b => b.FirstName.Contains(firstName));
            }

            if (lastName != null)
            {
                givenBooks = givenBooks.Where(b => b.LastName.Contains(lastName));
            }

            if (email != null)
            {
                givenBooks = givenBooks.Where(b => b.Email.Contains(email));
            }

            return givenBooks;
        }

        private IQueryable<GivenBookViewModel> SortBooks(
           string sortMethodId,
           IQueryable<GivenBookViewModel> givenBooks)
        {
            if (sortMethodId == "Заглавие а-я")
            {
                givenBooks = givenBooks.OrderByDescending(b => b.Title);
            }
            else if (sortMethodId == "Автор а-я")
            {
                givenBooks = givenBooks.OrderBy(b => b.Author);
            }
            else if (sortMethodId == "Автор я-а")
            {
                givenBooks = givenBooks.OrderByDescending(b => b.Author);
            }
            else if (sortMethodId == "Жанр а-я")
            {
                givenBooks = givenBooks.OrderBy(b => b.GenreName);
            }
            else if (sortMethodId == "Жанр я-а")
            {
                givenBooks = givenBooks.OrderByDescending(b => b.GenreName);
            }
            else
            {
                givenBooks = givenBooks.OrderBy(b => b.Title);
            }

            return givenBooks;
        }
    }
}
