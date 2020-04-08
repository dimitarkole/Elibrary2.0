using ELibrary.Data.Models;
using ELibrary.Services.Admin;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Services.Contracts.LibraryServices;
using ELibrary.Services.LibraryServices;
using ELibrary.Web.ViewModels.Library;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ELibrary.Services.Data.Tests.LibraryServicesTests
{
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
       // [InlineData("New Ganre", "Успешно добавен жанр!")]
        public void AddNewGenreAtDB(string categotyNumbre, string author, string review, string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddBookViewModel>();
            modelMock.Object.Author = author;
            modelMock.Object.CatalogNumber = categotyNumbre;
            var genreId = this.AddGenreAtDb("Genre");
            modelMock.Object.GenreId = genreId;
            modelMock.Object.Review = review;
            modelMock.Object.Review = review;

            // Act
            string result = this.addBookService.Object.AddBook(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
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
