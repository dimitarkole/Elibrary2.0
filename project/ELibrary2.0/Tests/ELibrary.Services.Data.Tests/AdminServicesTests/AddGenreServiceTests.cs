namespace ELibrary.Services.Data.Tests.AdminServicesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using ELibrary.Common;
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Admin;
    using ELibrary.Services.CommonResurcesServices;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Moq.Protected;
    using Xunit;

    public class AddGenreServiceTests : TransientDbContextProvider
    {
        private readonly Mock<IGenreService> genreServiceMock;
        private readonly Mock<INotificationService> messageServiceMock;
        private readonly Mock<AddGenreService> addGenreServiceMock;

        public AddGenreServiceTests()
        {
            this.genreServiceMock = new Mock<IGenreService>();
            this.messageServiceMock = new Mock<INotificationService>();
            this.addGenreServiceMock = new Mock<AddGenreService>(this.context, this.genreServiceMock.Object, this.messageServiceMock.Object);
        }

        [Theory]
        [InlineData("New Ganre", "Успешно добавен жанр!")]
        [InlineData(null, "Името на жанра трябва да съдържа поне 3 символа!")]
        [InlineData("a", "Името на жанра трябва да съдържа поне 3 символа!")]
        [InlineData("abc", "Името на жанра трябва да съдържа поне 3 символа!")]
        public void AddNewGenreAtDB(string genreName, string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddGenreViewModel>();
            modelMock.Object.Name = genreName;

            // Act
            string result = this.addGenreServiceMock.Object.AddGenre(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("New Ganre", "New Ganre Name", "Успешно редактиран жанр!")]
        [InlineData("New Ganre", null, "Името на жанра трябва да съдържа поне 3 символа!")]
        [InlineData("New Ganre", "a", "Името на жанра трябва да съдържа поне 3 символа!")]
        [InlineData("New Ganre", "abc", "Името на жанра трябва да съдържа поне 3 символа!")]

        public void EditGenreAtDB(string genreName, string newGenreName, string expectedResult)
        {
            // Arrange
            var id = this.AddGenreAtContextReturnId(genreName);
            var modelMock = new Mock<AddGenreViewModel>();
            modelMock.Object.Id = id;
            modelMock.Object.Name = genreName;

            modelMock.Object.Name = newGenreName;

            // Act
            var result = this.addGenreServiceMock.Object.EditGenre(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        [Fact]
        public void DublicateAddGenreAtDB()
        {
            // Arrange
            var modelMock = new Mock<AddGenreViewModel>();
            modelMock.Object.Name = "New Genre";
            this.addGenreServiceMock.Object.AddGenre(modelMock.Object, this.unitTestUserId);

            // act
            string result = this.addGenreServiceMock.Object.AddGenre(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Жанра се дублира с друг!", result);
        }

        [Fact]
        public void DublicateEditGenreAtDB()
        {
            // Arrange
            this.AddGenreAtContextReturnId("New Genre");
            var id = this.AddGenreAtContextReturnId("Editing Genre");
            var modelMock = new Mock<AddGenreViewModel>();
            modelMock.Object.Name = "New Genre";
            modelMock.Object.Id = id;

            // Act
            var result = this.addGenreServiceMock.Object.EditGenre(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Жанра се дублира с друг!", result["message"]);
        }

        private string AddGenreAtContextReturnId(string genreName)
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
