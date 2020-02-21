namespace ELibrary.Services.Contracts.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Library;

    public interface IGivenBooksService
    {
        GivenBooksViewModel PreparedPage(string userId);

        GivenBooksViewModel GetGevenBooks(
            GivenBooksViewModel model,
            string userId);

        List<object> ReturningBook(
            GivenBooksViewModel model,
            string userId,
            string givenBookId);

        List<object> DeletingBook(
           GivenBooksViewModel model,
           string userId,
           string givenBookId);

        GivenBooksViewModel ChangeActivePage(
            GivenBooksViewModel model,
            string userId,
            int newPage);

        List<object> SendMessageForReturningBook(
            GivenBooksViewModel model,
            string userId,
            string givenBookId);
    }
}
