﻿namespace ELibrary.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ELibrary.Data.Common.Repositories;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Data;
    using ELibrary.Web.Areas.Identity.Pages.Account;
    using ELibrary.Web.ViewModels.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class SettingsController : BaseController
    {
        private readonly ISettingsService settingsService;

        private readonly IDeletableEntityRepository<Setting> repository;


        public SettingsController(ISettingsService settingsService, IDeletableEntityRepository<Setting> repository, IViewBooksService homeService, IViewBookService viewBookService, IAllLibrariesService allLibraryService, IRoleService roleService, INotificationService notificationService, IGenreService genreService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, IHostingEnvironment hostingEnvironment) : base(homeService, viewBookService, allLibraryService, roleService, notificationService, genreService, userManager, signInManager, logger, hostingEnvironment)
        {
            this.settingsService = settingsService;
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var settings = this.settingsService.GetAll<SettingViewModel>();
            var model = new SettingsListViewModel { Settings = settings };
            return this.View(model);
        }

        public async Task<IActionResult> InsertSetting()
        {
            var random = new Random();
            var setting = new Setting { Name = $"Name_{random.Next()}", Value = $"Value_{random.Next()}" };

            await this.repository.AddAsync(setting);
            await this.repository.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
