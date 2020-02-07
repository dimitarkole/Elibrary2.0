namespace ELibrary.Services.Contracts.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.User;

    public interface ITakenBooksService
    {
        TakenBooksViewModel PreparedPage(string userId);

        TakenBooksViewModel TakenBooks(TakenBooksViewModel model, string userId);

        TakenBooksViewModel ChangeActivePage(TakenBooksViewModel model, string userId, int newPage);
    }
}
