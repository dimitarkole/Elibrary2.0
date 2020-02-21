namespace ELibrary.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.Home;
    using ELibrary.Web.ViewModels.Library;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ViewLibraryController : BaseController
    {
        private readonly IViewLibraryService viewLibraryService;

        public ViewLibraryController(IViewLibraryService viewLibraryService, IViewBooksService homeService, IViewBookService viewBookService, IAllLibrariesService allLibraryService, IRoleService roleService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(homeService, viewBookService, allLibraryService, roleService, notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.viewLibraryService = viewLibraryService;
        }

        public IActionResult Index(string id)
        {
            this.StartUp();
            var model = this.viewLibraryService.PreparedPage(id);
            this.TempData["viewLibraryId"] = id;
            return this.View(model);
        }

        [HttpPost]
        public IActionResult ChangeActiveBookPage(ViewLibraryViewModel model,  int id)
        {
            this.StartUp();
            var libraryId = this.TempData["viewLibraryId"].ToString();
            var returnModel = this.viewLibraryService.ChangeActiveBookPage(model, id, libraryId);
            this.TempData["viewLibraryId"] = libraryId;
            return this.View("Index", returnModel);
        }

        [HttpPost]
        public IActionResult SearchBookAtLibrary(ViewLibraryViewModel model)
        {
            this.StartUp();
            var libraryId = this.TempData["viewLibraryId"].ToString();
            var returnModel = this.viewLibraryService.GetLibraryData(model, libraryId);
            this.TempData["viewLibraryId"] = libraryId;
            return this.View("Index", returnModel);
        }

        public IActionResult ViewBook(AllAddedBooksViewModel model, string id)
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
    }
}