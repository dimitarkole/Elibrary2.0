namespace ELibrary.Services.Contracts.CommonResurcesServices
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INotificationService
    {
        string AddNotificationAtDB(string userId, string textOfNotification);

        NotificationsNavBarViewModel GetNotificationsNavBar(string userId);

        NotificationsViewModel GetNotificationsPreparedPage(string userId);


        NotificationsViewModel GetNotificationsChangePage(NotificationsViewModel model, string userId, int pageIndex);

    }
}
