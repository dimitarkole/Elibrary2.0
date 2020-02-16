namespace ELibrary.Web.Areas.User.Controllers
{
    using System.IO;

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

    public class AddBookController : UserController
    {
        public AddBookController(IAddBookService addBookService, INotificationService notificationService, IAllAddedBooksServices allAddedBooksServices, IGiveBookService giveBookService, IGivenBooksService givenBooksService, IUserService userService, ITakenBooksService takenBooksService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(addBookService, notificationService, allAddedBooksServices, giveBookService, givenBooksService, userService, takenBooksService, userManager, signInManager, logger, hostingEnvironment)
        {
        }

        // AddBook Page - HttpGet
        [Authorize]
        [HttpGet]
        public IActionResult AddBook()
        {
            this.StartUp();

            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

        // AddBook Page - HttpPost
        [Authorize]
        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            this.StartUp();
            model = this.UploadFiles(model);

            this.ViewData["message"] = this.addBookService.AddBook(model, this.userId);
            var returnModel = this.addBookService.PreparedAddBookPage();
            return this.View(returnModel);
        }

        private AddBookViewModel UploadBookLogo(AddBookViewModel model)
        {
            var pic = model.Logo;
            var folder = "BooksLogo";
            if (pic != null)
            {
                var fileName = Path.Combine(
                    this.hostingEnvironment.WebRootPath + "/img/" + folder,
                    Path.GetFileName(this.userId + "_" + pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                model.LogoLocation = "/img/" + folder + "/" + Path.GetFileName(fileName);
            }
            else
            {
                model.LogoLocation = "PIC IS NULL";
            }

            return model;
        }

        private AddBookViewModel UploadBookOnlineVersion(AddBookViewModel model)
        {
            var file = model.PDF;
            var folder = "BooksOnlineVersion";
            if (file != null)
            {
                var fileName = Path.Combine(
                    this.hostingEnvironment.WebRootPath + "/img/" + folder,
                    Path.GetFileName(this.userId + "_" + file.FileName));
                file.CopyTo(new FileStream(fileName, FileMode.Create));
                model.LogoLocation = "/img/" + folder + "/" + Path.GetFileName(fileName);
            }

            return model;
        }

        private AddBookViewModel UploadFiles(AddBookViewModel model)
        {
            model = this.UploadBookLogo(model);
            model = this.UploadBookOnlineVersion(model);
            return model;
        }
    }
}
