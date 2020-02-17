namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Models;
    using ELibrary.Web.ViewModels.CommonResurces;

    public class StatsAdminViewModel
    {
        public StatsAdminViewModel()
        {
            this.SearchUser = new ApplicationUser();
        }

        public ApplicationUser SearchUser { get; set; }

        public ChartAddedUsers ChartAddedUsers { get; set; }

        public ChartViewModel ChartAddedBookSinceSixМonth { get; set; }
    }
}
