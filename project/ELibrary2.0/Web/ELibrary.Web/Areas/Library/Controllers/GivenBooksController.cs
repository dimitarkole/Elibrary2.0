﻿namespace ELibrary.Web.Areas.Library.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.Library;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class GivenBooksController : LibraryController
    {
        public GivenBooksController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksService allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, IGenreService genreService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, genreService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
        }


        // GivenBooks Page - GivenBooks
        [Authorize]
        [HttpGet]
        public IActionResult GivenBooks()
        {
            this.StartUp();
            var returnModel = this.givenBooksService.PreparedPage(this.userId);
            return this.View(returnModel);
        }

        // GiveBook Page - ChangePageGivenBooks
        [Authorize]
        [HttpPost]
        public IActionResult ChangePageGivenBooks(GivenBooksViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.givenBooksService.ChangeActivePage(model, this.userId, id);
            return this.View("GivenBooks", returnModel);
        }

        // GiveBook Page - GivenBooksSearch
        [Authorize]
        [HttpPost]
        public IActionResult GivenBooksSearch(GivenBooksViewModel model)
        {
            this.StartUp();
            var returnModel = this.givenBooksService.GetGevenBooks(model, this.userId);
            return this.View("GivenBooks", returnModel);
        }

        // GiveBook Page - ReturningGivenBook
        [Authorize]
        [HttpPost]
        public IActionResult ReturningGivenBook(GivenBooksViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.givenBooksService.ReturningBook(model, this.userId, id);
            this.ViewData["message"] = returnModel[1];
            return this.View("GivenBooks", returnModel[0]);
        }

        // GiveBook Page - DeleteGivenBook
        [Authorize]
        [HttpPost]
        public IActionResult DeleteGivenBook(GivenBooksViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.givenBooksService.DeletingBook(model, this.userId, id);
            return this.View("GivenBooks", returnModel[0]);
        }


        // GiveBook Page - SendMessageForReturningBook
        [Authorize]
        [HttpPost]
        public IActionResult SendMessageForReturningBook(GivenBooksViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.givenBooksService.SendMessageForReturningBook(model, this.userId, id);
            this.ViewData["message"] = returnModel[1];
            return this.View("GivenBooks", returnModel[0]);
        }
    }
}