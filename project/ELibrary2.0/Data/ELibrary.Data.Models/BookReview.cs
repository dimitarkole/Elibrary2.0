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

        public virtual string Id { get; set; }

        public virtual string BookId { get; set; }

        public virtual Book Book { get; set; }


        [DataType(DataType.MultilineText)]
        public virtual string Review { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
