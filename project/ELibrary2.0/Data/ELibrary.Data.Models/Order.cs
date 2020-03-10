using ELibrary.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Data.Models
{
    public class Order : IAuditInfo, IDeletableEntity
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string PaymentPlanId { get; set; }

        public virtual PaymentPlan PaymentPlan { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime? ЕxpiryDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

    }
}
