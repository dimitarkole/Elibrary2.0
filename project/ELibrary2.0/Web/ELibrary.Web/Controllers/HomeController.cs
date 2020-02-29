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
        private readonly IViewLibraryService viewLibraryService;
        public HomeController(IViewLibraryService viewLibraryService, IViewBooksService homeService, IViewBookService viewBookService, IAllLibrariesService allLibraryService, IRoleService roleService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(homeService, viewBookService, allLibraryService, roleService, notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.viewLibraryService = viewLibraryService;
        }

        public IActionResult Index()
        {
            this.StartUp();
            var model = this.homeService.PreparedPage();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChangeActiveBookPage(AllAddedBooksViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.homeService.ChangeActivePage(model, id);
            return this.View("Index", returnModel);
        }

        [HttpPost]
        public IActionResult SearchBook(AllAddedBooksViewModel model)
        {
            this.StartUp();
            var returnModel = this.homeService.GetBooks(model);
            return this.View("Index", returnModel);
        }

        public IActionResult ViewBook(string id)
        {
            this.StartUp();
            this.TempData["viewBookId"] = id;
            var returnModel = this.viewBookService.PreparedPage(id);
            return this.View(returnModel);
        }

        public IActionResult ReserveBook()
        {
            this.StartUp();
            var bookId = this.TempData["viewBookId"].ToString();
            var result = this.viewBookService.ReserveTheBook(bookId, this.userId);
            var returnModel = result["model"];
            this.ViewData["message"] = result["message"];
            this.TempData["viewBookId"] = bookId;
            return this.View("ViewBook", returnModel);
        }

        public IActionResult AddReview(ViewBookViewModel model)
        {
            this.StartUp();
            var bookId = this.TempData["viewBookId"].ToString();
            var result = this.viewBookService.AddReview(model, bookId, this.userId);
            var returnModel = result["model"];
            this.ViewData["messageAddReview"] = result["message"];
            this.TempData["viewBookId"] = bookId;
            return this.View("ViewBook", returnModel);
        }











        /*public IActionResult Index()
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

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            this.StartUp();
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }*/
    }
}
