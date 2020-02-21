namespace ELibrary.Web.Areas.Library.Controllers
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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize(Roles = GlobalConstants.LibraryRoleName)]
    [Area("Library")]
    public class LibraryController : Controller
    {
        protected readonly IAddBookService addBookService;
        protected readonly IAllAddedBooksService allAddedBooksServices;
        protected readonly IGiveBookService giveBookService;
        protected readonly IGivenBooksService givenBooksService;
        protected readonly IUserService userService;

        protected readonly INotificationService notificationService;
        protected readonly IGenreService genreService;
        protected string userId;

        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly ILogger<LogoutModel> logger;
        protected readonly IHostingEnvironment hostingEnvironment;

        public LibraryController(
            IAddBookService addBookService,
            INotificationService notificationService,
            IAllAddedBooksService allAddedBooksServices,
            IGiveBookService giveBookService,
            IGivenBooksService givenBooksService,
            IUserService userService,
            IGenreService genreService,
            ITakenBooksService takenBooksService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IHostingEnvironment hostingEnvironment)
        {
            this.addBookService = addBookService;
            this.notificationService = notificationService;
            this.allAddedBooksServices = allAddedBooksServices;
            this.genreService = genreService;
            this.giveBookService = giveBookService;
            this.givenBooksService = givenBooksService;

            this.userService = userService;

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        protected void StartUp()
        {
            this.userId = this.userManager.GetUserId(this.User);
            this.ViewData["UserType"] = "library";

            var messages = this.notificationService.GetNotificationsNavBar(this.userId);
            this.ViewData["MessageNavBar"] = messages;
        }
    }
}