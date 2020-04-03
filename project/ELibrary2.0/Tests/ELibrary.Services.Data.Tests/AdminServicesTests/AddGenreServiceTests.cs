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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ELibrary.Services.Data.Tests.AdminServicesTests
{

    public class AddGenreServiceTests
    {
        private readonly string unitTestUserId;

        public AddGenreServiceTests()
        {
            this.unitTestUserId = GlobalConstants.UnitTestAdminId;
        }

        [Fact]
        public void AddNewGenreAtDB()
        {
            //Arrange
            var options = new DbContextOptions<ApplicationDbContext>(); //Mock??
            var contextMock = new Mock<ApplicationDbContext>(options);
            var genreServiceMock = new Mock<IGenreService>();
            var messageServiceMock = new Mock<INotificationService>();
            //addGenreServiceMock.Protected()
            //    .Setup("AddGenre", paramsMock)
            //    .CallBase();
            var addGenreServiceMock = new Mock<AddGenreService>(contextMock.Object, genreServiceMock.Object, messageServiceMock.Object);
            var modelMock = new Mock<AddGenreViewModel>();
            modelMock.Object.Name = "New Ganre";
            modelMock.Object.Id = "1";
            
            //  var paramsMock = new object[] { modelMock.Object, this.unitTestUserId };
            //  addGenreServiceMock.Object.AddGenre(modelMock.Object, this.unitTestUserId);
            //Act
            string result = addGenreServiceMock.Object.AddGenre(modelMock.Object, this.unitTestUserId);

            //Assert
            Assert.Equal("Успешно добавен жанр!", result);
        }
    }
}
