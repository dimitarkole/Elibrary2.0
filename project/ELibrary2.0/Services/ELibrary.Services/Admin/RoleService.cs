namespace ELibrary.Services.Admin
{
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RoleService : IRoleService
    {
        private ApplicationDbContext context;

        public RoleService(
            ApplicationDbContext context)
        {
            this.context = context;
        }

        public string GetUserRole(ApplicationUser user)
        {
            try
            {
                var roleId = this.context.UserRoles
               .FirstOrDefault(ur => ur.UserId == user.Id).RoleId;
                var roleName = this.context.Roles
                    .FirstOrDefault(r => r.Id == roleId).Name;
                return roleName;
            }
            catch (Exception)
            {

                return "User";

            }

        }
    }
}
