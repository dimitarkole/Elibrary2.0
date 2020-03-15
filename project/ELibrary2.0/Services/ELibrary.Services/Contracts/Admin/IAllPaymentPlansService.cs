namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAllPaymentPlansService
    {
        AllPaymentPlansViewModel PreparedPage(string userId);

        AddPaymentPlanViewModel GetPaymentPlanData(string planId);

        AllPaymentPlansViewModel GetPaymentPlans(AllPaymentPlansViewModel model, string userId);

        AllPaymentPlansViewModel DeletePaymentPlan(string userId, AllPaymentPlansViewModel model, string paymentPlanId);

        AllPaymentPlansViewModel ChangeActivePage(AllPaymentPlansViewModel model, string userId, int newPage);
    }
}
