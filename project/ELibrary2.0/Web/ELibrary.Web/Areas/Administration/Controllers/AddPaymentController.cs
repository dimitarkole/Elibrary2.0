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

    public class AddPaymentController : AdministrationController
    {
        private readonly IAddPaymentPlanService addPaymentPlantService;

        public AddPaymentController(IAddPaymentPlanService addPaymentPlantService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.addPaymentPlantService = addPaymentPlantService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            this.StartUp();
            var model = this.addPaymentPlantService.PreparedAddPaymentPlanPage();

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddPaymentPlan(AddPaymentPlanViewModel model)
        {
            this.StartUp();
            this.ViewData["message"] = this.addPaymentPlantService.AddPaymentPlan(model, this.userId);
            return this.View("Index", model);
        }
    }
}