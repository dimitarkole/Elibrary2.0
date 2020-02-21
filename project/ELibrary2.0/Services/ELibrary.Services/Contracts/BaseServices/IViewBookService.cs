namespace ELibrary.Services.Contracts.BaseServices
{ using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Home;
   

    public interface IViewBookService
    {
        ViewBookViewModel PreparedPage(string bookId);

        //ViewBookViewModel AddReview(string bookId);

    }
}
