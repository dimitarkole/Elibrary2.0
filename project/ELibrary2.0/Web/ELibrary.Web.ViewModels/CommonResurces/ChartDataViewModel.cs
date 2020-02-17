namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChartDataViewModel
    {
        public ChartDataViewModel(string dimensionOne, int quantity)
        {
            this.DimensionOne = dimensionOne;
            this.Quantity = quantity;
        }

        public string DimensionOne { get; set; }

        public int Quantity { get; set; }
    }
}
