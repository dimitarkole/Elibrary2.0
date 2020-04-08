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

        public Dictionary<string, object> EditPaymentPlan(AddPaymentPlanViewModel model, string userId)
        {
            var name = model.Name;
            var countBook = model.CountBook;
            var paymentId = model.Id;
            var message = this.IsHasNullData(model);
            var result = new Dictionary<string, object>();
            result.Add("model", model);
            if (string.IsNullOrEmpty(message))
            {
                message = this.CheckDublicatePaymentAdd(model.Name, model.CountBook);
                if (string.IsNullOrEmpty(message))
                {
                    var paymentPlan = this.context.PaymentPlans.FirstOrDefault(p => p.Id == paymentId);
                    paymentPlan.Name = name;
                    paymentPlan.PriceOneYear = model.PriceOneYear;
                    paymentPlan.PriceTwoYears = model.PriceTwoYears;
                    paymentPlan.Text = model.Text;
                    paymentPlan.CountBook = model.CountBook;

                    this.context.SaveChanges();
                    message = "Успешно редактиран абонаментен план!";
                    this.messageService.AddNotificationAtDB(userId, message);
                }
            }

            result.Add("message", message);
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
            var message = this.IsHasNullData(model);
            if (string.IsNullOrEmpty(message))
            {
                message = this.CheckDublicatePaymentAdd(model.Name, model.CountBook);
                if (string.IsNullOrEmpty(message))
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
                    message = "Успешно добавен абонаментен план!";
                    this.messageService.AddNotificationAtDB(userId, message);
                }
            }
            return message;
        }

        private string CheckDublicatePaymentEdit(string name, int countBook, string paymentPlanId)
        {
            var checkPaymentPlanName = this.context.PaymentPlans.FirstOrDefault(p => p.Id != paymentPlanId && p.Name == name && p.DeletedOn == null);
            var checkPaymentPlanCountBook = this.context.PaymentPlans.FirstOrDefault(p => p.Id != paymentPlanId && p.CountBook == countBook && p.DeletedOn == null);
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

        internal string CheckDublicatePaymentAdd(string name, int countBook)
        {
            var checkPaymentPlanName = this.context.PaymentPlans.FirstOrDefault(p => p.Name == name && p.DeletedOn == null);
            var checkPaymentPlanCountBook = this.context.PaymentPlans.FirstOrDefault(p => p.CountBook == countBook && p.DeletedOn == null);
            StringBuilder result = new StringBuilder();
            if (checkPaymentPlanName != null)
            {
                result.Append("Името на плана се дублира!");
            }

            if (checkPaymentPlanCountBook != null)
            {
                result.Append("Броя на книгите се доблира с друг план!");
            }

            return result.ToString().Trim();
        }

        internal string IsHasNullData(AddPaymentPlanViewModel model)
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrWhiteSpace(model.Name) || model.Name.Length < 4)
            {
                result.Append("Името на абонаметния план трябва да съдържа поне 3 символа!");
            }

            if (model.PriceTwoYears <= model.PriceOneYear)
            {
                result.Append("Двугодишната цена трябва да бъде по-голяма от едногодишната!");
            }

            if (model.PriceOneYear <= 0)
            {
                result.Append("Цената на едногодишния абонамента трябва да бъде не отрицателно число!");
            }

            if (model.PriceTwoYears <= 0)
            {
                result.Append("Цената на двугодишния абонамента трябва да бъде не отрицателно число!");
            }

            if (string.IsNullOrEmpty(model.Text) || string.IsNullOrWhiteSpace(model.Text) || model.Text.Length < 4)
            {
                result.Append("Текстът към абонаметния план трябва да съдържа поне 3 символа!");
            }

            if (model.CountBook < 1)
            {
                result.Append("Броя на книгите трябва да бъде по-голям от 0!");
            }

            return result.ToString().Trim();
        }
    }
}
