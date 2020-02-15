namespace ELibrary.Services.Contracts.BaseServices
{
    using ELibrary.Web.ViewModels.Home;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IViewBookService
    {
        ViewBookViewModel PreparedPage(string bookId);

    }
}
