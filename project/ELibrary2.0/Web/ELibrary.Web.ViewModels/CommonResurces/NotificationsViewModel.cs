using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.CommonResurces
{
    public class NotificationsViewModel
    {
        public NotificationsViewModel()
        {
            this.CountMessagesOfPageList = new List<int>();

            this.CountMessagesOfPageList.Add(10);
            this.CountMessagesOfPageList.Add(20);
            this.CountMessagesOfPageList.Add(30);

            this.CountMessagesOfPage = this.CountMessagesOfPageList[0];
            this.CurrentPage = 1;
        }

        public List<NotificationViewModel> Messages { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountMessagesOfPage { get; set; }

        public List<int> CountMessagesOfPageList { get; set; }
    }
}
