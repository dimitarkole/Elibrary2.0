namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NotificationViewModel
    {
        public string Id { get; set; }

        public string TextOfMessage { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? SeenOn { get; set; }
    }
}
