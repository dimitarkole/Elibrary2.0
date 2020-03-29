namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class Notification : IAuditInfo, IDeletableEntity
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

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
    }
}
