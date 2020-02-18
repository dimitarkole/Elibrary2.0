using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.User
{
    public class ChartGettenBookSinceSixМonth
    {
        public ChartGettenBookSinceSixМonth()
        {
            this.ChartData = new List<ChartGettenBookSinceSixМonthData>();
        }

        public ChartGettenBookSinceSixМonth(string titlle, List<ChartGettenBookSinceSixМonthData> chartData)
        {
            this.Titlle = titlle;
            this.ChartData = chartData;
        }

        public ChartGettenBookSinceSixМonth(string titlle)
        {
            this.Titlle = titlle;
            this.ChartData = new List<ChartGettenBookSinceSixМonthData>();
        }

        public string Titlle { get; set; }

        public string StackedDimensionOne { get; set; }

        public List<ChartGettenBookSinceSixМonthData> ChartData { get; set; }
    }
}
