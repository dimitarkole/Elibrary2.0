namespace ELibrary.Services.Contracts.Admin
{
    using ELibrary.Web.ViewModels.Administration;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAddGenreService
    {
        AddGenreViewModel PreparedAddGenrePage();

        string AddGenre(AddGenreViewModel model, string userId);

        AddGenreViewModel GetGenreDataById(string bookId);

        List<object> EditGenre(AddGenreViewModel model, string userId);
    }
}
