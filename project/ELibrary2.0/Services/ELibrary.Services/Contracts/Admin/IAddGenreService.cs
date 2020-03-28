namespace ELibrary.Services.Contracts.Admin
{
    using ELibrary.Web.ViewModels.Administration;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAddGenreService
    {
        AddGenreViewModel PreparedAddBookPage();

        string AddBook(AddGenreViewModel model, string userId);

        AddGenreViewModel GetBookDataById(string bookId);

        List<object> EditBook(AddGenreViewModel model, string userId);
    }
}
