namespace ELibrary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Data.Common.Models;

    public class PaymentPlan : IAuditInfo, IDeletableEntity
    {
        public PaymentPlan()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public double PriceOneYear { get; set; }

        public double PriceTwoYears { get; set; }

        public string Text { get; set; }

        public int CountBook { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }
    }
}
