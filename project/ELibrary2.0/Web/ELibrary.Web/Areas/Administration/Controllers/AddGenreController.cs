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

    public class AddGenreController : AdministrationController
    {
        private readonly IAddGenreService addGenreService;


        public AddGenreController(IAddGenreService addGenreService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.addGenreService = addGenreService;

        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            this.StartUp();

            var model = this.addGenreService.PreparedAddBookPage();
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel model)
        {
            this.StartUp();
            this.ViewData["message"] = this.addGenreService.AddBook(model, this.userId);
            return this.View("Index", model);
        }
        
    }
}