namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAddPaymentPlanService
    {
        public AddPaymentPlanViewModel PreparedAddPaymentPlanPage();

        public string AddPaymentPlan(AddPaymentPlanViewModel model, string userId);

        public AddPaymentPlanViewModel GetPaymentPlanDataById(string planId);

        public List<object> EditPaymentPlan(AddPaymentPlanViewModel model, string userId);

    }
}
