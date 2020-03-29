namespace ELibrary.Web.Controllers
{
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
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

        protected readonly IViewBooksService homeService;
        protected readonly IViewBookService viewBookService;
        protected readonly IAllLibrariesService allLibraryService;
        protected readonly IRoleService roleService;

        public BaseController(
            IViewBooksService homeService,
            IViewBookService viewBookService,
            IAllLibrariesService allLibraryService,
            IRoleService roleService,
            INotificationService notificationService,
            IGenreService genreService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger,
            IHostingEnvironment hostingEnvironment)
        {
            this.notificationService = notificationService;
            this.genreService = genreService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;

            this.homeService= homeService;
            this.viewBookService= viewBookService;
            this.allLibraryService= allLibraryService;
            this.roleService= roleService;
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
