namespace ELibrary.Web.Areas.User.Controllers
{
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddBookController : UserController
    {
        private readonly IAddBookService addBookService;
        private readonly INotificationService notificationService;
        private string userId;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;
        private readonly IHostingEnvironment hostingEnvironment;
        public AddBookController(
            IAddBookService addBookService,
            INotificationService notificationService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IHostingEnvironment hostingEnvironment)
        {
            this.addBookService = addBookService;
            this.notificationService = notificationService;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        // AddBook Page - HttpGet
        [Authorize]
        [HttpGet]
        public IActionResult AddBook()
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

        // AddBook Page - HttpPost
        [Authorize]
        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            this.ViewData["message"] = this.addBookService.AddBook(model, this.userId);
            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

        private IActionResult StartUp()
        {
            this.userId = this.userManager.GetUserId(this.User);
            this.ViewData["UserType"] = "library";

            var messages = this.notificationService.GetNotificationsNavBar(this.userId);
            this.ViewData["MessageNavBar"] = messages;
            return null;
        }

    }
}
