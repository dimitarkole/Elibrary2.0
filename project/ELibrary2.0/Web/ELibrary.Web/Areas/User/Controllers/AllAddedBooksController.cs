namespace ELibrary.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
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

    public class AllAddedBooksController : UserController
    {
        public AllAddedBooksController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksServices allAddedBooksServices, IGiveBookService giveBookService, IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, userService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        public IActionResult AddedBooks()
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var returnModel = this.allAddedBooksServices.PreparedPage(this.userId);
            return this.View(returnModel);
        }

        // AddedBooks Page - search book
        [Authorize]
        [HttpPost]
        public IActionResult AddedBooksSearch(AllAddedBooksViewModel model)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var returnModel = this.allAddedBooksServices.GetBooks(model, this.userId);
            return this.View("AddedBooks", returnModel);
        }

        // AddedBooks Page - Delete book
        [Authorize]
        [HttpPost]
        public IActionResult DeleteBook(AllAddedBooksViewModel model, string id)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            this.ViewData["message"] = "Успешно премахната книга";
            var returnModel = this.allAddedBooksServices.DeleteBook(this.userId, model, id);

            return this.View("AddedBooks", returnModel);
        }

        [Authorize]
        public IActionResult ChangePageAllBook(AllAddedBooksViewModel model, int id)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var returnModel = this.allAddedBooksServices.ChangeActivePage(model, this.userId, id);
            return this.View("AddedBooks", returnModel);
        }

        // AddedBooks Page - Edit book
        [Authorize]
        [HttpPost]
        public IActionResult EditBookAddedBook(string id)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var model = this.addBookService.GetBookDataById(id);
            this.HttpContext.Session.SetString("editBookId", id);
            return this.View("EditBook", model);
        }

        // AllBooks Page - Edit book
        [Authorize]
        [HttpPost]
        public IActionResult EditBook(AddBookViewModel model)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var bookId = this.HttpContext.Session.GetString("editBookId");
            model.BookId = bookId;
            var result = this.addBookService.EditBook(model, this.userId);
            var returnModel = result[0];
            this.ViewData["message"] = result[1];
            return this.View(model);
        }
    }
}