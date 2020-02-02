namespace ELibrary.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Common;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize(Roles = GlobalConstants.UserRoleName)]
    [Area("User")]
    public class UserController : BaseController
    {
        protected readonly IAddBookService addBookService;
        protected readonly INotificationService notificationService;
        protected string userId;
        protected readonly IAllAddedBooksServices allAddedBooksServices;

        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly ILogger<LogoutModel> logger;
        protected readonly IHostingEnvironment hostingEnvironment;

        public UserController(
            IAddBookService addBookService,
            INotificationService notificationService,
            IAllAddedBooksServices allAddedBooksServices,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IHostingEnvironment hostingEnvironment)
        {
            this.addBookService = addBookService;
            this.notificationService = notificationService;
            this.allAddedBooksServices = allAddedBooksServices;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        protected IActionResult StartUp()
        {
            this.userId = this.userManager.GetUserId(this.User);
            this.ViewData["UserType"] = "library";

            var messages = this.notificationService.GetNotificationsNavBar(this.userId);
            this.ViewData["MessageNavBar"] = messages;
            return null;
        }
    }
}
