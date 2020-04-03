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

    }
}
