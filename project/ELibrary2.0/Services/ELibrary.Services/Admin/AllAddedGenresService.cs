namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;

    public class AllAddedGenresService : IAllAddedGenresService
    {
        private ApplicationDbContext context;

        private INotificationService messageService;

        public AllAddedGenresService(
            ApplicationDbContext context,
            INotificationService messageService)
        {
            this.context = context;
            this.messageService = messageService;
        }

        public AllAddedGenresViewModel ChangeActivePage(AllAddedGenresViewModel model, string userId, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetGenres(model, userId);
        }

        public AllAddedGenresViewModel DeleteGenre(string userId, AllAddedGenresViewModel model, string genreId)
        {
            var deleteGenre = this.context.Genres.FirstOrDefault(g => g.Id == genreId);
            if (deleteGenre != null)
            {
                deleteGenre.DeletedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                string result = "Успешно изтрит жанр!";
                this.messageService.AddNotificationAtDB(userId, result);
            }

            var returnModel = this.GetGenres(model, userId);
            return returnModel;
        }

        public AllAddedGenresViewModel GetGenres(AllAddedGenresViewModel model, string userId)
        {
            var genreName = model.SearchGenre.Name;
            var sortMethodId = model.SortMethodId;
            var countBooksOfPage = model.CountGenresOfPage;
            var currentPage = model.CurrentPage;

            var genres = this.context.Genres.Where(g =>
              g.DeletedOn == null)
              .Select(g => new AddedGenreViewModel()
              {
                 Id = g.Id,
                 Name = g.Name,
              });

            genres = this.SelectGenres(genreName, genres);
            genres = this.SortGenres(sortMethodId, genres);

            int maxCountPage = genres.Count() / countBooksOfPage;
            if (genres.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewGenres = genres.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage);
            var searchGenre = new AddedGenreViewModel()
            {
                Name = genreName,
            };

            var returnModel = new AllAddedGenresViewModel()
            {
                SearchGenre = searchGenre,
                SortMethodId = sortMethodId,
                Genres = viewGenres,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
                CountGenresOfPage = countBooksOfPage,
            };
            return returnModel;
        }

        public AllAddedGenresViewModel PreparedPage(string userId)
        {
            var model = new AllAddedGenresViewModel();
            var returnModel = this.GetGenres(model, userId);
            return returnModel;
        }

        public AddGenreViewModel GetGenreData(string genreId)
        {
            var genre = this.context.Genres
                .FirstOrDefault(g => g.Id == genreId);
            AddGenreViewModel model = new AddGenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return model;
        }

        private IQueryable<AddedGenreViewModel> SortGenres(
          string sortMethodId,
          IQueryable<AddedGenreViewModel> genres)
        {
            if (sortMethodId == "Име а-я")
            {
                genres = genres.OrderByDescending(g => g.Name);
            }
            else
            {
                genres = genres.OrderBy(g => g.Name);
            }

            return genres;
        }

        private IQueryable<AddedGenreViewModel> SelectGenres(
          string genreName,
          IQueryable<AddedGenreViewModel> genres)
        {
            if (genreName != null)
            {
                genres = genres.Where(g => g.Name.Contains(genreName));
            }

            return genres;
        }
    }
}
