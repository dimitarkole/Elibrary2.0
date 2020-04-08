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
            this.GetBooks = new HashSet<GetBook>();
        }

        public string Id { get; set; }

        public string CatalogNumber { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string GenreId { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string WhereIsBook { get; set; }

        [DataType(DataType.MultilineText)]
        public string Review { get; set; }

        public string Logo { get; set; }


        [Range(1, 10000)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        public virtual ICollection<GetBook> GetBooks { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
