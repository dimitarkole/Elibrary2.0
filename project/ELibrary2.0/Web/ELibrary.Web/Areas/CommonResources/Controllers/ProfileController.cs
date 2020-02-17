namespace ELibrary.Web.Areas.CommonResources.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.CommonResurces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class ProfileController : CommonResourcesController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService,INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.profileService = profileService;
        }

        public IActionResult Index()
        {
            this.StartUp();
            var model = this.profileService.PreparedPage(this.userId);
            return this.View(model);
        }

        public async Task<IActionResult> ChangePassword(ProfilViewModel model)
        {
            this.StartUp();
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            var changPassword = model.ResetPasswordViewModel;

            var changePasswordResult = await this.userManager.ChangePasswordAsync(user, changPassword.OldPassword, changPassword.NewPassword);
            var returnModel = this.profileService.PreparedPage(this.userId);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }

                this.ViewData["message"] = "Неуспешно сменяне на парола!";
                return this.View("Profile", returnModel);
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.ViewData["message"] = "Успешно сменена на парола!";

            return this.View("Index", returnModel);
        }
    }
}