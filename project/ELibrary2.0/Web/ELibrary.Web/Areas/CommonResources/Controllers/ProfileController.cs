namespace ELibrary.Web.Areas.CommonResources.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ProfileController : CommonResourcesController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService,INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.profileService = profileService;
        }

        public IActionResult Index()
        {
            this.StartUp();
            var model = this.profileService.PreparedPage(this.userId);
            return this.View(model);
        }
    }
}