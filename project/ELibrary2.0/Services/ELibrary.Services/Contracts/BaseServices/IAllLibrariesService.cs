namespace ELibrary.Services.Contracts.BaseServices
{
    using ELibrary.Web.ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public interface IAllLibrariesService
    {
        AllLibrariesViewModel PreparedPage();

        AllLibrariesViewModel GetLibrarys(AllLibrariesViewModel model);

        AllLibrariesViewModel ChangeActivePage(AllLibrariesViewModel model, int newPage);
    }
}
