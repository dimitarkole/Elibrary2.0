namespace ELibrary.Services.Data.Tests.LibraryServicesTests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Models;
    using ELibrary.Services.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Services.LibraryServices;
    using ELibrary.Web.ViewModels.Library;
    using Moq;
    using Xunit;

    public class AddBookServiceTests : TransientDbContextProvider
    {
        private readonly Mock<IGenreService> genreServiceMock;
        private readonly Mock<INotificationService> messageServiceMock;
        private readonly Mock<AddBookService> addBookService;
        private readonly Mock<AddGenreService> addGenreServiceMock;

        public AddBookServiceTests()
        {
            this.genreServiceMock = new Mock<IGenreService>();
            this.messageServiceMock = new Mock<INotificationService>();
            this.addGenreServiceMock = new Mock<AddGenreService>(this.context, this.genreServiceMock.Object, this.messageServiceMock.Object);
            this.addBookService = new Mock<AddBookService>(this.context, this.genreServiceMock.Object, this.messageServiceMock.Object);
        }

        [Theory]
        [InlineData("0001", "author", "title", "some review of book", "Успешно добавена книга!")]
        [InlineData("0001", null, "title", "some review of book", "Името на автора трябва да съдържа поне 3 символа!")]
        [InlineData("0001", "author", null, "some review of book", "Името на книгата трябва да съдържа поне 2 символа!")]
        [InlineData("01", "author", "title", "some review of book", "Каталожният номер на книгата трябва да съдържа поне 3 символа!")]
        [InlineData("0001", "author", "title", null, "Описанието на книгата трябва да съдържа поне 10 символа!")]
        public void AddNewBookAtDBTest(string catalogNumber, string author, string title, string review, string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            modelMock.Object.CatalogNumber = catalogNumber;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Review = review;
            modelMock.Object.Title = title;

            // Act
            string result = this.addBookService.Object.AddBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void DublicateBook_ByCatalogNumber_AddingBookAtDB_Test()
        {
            string catalogNumber = "0001";
            string author = "author";
            string title = "title";
            string review = "some review of book";
            string expectedResult = "Каталожният номер доблира каталожния номер на друга книга!";

            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Title = title;
            modelMock.Object.CatalogNumber = catalogNumber;
            modelMock.Object.Review = review;

            this.AddBookAtDb(title, author, catalogNumber, genreId, review);

            // Act
            string result = this.addBookService.Object.AddBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void DublicateBook_ByNameAndAuthor_AddingBookAtDB_Test()
        {
            string catalogNumber = "0001";
            string author = "author";
            string title = "title";
            string review = "some review of book";
            string expectedResult = "Вече има такава книга в библиотеката Ви!";

            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Title = title;
            modelMock.Object.CatalogNumber = catalogNumber + "D";
            modelMock.Object.Review = review;

            this.AddBookAtDb(title, author, catalogNumber, genreId, review);

            // Act
            string result = this.addBookService.Object.AddBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("0001", "author", "title", "some review of book", "Успешно редактирана книга!")]
        [InlineData("0001", null, "title", "some review of book", "Името на автора трябва да съдържа поне 3 символа!")]
        [InlineData("0001", "author", null, "some review of book", "Името на книгата трябва да съдържа поне 2 символа!")]
        [InlineData("01", "author", "title", "some review of book", "Каталожният номер на книгата трябва да съдържа поне 3 символа!")]
        [InlineData("0001", "author", "title", null, "Описанието на книгата трябва да съдържа поне 10 символа!")]
        public void EditBookAtDBTest(string catalogNumber, string author, string title, string review, string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            var bookId = this.AddBookAtDb();
            modelMock.Object.Author = author;
            modelMock.Object.CatalogNumber = catalogNumber;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Review = review;
            modelMock.Object.Title = title;
            modelMock.Object.BookId = bookId;

            // Act
            var result = this.addBookService.Object.EditBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        [Fact]
        public void DublicateBook_ByCatalogNumber_EdingBookAtDB_Test()
        {
            string catalogNumber = "0001";
            string author = "author";
            string title = "title";
            string review = "some review of book";
            string expectedResult = "Каталожният номер доблира каталожния номер на друга книга!";

            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Title = title;
            modelMock.Object.CatalogNumber = catalogNumber;
            modelMock.Object.Review = review;

            this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            var bookId = this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            modelMock.Object.BookId = bookId;

            // Act
            Dictionary<string, object> result = this.addBookService.Object.EditBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        [Fact]
        public void DublicateBook_ByNameAndAuthor_EdingBookAtDB_Test()
        {
            string catalogNumber = "0001";
            string author = "author";
            string title = "title";
            string review = "some review of book";
            string expectedResult = "Вече има такава книга в библиотеката Ви!";

            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Title = title;
            modelMock.Object.CatalogNumber = catalogNumber + "D";
            modelMock.Object.Review = review;

            this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            var bookId = this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            modelMock.Object.BookId = bookId;

            // Act
            Dictionary<string, object> result = this.addBookService.Object.EditBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        [Fact]
        public void NotDublicateBook_ByNameAndAuthor_EdingBookAtDB_Test()
        {
            string catalogNumber = "0001";
            string author = "author";
            string title = "title";
            string review = "some review of book";
            string expectedResult = "Успешно редактирана книга!";

            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Title = title + " t";
            modelMock.Object.CatalogNumber = catalogNumber + "D";
            modelMock.Object.Review = review;

            this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            var bookId = this.AddBookAtDb(title, author, catalogNumber, genreId, review);
            modelMock.Object.BookId = bookId;

            // Act
            Dictionary<string, object> result = this.addBookService.Object.EditBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        private string AddBookAtDb(string title = "unit test book", string author = "author", string catalogNumber = "catalog Number", string genreId = "genreId", string review = "review")
        {
            var book = new Book()
            {
                Title = title,
                Author = author,
                CatalogNumber = catalogNumber,
                GenreId = genreId,
                Review = review,
                UserId = this.unitTestUserId,
            };
            this.context.Books.Add(book);
            this.context.SaveChanges();
            return book.Id;
        }

        private string AddGenreAtDb(string genreName)
        {
            var genre = new Genre()
            {
                Name = genreName,
            };
            this.context.Genres.Add(genre);
            this.context.SaveChanges();
            return genre.Id;
        }

    }
}
