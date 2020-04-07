using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ELibrary.Tests")]
namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;

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

        public string AddGenre(AddGenreViewModel model, string userId)
        {
            var message = this.IsHasNullData(model);
            if (string.IsNullOrEmpty(message))
            {
                message = "Жанра се дублира с друг!";
                if (this.IsDublicated(model) == false)
                {
                    var genre = new Genre()
                    {
                        Name = model.Name,
                    };

                    this.context.Genres.Add(genre);
                    this.context.SaveChanges();

                    message = "Успешно добавен жанр!";
                    this.messageService.AddNotificationAtDB(userId, message);
                }
            }

            return message;
        }

        public Dictionary<string,object> EditGenre(AddGenreViewModel model, string userId)
        {
            var result = new Dictionary<string,object>();
            var message = "Жанра се дублира с друг!";
            var a = this.IsDublicated(model);
            if (this.IsDublicated(model) == false)
            {
                var genre = this.context.Genres.FirstOrDefault(g => g.Id == model.Id);
                genre.Name = model.Name;
                this.context.SaveChanges();
                message = "Успешно редактиран жанр!";
                this.messageService.AddNotificationAtDB(userId, message);
            }

            result.Add("model", model);
            result.Add("message", message);
            return result;
        }

        public AddGenreViewModel GetGenreDataById(string genreId)
        {
            var genre = this.context.Genres.FirstOrDefault(g => g.Id == genreId);
            var model = new AddGenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
            };
            return model;
        }

        public AddGenreViewModel PreparedAddGenrePage()
        {
            var model = new AddGenreViewModel();
            return model;
        }

        internal bool IsDublicated(AddGenreViewModel model)
        {
            try
            {
                Genre genre = this.context.Genres.FirstOrDefault(g => g.Name == model.Name && g.DeletedOn == null);
                return genre == null ? false : true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        internal string IsHasNullData(AddGenreViewModel model)
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 4)
            {
                result.Append("Името на жанра трябва да съдържа поне 3 символа!");
            }

            return result.ToString().Trim();
        }
    }
}
