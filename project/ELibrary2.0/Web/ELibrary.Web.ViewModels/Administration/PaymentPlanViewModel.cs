namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaymentPlanViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double PriceOneYear { get; set; }

        public double PriceTwoYears { get; set; }

        public string Text { get; set; }

        public int CountBook { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
