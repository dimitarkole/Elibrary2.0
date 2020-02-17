namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAllUsersService
    {
        AllUsersViewModel PreparedPage();

        Dictionary<string, object> MakeUserAdmin(string userId);

        Dictionary<string, object> MakeAdminUser(string userId);


        Dictionary<string, object> DeleteUser(AllUsersViewModel model, string userId, string adminId);

        AllUsersViewModel ChangeActivePage(
         AllUsersViewModel model,
         int newPage);
    }
}
