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
            this.CountMessages = messages.Count;
        }

        public int CountMessages { get; set; }

        public List<NotificationNavBarViewModel> Messages { get; set; }
    }
}
