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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class AllAddedGenresController : AdministrationController
    {
        private readonly IAllAddedGenresService allGenresService;
        public AllAddedGenresController(IAllAddedGenresService allGenresService,INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.allGenresService = allGenresService;
        }

        public IActionResult Index()
        {
            this.StartUp();
            var model = this.allGenresService.PreparedPage(this.userId);
            return this.View(model);
        }
    }
}