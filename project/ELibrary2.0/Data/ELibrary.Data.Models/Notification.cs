﻿namespace ELibrary.Data.Models
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

        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual string TextOfNotification { get; set; }

        public virtual DateTime CreatedOn { get; set; }
        public virtual DateTime? SeenOn { get; set; }

        public virtual DateTime? DeletedOn { get; set; }
    }
}
