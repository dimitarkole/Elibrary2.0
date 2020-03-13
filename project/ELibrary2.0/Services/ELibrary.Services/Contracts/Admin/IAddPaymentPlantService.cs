namespace ELibrary.Services.Contracts.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Administration;

    public interface IAddPaymentPlantService
    {
        public AddPaymentPlantViewModel PreparedAddPaymentPlantPage();

        public string AddPaymentPlant(AddPaymentPlantViewModel model, string userId);

        public AddPaymentPlantViewModel GetPaymentPlantDataById(string planId);

        public List<object> EditPaymentPlant(AddPaymentPlantViewModel model, string userId);

    }
}
