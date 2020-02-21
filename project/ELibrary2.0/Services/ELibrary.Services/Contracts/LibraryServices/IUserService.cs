namespace ELibrary.Services.Contracts.LibraryServices
{
    using ELibrary.Web.ViewModels.Library;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public interface IUserService
    {
        AllUsersViewModel PreparedPage();

        AllUsersViewModel GetUsers(AllUsersViewModel model);

        AllUsersViewModel ChangeActivePage(AllUsersViewModel model, int newPage);
    }
}
