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

    public class AllAddedPaymentPlansController : AdministrationController
    {
        private readonly IAllPaymentPlansService allPaymentPlansService;
        private readonly IAddPaymentPlanService addPaymentPlantService;


        public AllAddedPaymentPlansController(IAddPaymentPlanService addPaymentPlantService, IAllPaymentPlansService allPaymentPlansService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.addPaymentPlantService = addPaymentPlantService;
            this.allPaymentPlansService = allPaymentPlansService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            this.StartUp();
            var model = this.allPaymentPlansService.PreparedPage(this.userId);
            return this.View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeletePaymentPlan(AllPaymentPlansViewModel model, string id)
        {
            this.StartUp();
            var returnModel = this.allPaymentPlansService.DeletePaymentPlan(this.userId, model, id);
            this.ViewData["message"] = "Успешно премахнат план за плащане";
            return this.View("Index", returnModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditPaymentPlan(string id)
        {
            this.StartUp();
            var model = this.allPaymentPlansService.GetPaymentPlanData(id);
            this.TempData["editPlanId"] = id;
            return this.View("EditPaymentPlan", model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePagePaymentPlans(AllPaymentPlansViewModel model, int id)
        {
            this.StartUp();
            var returnModel = this.allPaymentPlansService.ChangeActivePage(model, this.userId, id);
            return this.View("Index", returnModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult PaymentPlanEditing(AddPaymentPlanViewModel model, string id)
        {
            this.StartUp();
            model.Id = this.TempData["editPlanId"].ToString();
            var result = this.addPaymentPlantService.EditPaymentPlan(model, this.userId);
            this.ViewData["message"] = result[1];
            this.TempData["editPlanId"] = model.Id;
            return this.View("EditPaymentPlan", model);
        }
    }
}