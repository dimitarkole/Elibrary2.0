namespace ELibrary.Services.Contracts.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Home;

    public interface IViewLibraryService
    {
        ViewLibraryViewModel PreparedPage(string libraryId);

        ViewLibraryViewModel GetLibraryData(ViewLibraryViewModel model, string libraryId);

        ViewLibraryViewModel ChangeActiveBookPage(ViewLibraryViewModel model, int newPage,string libraryId);

    }
}
