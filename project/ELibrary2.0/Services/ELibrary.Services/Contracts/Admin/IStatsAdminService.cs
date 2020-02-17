namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IStatsAdminService
    {
        StatsAdminViewModel PreparedPage(string userId);

        StatsAdminViewModel SearchStats(StatsAdminViewModel model, string userId);
    }
}
