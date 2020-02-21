namespace ELibrary.Services.Contracts.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Library;

    public interface IGiveBookService
    {
        GiveBookViewModel PreparedPage(string userId);

        GiveBookViewModel GiveBookSearchBook(
            GiveBookViewModel model,
            string userId,
            string selectedBookId,
            string selectedUserId);

        GiveBookViewModel GiveBookChangeBookPage(
            GiveBookViewModel model,
            string userId,
            int newPage,
            string selectedBookId,
            string selectedUserId);

        GiveBookViewModel GiveBookSearchUser(
            GiveBookViewModel model,
            string userId,
            string selectedBookId,
            string selectedUserId);

        GiveBookViewModel GiveBookChangeUserPage(
            GiveBookViewModel model,
            string userId,
            int newPage,
            string selectedBookId,
            string selectedUserId);

        GiveBookViewModel GiveBookSelectedBook(
            GiveBookViewModel model,
            string userId,
            string bookId,
            string selectedUserId);

        GiveBookViewModel GiveBookSelectedUser(
            GiveBookViewModel model,
            string userId,
            string selectUserId,
            string selectedBookId);

        List<object> GivingBook(
          GiveBookViewModel model,
          string userId,
          string selectedBookId,
          string selectedUserId);

    }
}
