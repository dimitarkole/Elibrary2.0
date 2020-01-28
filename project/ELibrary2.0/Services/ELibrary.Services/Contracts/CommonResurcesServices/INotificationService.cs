namespace ELibrary.Services.Contracts.CommonResurcesServices
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INotificationService
    {
        string AddMessageAtDB(string userId, string textOfMessage);

        NotificationNavBarViewModel GetMessagesNavBar(string userId);

        NotificationsViewModel GetMessagesPreparedPage(string userId);


        NotificationsViewModel GetMessagesChangePage(NotificationsViewModel model, string userId, int pageIndex);

    }
}
