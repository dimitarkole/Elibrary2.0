namespace ELibrary.Web.Controllers
{
    using System.Diagnostics;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels;
    using ELibrary.Web.ViewModels.Home;
    using ELibrary.Web.ViewModels.Library;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class HomeController : BaseController
    {
        public HomeController(IViewBooksService homeService, IViewBookService viewBookService, IAllLibrariesService allLibraryService, IRoleService roleService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(homeService, viewBookService, allLibraryService, roleService, notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        public IActionResult Index()
        {
            this.StartUp();
            var model = this.allLibraryService.PreparedPage();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult SearchLibraries(AllLibrariesViewModel model)
        {
            this.StartUp();
            var returnModel = this.allLibraryService.GetLibrarys(model);
            return this.View("Index", returnModel);
        }

        [HttpPost]
        public IActionResult AllLibrariesChangePage(AllLibrariesViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.allLibraryService.ChangeActivePage(model, id);
            return this.View("Index", returnModel);
        }

        public IActionResult Index2()
        {
            this.StartUp();
            var model = this.homeService.PreparedPage();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult HomeBooksSearch(AllAddedBooksViewModel model)
        {
            this.StartUp();
            var returnModel = this.homeService.GetBooks(model);
            return this.View("Index", returnModel);
        }

        [HttpPost]
        public IActionResult ChangePageHome(AllAddedBooksViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.homeService.ChangeActivePage(model, id);
            return this.View("Index", returnModel);
        }

        public IActionResult ViewBook(AllAddedBooksViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.viewBookService.PreparedPage(id);
            return this.View(returnModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this.StartUp();
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
