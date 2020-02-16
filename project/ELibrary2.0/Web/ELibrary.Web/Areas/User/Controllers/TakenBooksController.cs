namespace ELibrary.Web.Areas.User.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class TakenBooksController : UserController
    {
        public TakenBooksController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksServices allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult TakenBooks()
        {
            this.StartUp();
            var returnModel = this.takenBooksService.PreparedPage(this.userId);
            return this.View(returnModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult TakenBooksSearch(TakenBooksViewModel model)
        {
            this.StartUp();
            var returnModel = this.takenBooksService.TakenBooks(model, this.userId);
            return this.View("TakenBooks", returnModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ChangePageTakenBooks(TakenBooksViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.takenBooksService.ChangeActivePage(model, this.userId, id);
            return this.View("TakenBooks", returnModel);
        }
    }
}
