namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class Book : IAuditInfo, IDeletableEntity
    {
        public Book()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public virtual string Id { get; set; }

        public virtual string CatalogNumber { get; set; }

        public virtual string VirtualOrReal { get; set; }


        public virtual string Title { get; set; }

        public virtual string Author { get; set; }

        public virtual string GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual string WhereIsBook { get; set; }

        [DataType(DataType.MultilineText)]
        public virtual string Review { get; set; }
        
        public virtual string Logo { get; set; }

        public virtual string EBookFile { get; set; }


        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public virtual double Price { get; set; }

        public virtual string Currency { get; set; }


        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
