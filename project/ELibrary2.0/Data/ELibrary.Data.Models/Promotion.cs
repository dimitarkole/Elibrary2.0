namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class Promotion : IAuditInfo, IDeletableEntity
    {
        public Promotion()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public double Value { get; set; }

        public string Currency { get; set; }

        public DateTime? ЕxpiryDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

    }
}
