namespace ELibrary.Services.Admin
{
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AddGenreService : IAddGenreService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AddGenreService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public string AddBook(AddGenreViewModel model, string userId)
        {
            var message = "Жанра се дублира с друг!";
            if (this.IsDublicated(model) == false)
            {
                var genre = new Genre()
                {
                    Name = model.Name,
                };

                this.context.Genres.Add(genre);
                this.context.SaveChanges();

                message = "Успершно добавен жанр!";
                this.messageService.AddNotificationAtDB(userId, message);
            }

            return message;
        }

        public List<object> EditBook(AddGenreViewModel model, string userId)
        {
            var result = new List<object>();
            var message = "Жанра се дублира с друг!";
            if (this.IsDublicated(model) == false)
            {
                var genre = this.context.Genres.FirstOrDefault(g => g.Id == model.Id);
                genre.Name = model.Name;
                this.context.Genres.Add(genre);
                this.context.SaveChanges();
                message = "Успершно редактиран жанр!";
                this.messageService.AddNotificationAtDB(userId, message);
            }

            result.Add(model);
            result.Add(message);
            return result;
        }

        public AddGenreViewModel GetBookDataById(string genreId)
        {
            var genre = this.context.Genres.FirstOrDefault(g => g.Id == genreId);
            var model = new AddGenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
            };
            return model;
        }

        public AddGenreViewModel PreparedAddBookPage()
        {
            var model = new AddGenreViewModel();
            return model;
        }

        private bool IsDublicated(AddGenreViewModel model)
        {
            var genre = this.context.Genres.FirstOrDefault(g => g.Name == model.Name);
            if (genre == null)
            {
                return false;
            }

            return true;
        }
    }
}
