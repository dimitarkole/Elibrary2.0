namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Models;

    public interface IRoleService
    {
        string GetUserRole(ApplicationUser user);

    }
}
