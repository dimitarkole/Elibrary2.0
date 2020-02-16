namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NotificationsNavBarViewModel
    {
        public NotificationsNavBarViewModel(List<NotificationNavBarViewModel> messages)
        {
            this.Messages = messages;
            this.CountNotification = messages.Count;
        }
        public NotificationsNavBarViewModel()
        {
            this.Messages = new List<NotificationNavBarViewModel>();
            this.CountNotification = 0;
        }

        public int CountNotification { get; set; }

        public List<NotificationNavBarViewModel> Messages { get; set; }
    }
}
