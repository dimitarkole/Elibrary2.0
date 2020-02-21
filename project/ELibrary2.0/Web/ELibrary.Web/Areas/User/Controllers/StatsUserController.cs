using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Data.Models;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Services.Contracts.LibraryServices;
using ELibrary.Services.Contracts.UserServices;
using ELibrary.Web.Areas.Identity.Pages.Account;
using ELibrary.Web.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ELibrary.Web.Areas.User.Controllers
{
    public class StatsUserController : UserController
    {
        private IStatsUserService statsUserService;
        public StatsUserController(
             IStatsUserService statsUserService,
             INotificationService notificationService,
                IUserService userService,
                IGenreService genreService,
                ITakenBooksService takenBooksService,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILogger<LogoutModel> logger,
                                   IHostingEnvironment hostingEnvironment)
            : base(notificationService, userService, genreService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.statsUserService = statsUserService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
             this.StartUp();
             var returnModel = this.statsUserService.PreparedPage(this.userId);
             this.ViewData["message"] = this.userId;
             return this.View(returnModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult StatsSearch(StatsUserViewModel model)
        {
            this.StartUp();
            var returnModel = this.statsUserService.SearchStats(model, this.userId);
            this.ViewData["message"] = this.userId;
            return this.View("Index", returnModel);
        }
    }
}