namespace ELibrary.Services.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Home;

    public class ViewBookService : IViewBookService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public ViewBookService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public ViewBookViewModel PreparedPage(string bookId)
        {
            var book = this.context.Books.FirstOrDefault(b => b.Id == bookId);
            var genre = this.context.Genres.FirstOrDefault(g => g.Id == book.GenreId);
            ViewBookViewModel model = new ViewBookViewModel()
            {
                Author = book.Author,
                BookId = book.Id,
                CatalogNumber = book.CatalogNumber,
                GenreId = book.GenreId,
                GenreName = genre.Name,
                Logo = book.Logo,
                Review = book.Review,
                Title = book.Title,
            };
            return model;
        }
    }
}
