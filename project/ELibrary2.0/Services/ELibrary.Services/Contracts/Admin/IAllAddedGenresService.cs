using ELibrary.Web.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Services.Contracts.Admin
{
    public interface IAllAddedGenresService
    {
        AllAddedGenresViewModel PreparedPage(string userId);

        AllAddedGenresViewModel GetGenres(AllAddedGenresViewModel model, string userId);

        AllAddedGenresViewModel DeleteGenre(string userId, AllAddedGenresViewModel model, string genreId);

        AllAddedGenresViewModel ChangeActivePage(AllAddedGenresViewModel model, string userId, int newPage);
    }
}
