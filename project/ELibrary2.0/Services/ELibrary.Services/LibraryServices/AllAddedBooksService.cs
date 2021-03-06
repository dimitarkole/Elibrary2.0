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
    public class AllAddedBooksService : IAllAddedBooksService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AllAddedBooksService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public AllAddedBooksViewModel ChangeActivePage(AllAddedBooksViewModel model, string userId, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetBooks(model, userId);
        }

        public AllAddedBooksViewModel DeleteBook(string userId, AllAddedBooksViewModel model, string bookId)
        {
            var deleteBook = this.context.Books.FirstOrDefault(b => b.Id == bookId);
            if (deleteBook != null)
            {
                deleteBook.DeletedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                string result = "Успешно изтрита книга!";
                this.messageService.AddNotificationAtDB(userId, result);
            }

            var returnModel = this.GetBooks(model, userId);
            return returnModel;
        }

        public AllAddedBooksViewModel GetBooks(AllAddedBooksViewModel model, string userId)
        {
            var bookCatalogNumber = model.SearchBook.CatalogNumber;
            var title = model.SearchBook.Title;
            var author = model.SearchBook.Author;
            var genreId = model.SearchBook.GenreId;
            var sortMethodId = model.SortMethodId;
            var countBooksOfPage = model.CountBooksOfPage;
            var currentPage = model.CurrentPage;

            var books = this.context.Books.Where(b =>
              b.DeletedOn == null
              && b.UserId == userId)
              .Select(b => new BookViewModel()
              {
                  Author = b.Author,
                  BookId = b.Id,
                  Title = b.Title,
                  GenreName = b.Genre.Name,
                  GenreId = b.GenreId,
                  CatalogNumber = b.CatalogNumber,
                  Logo = b.Logo,
                  Review = b.Review,
              });

            books = this.SelectBooks(bookCatalogNumber, title, author, genreId, books);

            books = this.SortBooks(sortMethodId, books);

            var genres = this.genreService.GetAllGenres()
                 .OrderByDescending(x => x.Name).ToList();

            var genre = new GenreListViewModel()
            {
                Id = null,
                Name = "Избери жанр",
            };

            genres.Add(genre);
            genres.Reverse();
            int maxCountPage = books.Count() / countBooksOfPage;
            if (books.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewBook = books.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage);
            var searchBook = new BookViewModel()
            {
                Author = author,
                Title = title,
                GenreId = genreId,
            };

            var returnModel = new AllAddedBooksViewModel()
            {
                Books = viewBook,
                SearchBook = searchBook,
                SortMethodId = sortMethodId,
                Genres = genres,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
                CountBooksOfPage = countBooksOfPage,
            };
            return returnModel;
        }

        public AllAddedBooksViewModel PreparedPage(string userId)
        {
            var model = new AllAddedBooksViewModel();
            var returnModel = this.GetBooks(model, userId);
            return returnModel;
        }

        private IQueryable<BookViewModel> SortBooks(
            string sortMethodId,
            IQueryable<BookViewModel> books)
        {
            if (sortMethodId == "Заглавие а-я")
            {
                books = books.OrderByDescending(b => b.Title);
            }
            else if (sortMethodId == "Автор а-я")
            {
                books = books.OrderBy(b => b.Author);
            }
            else if (sortMethodId == "Автор я-а")
            {
                books = books.OrderByDescending(b => b.Author);
            }
            else if (sortMethodId == "Жанр а-я")
            {
                books = books.OrderBy(b => b.GenreName);
            }
            else if (sortMethodId == "Жанр я-а")
            {
                books = books.OrderByDescending(b => b.GenreName);
            }
            else
            {
                books = books.OrderBy(b => b.Title);
            }

            return books;
        }

        private IQueryable<BookViewModel> SelectBooks(
          string bookCatalogNumber,
          string title,
          string author,
          string genreId,
          IQueryable<BookViewModel> books)
        {
            if (bookCatalogNumber != null)
            {
                books = books.Where(b => b.CatalogNumber.Contains(bookCatalogNumber));
            }

            if (title != null)
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            if (author != null)
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            if (genreId != null)
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            return books;
        }
    }
}
