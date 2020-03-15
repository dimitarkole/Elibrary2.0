namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;

    public class AllPaymentPlansService : IAllPaymentPlansService
    {
        private ApplicationDbContext context;

        private INotificationService messageService;

        public AllPaymentPlansService(
            ApplicationDbContext context,
            INotificationService messageService)
        {
            this.context = context;
            this.messageService = messageService;
        }

        public AllPaymentPlansViewModel ChangeActivePage(AllPaymentPlansViewModel model, string userId, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetPaymentPlans(model, userId);
        }

        public AllPaymentPlansViewModel DeletePaymentPlan(string userId, AllPaymentPlansViewModel model, string paymentPlanId)
        {
            var deletePaymentPlan = this.context.PaymentPlans.FirstOrDefault(p => p.Id == paymentPlanId);
            if (deletePaymentPlan != null)
            {
                deletePaymentPlan.DeletedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                string result = "Успешно изтрит абонаментен план!";
                this.messageService.AddNotificationAtDB(userId, result);
            }

            var returnModel = this.GetPaymentPlans(model, userId);
            return returnModel;
        }

        public AllPaymentPlansViewModel GetPaymentPlans(AllPaymentPlansViewModel model, string userId)
        {
            var paymentPlanName = model.SearchPaymentPlan.Name;
            var sortMethodId = model.SortMethodId;
            var countBooksOfPage = model.CountPaymentPlanOfPage;
            var currentPage = model.CurrentPage;

            var plans = this.context.PaymentPlans.Where(p => p.DeletedOn == null)
              .Select(p => new PaymentPlanViewModel()
              {
                  CountBook = p.CountBook,
                  CreatedOn = p.CreatedOn,
                  Id = p.Id,
                  Name = p.Name,
                  PriceOneYear = p.PriceOneYear,
                  PriceTwoYears = p.PriceTwoYears,
                  Text = p.Text,
              });

            plans = this.SelectPlans(paymentPlanName, plans);

            plans = this.SortPlans(sortMethodId, plans);

            int maxCountPage = plans.Count() / countBooksOfPage;
            if (plans.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewBook = plans.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage);
            var searchPlan = new PaymentPlanViewModel()
            {
                Name = paymentPlanName,
            };

            var returnModel = new AllPaymentPlansViewModel()
            {
                CountPaymentPlanOfPage =countBooksOfPage,
                PaymentPlans = plans,
                SearchPaymentPlan = searchPlan,
                SortMethodId = sortMethodId,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
            };
            return returnModel;
        }

        public AllPaymentPlansViewModel PreparedPage(string userId)
        {
            var model = new AllPaymentPlansViewModel();
            var returnModel = this.GetPaymentPlans(model, userId);
            return returnModel;
        }

        public AddPaymentPlanViewModel GetPaymentPlanData(string planId)
        {
            var paymentPlan = this.context.PaymentPlans
                .FirstOrDefault(p => p.Id == planId);
            AddPaymentPlanViewModel model = new AddPaymentPlanViewModel()
            {
                CountBook = paymentPlan.CountBook,
                Id = paymentPlan.Id,
                Name = paymentPlan.Name,
                PriceOneYear = paymentPlan.PriceOneYear,
                PriceTwoYears = paymentPlan.PriceTwoYears,
                Text = paymentPlan.Text,
            };

            return model;
        }

        private IQueryable<PaymentPlanViewModel> SortPlans(
           string sortMethodId,
           IQueryable<PaymentPlanViewModel> paymentPlans)
        {
            if (sortMethodId == "Име я-а")
            {
                paymentPlans = paymentPlans.OrderByDescending(p => p.Name);
            }
            else 
            {
                paymentPlans = paymentPlans.OrderBy(p => p.Name);
            }

            return paymentPlans;
        }

        private IQueryable<PaymentPlanViewModel> SelectPlans(
          string paymentPlanName,
          IQueryable<PaymentPlanViewModel> paymentPlans)
        {
            if (paymentPlanName != null)
            {
                paymentPlans = paymentPlans.Where(b => b.Name.Contains(paymentPlanName));
            }

            return paymentPlans;
        }
    }
}
