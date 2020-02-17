namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChartAddedUsers
    {
        public ChartAddedUsers()
        {
            this.ChartData = new List<ChartAddedUserData>();
        }

        public ChartAddedUsers(string titlle, List<ChartAddedUserData> chartData)
        {
            this.Titlle = titlle;
            this.ChartData = chartData;
        }

        public ChartAddedUsers(string titlle)
        {
            this.Titlle = titlle;
            this.ChartData = new List<ChartAddedUserData>();
        }

        public string Titlle { get; set; }

        public string StackedDimensionOne { get; set; }

        public List<ChartAddedUserData> ChartData { get; set; }
    }
}
