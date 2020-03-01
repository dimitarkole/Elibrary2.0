namespace ELibrary.Services.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Common;
    using ELibrary.Data;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AllLibrariesService : IAllLibrariesService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        private IRoleService roleService;

        public AllLibrariesService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService,
            IRoleService roleService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
            this.roleService = roleService;
        }

        public AllLibrariesViewModel ChangeActivePage(AllLibrariesViewModel model, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetLibrarys(model);
        }

        public AllLibrariesViewModel GetLibrarys(AllLibrariesViewModel model)
        {
            var libraryEmail = model.SearchLibrary.Email;
            var libraryName = model.SearchLibrary.Name;

            var sortMethodId = model.SortMethodId;
            var countLibraryOfPage = model.CountLibraiesOfPage;
            var currentPage = model.CurrentPage;
            var libraryRoleId = this.context.Roles.FirstOrDefault(r => r.Name == GlobalConstants.LibraryRoleName).Id;

            var libraries = this.context.Users
                .Include(u => u.Roles)
                .Where(u =>
                    u.DeletedOn == null
                    && u.Roles.FirstOrDefault().RoleId == libraryRoleId)
                .Select(u => new LibraryViewModel()
                {
                    Email = u.Email,
                    Location = u.LastName,
                    Name = u.FirstName,
                    Role = this.roleService.GetUserRole(u),
                    Avatar = u.Avatar,
                    UserId = u.Id,
                });

            libraries = this.SelectLibraries(libraryEmail, libraryName, libraries);
            libraries = this.SortLibraries(sortMethodId, libraries);
            int maxCountPage = libraries.Count() / countLibraryOfPage;
            if (libraries.Count() % countLibraryOfPage != 0)
            {
                maxCountPage++;
            }

            var viewLibraries = libraries.Skip((currentPage - 1) * countLibraryOfPage)
                                .Take(countLibraryOfPage);
            var searchLibrary = new LibraryViewModel()
            {
                Email = libraryEmail,
                Name = libraryName,
            };

            var returnModel = new AllLibrariesViewModel()
            {
                CountLibraiesOfPage = countLibraryOfPage,
                Libraries = viewLibraries,
                SearchLibrary = searchLibrary,
                SortMethodId = sortMethodId,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
            };
            return returnModel;
        }

        public AllLibrariesViewModel PreparedPage()
        {
            var model = new AllLibrariesViewModel();
            var returnModel = this.GetLibrarys(model);
            return returnModel;
        }

        private IQueryable<LibraryViewModel> SortLibraries(
        string sortMethodId,
        IQueryable<LibraryViewModel> libraries)
        {
            if (sortMethodId == "Email на билиотеката я-а")
            {
                libraries = libraries.OrderByDescending(b => b.Email);
            }
            else
            {
                libraries = libraries.OrderBy(b => b.Email);
            }

            return libraries;
        }

        private IQueryable<LibraryViewModel> SelectLibraries(
          string libraryEmail,
          string libraryName,
          IQueryable<LibraryViewModel> libraries)
        {
            if (libraryEmail != null)
            {
                libraries = libraries.Where(l => l.Email.Contains(libraryEmail));
            }

            if (libraryName != null)
            {
                libraries = libraries.Where(l => l.Name.Contains(libraryName));
            }

            return libraries;
        }
    }
}
