using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.CommonResurces
{
    public class NotificationsViewModel
    {
        public NotificationsViewModel()
        {
            this.CountNotificationsOfPageList = new List<int>();

            this.CountNotificationsOfPageList.Add(10);
            this.CountNotificationsOfPageList.Add(20);
            this.CountNotificationsOfPageList.Add(30);

            this.CountNotificationsOfPage = this.CountNotificationsOfPageList[0];
            this.CurrentPage = 1;
        }

        public List<NotificationViewModel> Notifications { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountNotificationsOfPage { get; set; }

        public List<int> CountNotificationsOfPageList { get; set; }
    }
}
