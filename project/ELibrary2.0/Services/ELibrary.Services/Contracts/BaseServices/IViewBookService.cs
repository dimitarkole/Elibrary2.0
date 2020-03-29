namespace ELibrary.Services.Contracts.BaseServices
{ using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Home;
   

    public interface IViewBookService
    {
        ViewBookViewModel PreparedPage(string bookId);

        Dictionary<string, object> ReserveTheBook(string bookId, string userId);

        Dictionary<string, object> AddReview(ViewBookViewModel model, string bookId, string userId);

    }
}
