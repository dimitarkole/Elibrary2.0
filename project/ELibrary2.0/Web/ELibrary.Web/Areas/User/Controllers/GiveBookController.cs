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

    public class GiveBookController : UserController
    {
        public GiveBookController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksService allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
        }


        // GiveBook Page
        [Authorize]
        [HttpGet]
        public IActionResult GiveBook()
        {
            this.StartUp();
            var model = this.giveBookService.PreparedPage(this.userId);
            return this.View(model);
        }

        // GiveBook Page - GiveBookSearchBook
        [Authorize]
        [HttpPost]
        public IActionResult GiveBookSearchBook(GiveBookViewModel model)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");
            var returnModel = this.giveBookService.GiveBookSearchBook(
                model, this.userId, selectedBookId, selecteduserId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - GiveBookSearchUser
        [Authorize]
        [HttpPost]
        public IActionResult GiveBookSearchUser(GiveBookViewModel model)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");
            var returnModel = this.giveBookService.GiveBookSearchUser(
                model, this.userId, selectedBookId, selecteduserId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - GiveBookChangePageBook
        [Authorize]
        [HttpPost]
        public IActionResult GiveBookChangePageBooks(GiveBookViewModel model, int id)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");
            var returnModel = this.giveBookService.GiveBookChangeBookPage(
                model, this.userId, id, selectedBookId, selecteduserId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - GiveBookChangePageUser
        [Authorize]
        [HttpPost]
        public IActionResult GiveBookChangePageUsers(GiveBookViewModel model, int id)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");
            var returnModel = this.giveBookService.GiveBookChangeUserPage(
                model, this.userId, id, selectedBookId, selecteduserId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - GiveBookChangePageUser
        [Authorize]
        [HttpPost]
        public IActionResult SelectBookGiveBookPage(GiveBookViewModel model, string id)
        {
            this.StartUp();
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");
            var returnModel = this.giveBookService.GiveBookSelectedBook(
                model, this.userId, id, selecteduserId);
            this.HttpContext.Session.SetString("SelectedBookId", returnModel.SelectedBook.BookId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - SelectUserGiveBookPage
        [Authorize]
        [HttpPost]
        public IActionResult SelectUserGiveBookPage(GiveBookViewModel model, string id)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");

            var returnModel = this.giveBookService.GiveBookSelectedUser(
                model, this.userId, id, selectedBookId);
            this.HttpContext.Session.SetString("SelecteduserId", returnModel.SelectedUser.UserId);
            return this.View("GiveBook", returnModel);
        }

        // GiveBook Page - GiveBookGivingBook
        [Authorize]
        [HttpPost]
        public IActionResult GiveBookGivingBook(GiveBookViewModel model)
        {
            this.StartUp();
            string selectedBookId = this.HttpContext.Session.GetString("SelectedBookId");
            string selecteduserId = this.HttpContext.Session.GetString("SelecteduserId");

            var returnModel = this.giveBookService.GivingBook(
                model, this.userId, selectedBookId, selecteduserId);
            this.ViewData["message"] = returnModel[0] == null ? "Да" : returnModel[0];
            //return this.View("GiveBook", model);

            return this.View("GiveBook", returnModel[1]);
        }
    }
}