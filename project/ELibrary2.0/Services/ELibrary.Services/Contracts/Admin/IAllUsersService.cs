namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAllUsersService
    {
        AllUsersViewModel PreparedPage();

        List<object> MakeUserAdmin(AllUsersViewModel model, string userId);

        List<object> MakeAdminUser(AllUsersViewModel model, string userId);


        List<object> DeleteUser(AllUsersViewModel model, string userId, string adminId);

        AllUsersViewModel ChangeActivePage(
         AllUsersViewModel model,
         int newPage);
    }
}
