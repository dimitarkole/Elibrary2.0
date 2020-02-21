namespace ELibrary.Web.Areas.Library.Controllers
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

    public class StatsLibraryController : LibraryController
    {
        private readonly IStatsLibraryService statsUserService;

        public StatsLibraryController(IStatsLibraryService statsUserService, IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksService allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, IGenreService genreService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, genreService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.statsUserService = statsUserService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            this.StartUp();
            var model = this.statsUserService.PreparedPage(this.userId);
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult StatsSearch(StatsLibraryViewModel model)
        {
            this.StartUp();
            var returnModel = this.statsUserService.SearchStats(model, this.userId);
            return this.View("Index", returnModel);
        }
    }
}