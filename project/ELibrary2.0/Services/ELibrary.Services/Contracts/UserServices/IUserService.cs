namespace ELibrary.Services.Contracts.UserServices
{
    using ELibrary.Web.ViewModels.User;
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
