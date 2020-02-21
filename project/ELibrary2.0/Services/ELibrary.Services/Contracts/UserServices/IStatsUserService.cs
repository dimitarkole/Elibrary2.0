using ELibrary.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Services.Contracts.UserServices
{
    public interface IStatsUserService
    {
        StatsUserViewModel PreparedPage(string userId);

        StatsUserViewModel SearchStats(StatsUserViewModel model, string userId);
    }
}
