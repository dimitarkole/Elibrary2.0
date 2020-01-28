namespace ELibrary.Services.Contracts.UserServices
{
    using ELibrary.Web.ViewModels.User;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IAddBookService
    {
        AddBookViewModel PreparedAddBookPage();

    }
}
