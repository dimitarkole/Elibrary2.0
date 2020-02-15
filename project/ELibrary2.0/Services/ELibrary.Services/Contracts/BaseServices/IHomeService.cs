namespace ELibrary.Services.Contracts.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.User;

    public interface IHomeService
    {
        AllAddedBooksViewModel PreparedPage();

        AllAddedBooksViewModel GetBooks(AllAddedBooksViewModel model);

        AllAddedBooksViewModel ChangeActivePage(AllAddedBooksViewModel model, int newPage);
    }
}
