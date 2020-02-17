namespace ELibrary.Services.Contracts.CommonResurcesServices
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IProfileService
    {
        ProfilViewModel PreparedPage(string userId);

        ProfilViewModel SaveChanges(ProfilViewModel model, string userId);

        List<object> ResetPassword(ProfilViewModel model, string userId);
    }
}
