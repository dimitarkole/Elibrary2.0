namespace ELibrary.Web.Areas.Library.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class AllAddedBooksController : LibraryController
    {
        public AllAddedBooksController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksService allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, IGenreService genreService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, genreService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddedBooks()
        {
            this.StartUp();
            var returnModel = this.allAddedBooksServices.PreparedPage(this.userId);
            return this.View(returnModel);
        }

        // AddedBooks Page - search book
        [Authorize]
        [HttpGet]
        public IActionResult AddedBooksSearch(AllAddedBooksViewModel model)
        {
            this.StartUp();

            var returnModel = this.allAddedBooksServices.GetBooks(model, this.userId);
            return this.View("AddedBooks", returnModel);
        }

        // AddedBooks Page - Delete book
        [Authorize]
        [HttpGet]
        public IActionResult DeleteBook(AllAddedBooksViewModel model, string id)
        {
            this.StartUp();

            this.ViewData["message"] = "Успешно премахната книга";
            var returnModel = this.allAddedBooksServices.DeleteBook(this.userId, model, id);

            return this.View("AddedBooks", returnModel);
        }

        [Authorize]
        [HttpGet]

        public IActionResult ChangePageAddedBook(AllAddedBooksViewModel model, int id)
        {
            this.StartUp();

            var returnModel = this.allAddedBooksServices.ChangeActivePage(model, this.userId, id);
            return this.View("AddedBooks", returnModel);
        }

        // AddedBooks Page - Edit book
        [Authorize]
        [HttpGet]
        public IActionResult EditBookAddedBook(string id)
        {
            this.StartUp();
            var model = this.addBookService.GetBookDataById(id);
            this.HttpContext.Session.SetString("editBookId", id);
            model.BookId = id;
            return this.View("EditBook", model);
        }

        // AllBooks Page - Edit book
        [Authorize]
        [HttpPost]
        public IActionResult EditBook(AddBookViewModel model)
        {
            this.StartUp();
            var bookId = this.HttpContext.Session.GetString("editBookId");
            model.BookId = bookId;
            var pic = model.Logo;
            var folder = "BooksLogo";
            if (pic != null)
            {
                var fileName = Path.Combine(
                    this.hostingEnvironment.WebRootPath + "/img/" + folder,
                    Path.GetFileName(this.userId + "_" + pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                model.LogoLocation = "/img/" + folder + "/" + Path.GetFileName(fileName);
            }

            var result = this.addBookService.EditBook(model, this.userId);
            this.ViewData["message"] = result[1];
            var returnModel = this.allAddedBooksServices.PreparedPage(this.userId);
            return this.View("AddedBooks", returnModel);
        }
    }
}