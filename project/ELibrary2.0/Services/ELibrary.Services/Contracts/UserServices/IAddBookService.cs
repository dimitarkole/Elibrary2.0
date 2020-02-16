namespace ELibrary.Services.Contracts.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.User;

    public interface IAddBookService
    {
        AddBookViewModel PreparedAddBookPage();

        string AddBook(AddBookViewModel model, string userId);

        AddBookViewModel GetBookDataById(string genreId);

        List<object> EditBook(AddBookViewModel model, string userId);
    }
}
