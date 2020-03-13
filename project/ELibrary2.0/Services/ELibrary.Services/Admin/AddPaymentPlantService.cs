using ELibrary.Data;
using ELibrary.Data.Models;
using ELibrary.Services.Contracts.Admin;
using ELibrary.Services.Contracts.CommonResurcesServices;
using ELibrary.Web.ViewModels.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELibrary.Services.Admin
{
    public class AddPaymentPlantService : IAddPaymentPlantService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        public AddPaymentPlantService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
        }

        public List<object> EditPaymentPlant(AddPaymentPlantViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public AddPaymentPlantViewModel GetPaymentPlantDataById(string planId)
        {
            var payment = this.context.PaymentPlans.FirstOrDefault(p => p.Id == planId);
            var model = new AddPaymentPlantViewModel()
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

        public AddPaymentPlantViewModel PreparedAddPaymentPlantPage()
        {
            var model = new AddPaymentPlantViewModel();
            return model;
        }

        public string AddPaymentPlant(AddPaymentPlantViewModel model, string userId)
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
            return "Успешно добавен план!";
        }
    }
}
