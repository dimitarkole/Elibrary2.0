namespace ELibrary.Web.Controllers
{
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.CommonResurces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class BaseController : Controller
    {
        protected readonly INotificationService notificationService;
        protected readonly IGenreService genreService;
        protected string userId;

        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;
        protected readonly ILogger<LogoutModel> logger;
        protected readonly IHostingEnvironment hostingEnvironment;

        public BaseController(INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment)
        {
            this.notificationService = notificationService;
            this.genreService = genreService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }


        public BaseController(
            INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        protected void StartUp()
        {
            if (this.signInManager.IsSignedIn(this.User))
            {
                this.userId = this.userManager.GetUserId(this.User);

                var messages = this.notificationService.GetNotificationsNavBar(this.userId);
                this.ViewData["MessageNavBar"] = messages;

            }
            else
            {
                NotificationsNavBarViewModel messages = new NotificationsNavBarViewModel();
                this.ViewData["MessageNavBar"] = messages;
            }

        }
    }
}
