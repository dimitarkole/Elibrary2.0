namespace ELibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class UserRolesController : AdministrationController
    {
        private readonly IAllUsersService allUsersService;

        public UserRolesController(IAllUsersService allUsersService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.allUsersService = allUsersService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            this.StartUp();

            var returnModel = this.allUsersService.PreparedPage();
            return this.View(returnModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult MakeUserAdmin(AllUsersViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.allUsersService.MakeUserAdmin(id);
            this.ViewData["message"] = returnModel["message"].ToString();
            var modelView = this.allUsersService.PreparedPage();
            return this.View("Index", modelView);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult MakeAdminUser(string id)
        {
            this.StartUp();
            var returnModel = this.allUsersService.MakeAdminUser(id);
            this.ViewData["message"] = returnModel["message"].ToString();
            var modelView = this.allUsersService.PreparedPage();
            return this.View("Index", modelView);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult DeleteUser(AllUsersViewModel model, string id)
        {
            this.StartUp();

            var returnModel = this.allUsersService.DeleteUser(model, id, this.userId);
            this.ViewData["message"] = returnModel["message"].ToString();
            var modelView = this.allUsersService.PreparedPage();
            return this.View("Index", modelView);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangeActivePage(AllUsersViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.allUsersService.ChangeActivePage(model, id);
            return this.View("Index", returnModel);
        }
    }
}
