namespace ELibrary.Services.Contracts.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Library;

    public interface IStatsLibraryService
    {
        StatsLibraryViewModel PreparedPage(string userId);

        StatsLibraryViewModel SearchStats(StatsLibraryViewModel model, string userId);
    }
}
