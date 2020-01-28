namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class GetBook : IAuditInfo, IDeletableEntity
    {
        public GetBook()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public virtual string Id { get; set; }

        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }


        public virtual string BookId { get; set; }

        public virtual Book Book { get; set; }


        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? ReturnedOn { get; set; }

        public virtual DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

    }
}
