namespace ELibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class AllAddedGenresController : AdministrationController
    {
        private readonly IAllAddedGenresService allGenresService;
        private readonly IAddGenreService addGenreService;


        public AllAddedGenresController(IAddGenreService addGenreService, IAllAddedGenresService allGenresService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.allGenresService = allGenresService;
            this.addGenreService = addGenreService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            this.StartUp();
            var model = this.allGenresService.PreparedPage(this.userId);
            return this.View("Index", model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddedGenresSearch(AllAddedGenresViewModel model)
        {
            this.StartUp();
            var returnModel = this.allGenresService.GetGenres(model, this.userId);
            return this.View("Index", returnModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteGenre(AllAddedGenresViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.allGenresService.DeleteGenre(this.userId, model, id);
            this.ViewData["message"] = "Успешно премахнат жанр!";
            return this.View("Index", returnModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePageGenres(AllAddedGenresViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.allGenresService.ChangeActivePage(model, this.userId, id);
            return this.View("Index", returnModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditGenre(string id)
        {
            this.StartUp();
            var model = this.allGenresService.GetGenreData(id);
            this.TempData["editPlanId"] = id;
            return this.View("EditGenre", model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult GenreEditing(AddGenreViewModel model, string id)
        {
            this.StartUp();
            model.Id = this.TempData["editPlanId"].ToString();
            var result = this.addGenreService.EditGenre(model, this.userId);
            this.ViewData["message"] = result[1];
            if (result[1].ToString().Contains("Успешно"))
            {
                return this.Index();
            }

            this.TempData["editPlanId"] = model.Id;
            return this.View("EditGenre", model);
        }
    }
}
