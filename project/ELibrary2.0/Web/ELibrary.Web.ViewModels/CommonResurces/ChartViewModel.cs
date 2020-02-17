namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChartViewModel
    {
        public ChartViewModel()
        {
            this.ChartData = new List<ChartDataViewModel>();
        }

        public ChartViewModel(string titlle, List<ChartDataViewModel> chartData)
        {
            this.Titlle = titlle;
            this.ChartData = chartData;
        }

        public ChartViewModel(string titlle)
        {
            this.Titlle = titlle;
            this.ChartData = new List<ChartDataViewModel>();
        }

        public string Titlle { get; set; }

        public string StackedDimensionOne { get; set; }

        public List<ChartDataViewModel> ChartData { get; set; }
    }
}
