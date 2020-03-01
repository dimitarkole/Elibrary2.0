using ELibrary.Services.Contracts.LibraryServices;
using ELibrary.Web.ViewModels.Library;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Services.Data.Tests.LibraryServicesTests
{
    [TestFixture]
    public class AddBookServiceTests
    {
        private readonly IAddBookService addBookService;

        public AddBookServiceTests(IAddBookService addBookService)
        {
            this.addBookService = addBookService;
        }

        [Test]
        public void AddNewBookNotDublicated()
        {
            AddBookViewModel modelNewBook = new AddBookViewModel();

            var result = this.addBookService.AddBook(modelNewBook, " ");
            /*Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(10, 10);
            axe.Attack(dummy);
            Assert.AreEqual(9, axe.DurabilityPoints, "Axe Durability dosn't change after atack.");*/
        }
    }
}
