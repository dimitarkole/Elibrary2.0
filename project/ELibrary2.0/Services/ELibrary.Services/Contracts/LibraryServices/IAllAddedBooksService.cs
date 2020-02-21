namespace ELibrary.Services.Contracts.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ELibrary.Web.ViewModels.Library;
    using ELibrary.Web.ViewModels.User;

    public interface IAllAddedBooksService
    {
        AllAddedBooksViewModel PreparedPage(string userId);

        AllAddedBooksViewModel GetBooks(AllAddedBooksViewModel model, string userId);

        AllAddedBooksViewModel DeleteBook(string userId, AllAddedBooksViewModel model, string bookId);

        AllAddedBooksViewModel ChangeActivePage(AllAddedBooksViewModel model, string userId, int newPage);
    }
}
