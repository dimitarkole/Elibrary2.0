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
    }
}