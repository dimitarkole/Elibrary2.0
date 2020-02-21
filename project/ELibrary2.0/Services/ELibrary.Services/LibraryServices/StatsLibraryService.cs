using ELibrary.Data;
using ELibrary.Data.Models;
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
    public class StatsLibraryService : IStatsLibraryService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        public StatsLibraryService(
            ApplicationDbContext context,
            IGenreService genreService)
        {
            this.context = context;
            this.genreService = genreService;
        }

        public StatsLibraryViewModel PreparedPage(string userId)
        {
            var model = new StatsLibraryViewModel();
            var returnModel = this.SearchStats(model, userId);
            return returnModel;
        }

        public StatsLibraryViewModel SearchStats(StatsLibraryViewModel model, string userId)
        {
            model.Genres = this.GetGenre();
            var searchBook = model.SearchBook;
            var returnModel = new StatsLibraryViewModel()
            {
                SearchBook = searchBook,
                Genres = model.Genres,
                ChartGettenBookSinceSixМonth = this.ChartGettenBookSinceSixМonth(searchBook, userId),
                ChartAddedBookSinceSixМonth = this.ChartAddedBookSinceSixМonth(searchBook, userId),
            };
            return returnModel;
        }

        private ChartGettenBookSinceSixМonth ChartGettenBookSinceSixМonth(Book searchBook, string userId)
        {
            var chartData = new List<ChartGettenBookSinceSixМonthData>();
            var groups = this.context.GetBooks
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

                  ReturnedOn = gb.ReturnedOn,
                  CreatedOn = gb.CreatedOn,
              })
              .ToList()
              .GroupBy(gb => gb.CreatedOn.Year + " " + gb.CreatedOn.Month)
              .Take(6)
              .ToList();

            string bookName = searchBook.Title;
            string author = searchBook.Author;
            string genreId = searchBook.GenreId;
            string catalogNumber = searchBook.CatalogNumber;
            foreach (var group in groups)
            {
                List<GivenBookViewModel> getBookOfMonth = group.Select(group => group).ToList();
                getBookOfMonth = this.SelectGettenBookOfMonthViewModel(catalogNumber, bookName, author, genreId, getBookOfMonth);
                if (getBookOfMonth.Count > 0)
                {
                    var gb = getBookOfMonth[0];
                    string createdOnMonth = this.MonthToSring(gb.CreatedOn.Month);
                    int getBookCount = getBookOfMonth.Count;
                    int returnedBookCount = getBookOfMonth
                        .Where(gb => gb.ReturnedOn != null)
                        .Count();

                    chartData.Add(new ChartGettenBookSinceSixМonthData(
                       createdOnMonth,
                       getBookCount,
                       returnedBookCount));
                }
            }

            var chartGettenBookSinceSixМonth = new ChartGettenBookSinceSixМonth("Взети книги за последните 6 месеца", chartData);
            return chartGettenBookSinceSixМonth;
        }

        private ChartViewModel ChartAddedBookSinceSixМonth(Book searchBook, string userId)
        {
            var chartData = new List<ChartDataViewModel>();

            var groups = this.context.Books
                .Where(gb =>
                   gb.DeletedOn == null
                   && gb.UserId == userId)
                .ToList()
               .GroupBy(b => b.CreatedOn.Year + " " + b.CreatedOn.Month)
               .Take(6)
               .ToList();

            string title = searchBook.Title;
            string author = searchBook.Author;
            string genreId = searchBook.GenreId;
            string catalogNumber = searchBook.CatalogNumber;
            foreach (var group in groups)
            {
                List<Book> bookOfMonth = group.Select(group => group).ToList();
                bookOfMonth = this.SelectBooksAddedBookOfMonthViewModel(catalogNumber, title, author, genreId, bookOfMonth);
                if (bookOfMonth.Count > 0)
                {
                    var gb = bookOfMonth[0];
                    string createdOnMonth = this.MonthToSring(gb.CreatedOn.Month);

                    chartData.Add(new ChartDataViewModel(
                        createdOnMonth,
                        bookOfMonth.Count));
                }
            }

            var chartGettenBookSinceSixМonth = new ChartViewModel("Добавени книги за последните 6 месеца", chartData);
            return chartGettenBookSinceSixМonth;
        }

        private List<GenreListViewModel> GetGenre()
        {
            var genres = this.genreService.GetAllGenres()
                       .OrderByDescending(x => x.Name).ToList();

            var genre = new GenreListViewModel()
            {
                Id = null,
                Name = "Изберете жанр",
            };

            genres.Add(genre);
            genres.Reverse();
            return genres;
        }

        private List<Book> SelectBooksAddedBookOfMonthViewModel(
          string catalogNumber,
          string title,
          string author,
          string genreId,
          List<Book> books)
        {
            if (catalogNumber != null)
            {
                books = books.Where(b => b.CatalogNumber == null || b.CatalogNumber.Contains(catalogNumber)).ToList();
            }

            if (title != null)
            {
                books = books.Where(b => b.Title.Contains(title)).ToList();
            }

            if (author != null)
            {
                books = books.Where(b => b.Author.Contains(author)).ToList();
            }

            if (genreId != null)
            {
                books = books.Where(b => b.GenreId == genreId).ToList();
            }

            return books;
        }

        private List<GivenBookViewModel> SelectGettenBookOfMonthViewModel(
            string catalogNumber,
            string title,
            string author,
            string genreId,
            List<GivenBookViewModel> books)
        {
            if (catalogNumber != null)
            {
                books = books.Where(b => b.CatalogNumber == null || b.CatalogNumber.Contains(catalogNumber)).ToList();
            }

            if (title != null)
            {
                books = books.Where(b => b.Title.Contains(title)).ToList();
            }

            if (author != null)
            {
                books = books.Where(b => b.Author.Contains(author)).ToList();
            }

            if (genreId != null)
            {
                books = books.Where(b => b.GenreId == genreId).ToList();
            }

            return books;
        }

        private string MonthToSring(int month)
        {
            string result;
            switch (month)
            {
                case 1: result = "Януари"; break;
                case 2: result = "Февруари"; break;
                case 3: result = "Март"; break;
                case 4: result = "Април"; break;
                case 5: result = "Май"; break;
                case 6: result = "Юни"; break;
                case 7: result = "Юли"; break;
                case 8: result = "Август"; break;
                case 9: result = "Септември"; break;
                case 10: result = "Октомври"; break;
                case 11: result = "Ноември"; break;
                case 12: result = "Декември"; break;
                default:
                    result = "null";
                    break;
            }

            return result;
        }
    }
}
