namespace ELibrary.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Common;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
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
    public class UserController : Controller
    {

        protected readonly IUserService userService;
        protected readonly ITakenBooksService takenBooksService;


        protected readonly INotificationService notificationService;
        protected readonly IGenreService genreService;
        protected string userId;

        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly ILogger<LogoutModel> logger;
        protected readonly IHostingEnvironment hostingEnvironment;

        public UserController(
            INotificationService notificationService,
            IUserService userService,
            IGenreService genreService,
            ITakenBooksService takenBooksService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IHostingEnvironment hostingEnvironment)
        {
            this.notificationService = notificationService;
            this.genreService = genreService;

            this.userService = userService;
            this.takenBooksService = takenBooksService;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        protected void StartUp()
        {
            this.userId = this.userManager.GetUserId(this.User);
            this.ViewData["UserType"] = "user";

            var messages = this.notificationService.GetNotificationsNavBar(this.userId);
            this.ViewData["MessageNavBar"] = messages;
        }
    }
}
