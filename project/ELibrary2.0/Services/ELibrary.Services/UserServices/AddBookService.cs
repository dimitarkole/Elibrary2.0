namespace ELibrary.Services.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Services.Contracts.UserServices;
    using ELibrary.Web.ViewModels.User; 

    public class AddBookService : IAddBookService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AddBookService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public AddBookViewModel PreparedAddBookPage()
        {
            throw new NotImplementedException();
        }
    }
}
