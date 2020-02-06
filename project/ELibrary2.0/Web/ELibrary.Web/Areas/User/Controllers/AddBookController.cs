namespace ELibrary.Web.Areas.User.Controllers
{
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddBookController : UserController
    {
        public AddBookController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksServices allAddedBooksServices, IGiveBookService giveBookService, IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, userService, userManager, signInManager, logger, hostingEnvironment)
        {
        }



        // AddBook Page - HttpGet
        [Authorize]
        [HttpGet]
        public IActionResult AddBook()
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

        // AddBook Page - HttpPost
        [Authorize]
        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            var startUp = this.StartUp();
            if (startUp != null)
            {
                return startUp;
            }

            this.ViewData["message"] = this.addBookService.AddBook(model, this.userId);
            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

       

    }
}
