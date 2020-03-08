namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string TextOfNotification { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? SeenOn { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
