namespace ELibrary.Services.Data.Tests.AdminServicesTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data.Models;
    using ELibrary.Services.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;
    using Moq;
    using Xunit;

    public class AddPaymenPlantServiceTests : TransientDbContextProvider
    {
        private readonly Mock<IGenreService> genreServiceMock;
        private readonly Mock<INotificationService> messageServiceMock;
        private readonly Mock<AddPaymenPlantService> addPaymenPlantService;

        public AddPaymenPlantServiceTests()
        {
            this.genreServiceMock = new Mock<IGenreService>();
            this.messageServiceMock = new Mock<INotificationService>();
            this.addPaymenPlantService = new Mock<AddPaymenPlantService>(this.context, this.genreServiceMock.Object, this.messageServiceMock.Object);
        }

        [Theory]
        [InlineData("New plan1 here", 100, 200, "some text", 200, "Успешно добавен абонаментен план!")]
        [InlineData("N", 100, 200, "some text", 200, "Името на абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData(null, 100, 200, "some text", 200, "Името на абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData("New plan2", 500, 200, "some text", 200, "Двугодишната цена трябва да бъде по-голяма от едногодишната!")]
        [InlineData("New plan3", -500, 200, "some text", 200, "Цената на едногодишния абонамента трябва да бъде не отрицателно число!")]
        [InlineData("New plan4", -500, -200, "some text", 200, "Цената на едногодишния абонамента трябва да бъде не отрицателно число!Цената на двугодишния абонамента трябва да бъде не отрицателно число!")]
        [InlineData("New plan5", 100, 200, null, 200, "Текстът към абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData("New plan6", 100, 200, "some text", -200, "Броя на книгите трябва да бъде по-голям от 0!")]

        public void AddNewPlanAtDBTest(string planName, double priceOneYear, double priceTwoYears,  string text, int countBook,  string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = planName;
            modelMock.Object.PriceOneYear = priceOneYear;
            modelMock.Object.PriceTwoYears = priceTwoYears;
            modelMock.Object.Text = text;
            modelMock.Object.CountBook = countBook;

            // Act
            string result = this.addPaymenPlantService.Object.AddPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void DublicateAddNewPlanDublicateByNameTest()
        {
            // Arrange
            this.AddPlanAtContextReturnId("Plan name A");
            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = "Plan name A";
            modelMock.Object.PriceOneYear = 5;
            modelMock.Object.PriceTwoYears = 10;
            modelMock.Object.Text = "some text";
            modelMock.Object.CountBook = 30;

            // Act
            string result = this.addPaymenPlantService.Object.AddPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Името на плана се дублира!", result);
        }

        [Fact]
        public void DublicateAddNewPlanDublicateByBookCountTest()
        {
            // Arrange
            this.AddPlanAtContextReturnId("Plan name A");
            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = "Plan name B";
            modelMock.Object.PriceOneYear = 5;
            modelMock.Object.PriceTwoYears = 10;
            modelMock.Object.Text = "some text";
            modelMock.Object.CountBook = 100;

            // Act
            string result = this.addPaymenPlantService.Object.AddPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Броя на книгите се доблира с друг план!", result);
        }

        [Theory]
        [InlineData("Edeted plan", 100, 200, "some text", 200, "Успешно редактиран абонаментен план!")]
        [InlineData("N", 100, 200, "some text", 200, "Името на абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData(null, 100, 200, "some text", 200, "Името на абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData("Edeted plan", 500, 200, "some text", 200, "Двугодишната цена трябва да бъде по-голяма от едногодишната!")]
        [InlineData("Edeted plan", -500, 200, "some text", 200, "Цената на едногодишния абонамента трябва да бъде не отрицателно число!")]
        [InlineData("Edeted plan", -500, -200, "some text", 200, "Цената на едногодишния абонамента трябва да бъде не отрицателно число!Цената на двугодишния абонамента трябва да бъде не отрицателно число!")]
        [InlineData("Edeted plan", 100, 200, null, 200, "Текстът към абонаметния план трябва да съдържа поне 3 символа!")]
        [InlineData("Edeted plan", 100, 200, "some text", -200, "Броя на книгите трябва да бъде по-голям от 0!")]

        public void EditPlanAtDBTest(string planName, double priceOneYear, double priceTwoYears, string text, int countBook, string expectedResult)
        {
            // Arrange
            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = planName;
            modelMock.Object.PriceOneYear = priceOneYear;
            modelMock.Object.PriceTwoYears = priceTwoYears;
            modelMock.Object.Text = text;
            modelMock.Object.CountBook = countBook;

            var id = this.AddPlanAtContextReturnId("Editing plan Name");
            modelMock.Object.Id = id;
            // Act
            var result = this.addPaymenPlantService.Object.EditPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal(expectedResult, result["message"]);
        }

        [Fact]
        public void DublicateEditPlanDublicateByNameTest()
        {
            // Arrange
            this.AddPlanAtContextReturnId("Plan name A");
            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = "Plan name A";
            modelMock.Object.PriceOneYear = 5;
            modelMock.Object.PriceTwoYears = 10;
            modelMock.Object.Text = "some text";
            modelMock.Object.CountBook = 30;

            // Act
            var result = this.addPaymenPlantService.Object.EditPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Името на плана се дублира!", result["message"]);
        }

        [Fact]
        public void DublicateEditPlanDublicateByBookCountTest()
        {
            // Arrange
            this.AddPlanAtContextReturnId("Plan name A");
            var id = this.AddPlanAtContextReturnId("Plan name B");

            var modelMock = new Mock<AddPaymentPlanViewModel>();
            modelMock.Object.Name = "editing Plan name B";
            modelMock.Object.PriceOneYear = 5;
            modelMock.Object.PriceTwoYears = 10;
            modelMock.Object.Text = "some text";
            modelMock.Object.CountBook = 100;
            modelMock.Object.Id = id;
            // Act
            var result = this.addPaymenPlantService.Object.EditPaymentPlan(modelMock.Object, this.unitTestUserId);

            // Assert
            Assert.Equal("Броя на книгите се доблира с друг план!", result["message"]);
        }

        private string AddPlanAtContextReturnId(string planName)
        {
            var plan = new PaymentPlan()
            {
                Name = planName,
                CountBook = 100,
            };
            this.context.PaymentPlans.Add(plan);
            this.context.SaveChanges();
            return plan.Id;
        }
    }
}
