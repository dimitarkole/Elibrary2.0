namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;

    public class AddPaymenPlantService : IAddPaymentPlanService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AddPaymenPlantService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public List<object> EditPaymentPlan(AddPaymentPlanViewModel model, string userId)
        {
            var name = model.Name;
            var countBook = model.CountBook;
            var paymentId = model.Id;
            var checkResult = this.CheckDublicatePaymentEdit(name, countBook, paymentId);
            var result = new List<object>();
            result.Add(model);
            if (checkResult == null)
            {
                var paymentPlan = this.context.PaymentPlans.FirstOrDefault(p => p.Id == paymentId);
                paymentPlan.Name = name;
                paymentPlan.PriceOneYear = model.PriceOneYear;
                paymentPlan.PriceTwoYears = model.PriceTwoYears;
                paymentPlan.Text = model.Text;
                paymentPlan.CountBook = model.CountBook;

                this.context.SaveChanges();
                checkResult = "Успешно редактиран абонаментен план!";
                this.messageService.AddNotificationAtDB(userId, checkResult);
            }

            result.Add(checkResult);
            return result;
        }

        public AddPaymentPlanViewModel GetPaymentPlanDataById(string planId)
        {
            var payment = this.context.PaymentPlans.FirstOrDefault(p => p.Id == planId);
            var model = new AddPaymentPlanViewModel()
            {
                Id = payment.Id,
                Name = payment.Name,
                CountBook = payment.CountBook,
                PriceOneYear = payment.PriceOneYear,
                PriceTwoYears = payment.PriceTwoYears,
                Text = payment.Text,
            };
            return model;
        }

        public AddPaymentPlanViewModel PreparedAddPaymentPlanPage()
        {
            var model = new AddPaymentPlanViewModel();
            return model;
        }

        public string AddPaymentPlan(AddPaymentPlanViewModel model, string userId)
        {
            PaymentPlan paymentPlan = new PaymentPlan()
            {
                CountBook = model.CountBook,
                Name = model.Name,
                PriceOneYear = model.PriceOneYear,
                PriceTwoYears = model.PriceTwoYears,
                Text = model.Text,
            };
            this.context.PaymentPlans.Add(paymentPlan);
            this.context.SaveChanges();
            var message = "Успешно добавен абонаментен план!";
            this.messageService.AddNotificationAtDB(userId, message);
            return message;
        }

        private string CheckDublicatePaymentEdit(string name, int countBook, string paymentPlanId)
        {
            var checkPaymentPlanName = this.context.PaymentPlans.FirstOrDefault(p => p.Name == name && p.DeletedOn == null);
            var checkPaymentPlanCountBook = this.context.PaymentPlans.FirstOrDefault(p => p.CountBook == countBook && p.DeletedOn == null);
            if (checkPaymentPlanName == null)
            {
                if (checkPaymentPlanCountBook == null)
                {
                    return null;
                }

                return "Броя на книгите се доблира с друг план!";
            }

            return "Името на плана се повтаря!";
        }
    }
}
