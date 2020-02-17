using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Common;
using ELibrary.Data.Models;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Web.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ELibrary.Web.Areas.CommonResources.Controllers
{
    [Authorize]
    [Area("CommonResources")]
    public class CommonResourcesController : Controller
    {
        protected readonly INotificationService notificationService;
        protected readonly IGenreService genreService;
        protected string userId;

        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly ILogger<LogoutModel> logger;
        protected readonly IHostingEnvironment hostingEnvironment;

        public CommonResourcesController(INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment)
        {
            this.notificationService = notificationService;
            this.genreService = genreService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        protected void StartUp()
        {
            this.userId = this.userManager.GetUserId(this.User);

            var messages = this.notificationService.GetNotificationsNavBar(this.userId);
            this.ViewData["MessageNavBar"] = messages;
        }
    }
}