using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.Administration
{
    public class AllPaymentPlansViewModel
    {
        public AllPaymentPlansViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Име а-я");
            this.SortMethods.Add("Име я-а");
            this.SortMethodId = this.SortMethods[0];

            this.CountPaymentPlanOfPageList = new List<int>();
            this.CountPaymentPlanOfPageList.Add(10);
            this.CountPaymentPlanOfPageList.Add(15);
            this.CountPaymentPlanOfPageList.Add(20);

            this.CountPaymentPlanOfPage = this.CountPaymentPlanOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;
            this.SearchPaymentPlan = new PaymentPlanViewModel();
            this.PaymentPlans = new List<PaymentPlanViewModel>();
        }

        public PaymentPlanViewModel SearchPaymentPlan { get; set; }

        public string SortMethodId { get; set; }

        public List<string> SortMethods { get; set; }

        public IEnumerable<PaymentPlanViewModel> PaymentPlans { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountPaymentPlanOfPage { get; set; }

        public List<int> CountPaymentPlanOfPageList { get; set; }
    }
}
