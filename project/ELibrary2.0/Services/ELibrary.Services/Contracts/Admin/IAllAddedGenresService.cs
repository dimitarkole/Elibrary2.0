namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAllAddedGenresService
    {
        AllAddedGenresViewModel PreparedPage(string userId);

        AllAddedGenresViewModel GetGenres(AllAddedGenresViewModel model, string userId);

        AddGenreViewModel GetGenreData(string genreId);

        AllAddedGenresViewModel DeleteGenre(string userId, AllAddedGenresViewModel model, string genreId);

        AllAddedGenresViewModel ChangeActivePage(AllAddedGenresViewModel model, string userId, int newPage);
    }
}
