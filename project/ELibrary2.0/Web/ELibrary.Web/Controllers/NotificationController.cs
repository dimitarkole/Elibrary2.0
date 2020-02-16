using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELibrary.Data.Models;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Web.Areas.Identity.Pages.Account;
using ELibrary.Web.ViewModels.CommonResurces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ELibrary.Web.Controllers
{
    public class NotificationController : BaseController
    {
        public NotificationController(INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            this.StartUp();
            var returnModel = this.notificationService.GetNotificationsPreparedPage(this.userId);
            return this.View(returnModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult NotificationsChangePage(NotificationsViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.notificationService.GetNotificationsChangePage(model, this.userId, id);
            this.StartUp();

            return this.View("Index", returnModel);
        }
    }
}