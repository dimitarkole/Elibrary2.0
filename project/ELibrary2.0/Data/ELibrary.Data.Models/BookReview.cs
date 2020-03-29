namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class BookReview : IAuditInfo, IDeletableEntity
    {
        public BookReview()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string BookId { get; set; }

        public virtual Book Book { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [DataType(DataType.MultilineText)]
        public string Review { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
