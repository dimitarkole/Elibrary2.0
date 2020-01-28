namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class Genre : IAuditInfo, IDeletableEntity
    {
        public Genre()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public virtual string Id { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }
        
        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
