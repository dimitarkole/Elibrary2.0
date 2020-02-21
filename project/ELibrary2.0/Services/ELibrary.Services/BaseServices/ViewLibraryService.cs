namespace ELibrary.Services.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Web.ViewModels.Home;

    public class ViewLibraryService : IViewLibraryService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        private IRoleService roleService;

        private IAllAddedBooksService allAddedBooksService;


        public ViewLibraryService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService,
            IRoleService roleService,
            IAllAddedBooksService allAddedBooksService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
            this.roleService = roleService;
            this.allAddedBooksService = allAddedBooksService;
        }

        public ViewLibraryViewModel PreparedPage(string libraryId)
        {
            ViewLibraryViewModel model = new ViewLibraryViewModel();
            return this.GetLibraryData(model, libraryId);
        }

        public ViewLibraryViewModel GetLibraryData(ViewLibraryViewModel model, string libraryId)
        {
            var library = this.context.Users.FirstOrDefault(u => u.Id == libraryId);
            var allAddedBooksModel = model.AllAddedBooks;
            var allAddedBooks = this.allAddedBooksService.GetBooks(allAddedBooksModel, libraryId);
            var returnModel = new ViewLibraryViewModel()
            {
                Avatar = library.Avatar,
                Email = library.Email,
                Location = library.LastName,
                Name = library.FirstName,
                AllAddedBooks = allAddedBooks,
            };
            return returnModel;

        }

        public ViewLibraryViewModel ChangeActiveBookPage(ViewLibraryViewModel model, int newPage, string libraryId)
        {
            var allAddedBooksModel = model.AllAddedBooks;
            allAddedBooksModel.CurrentPage = newPage;
            model.AllAddedBooks = allAddedBooksModel;
            return this.GetLibraryData(model, libraryId);
        }
    }
}
