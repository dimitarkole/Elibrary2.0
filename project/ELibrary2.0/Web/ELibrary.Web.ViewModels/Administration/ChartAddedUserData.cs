namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ChartAddedUserData
    {
        public ChartAddedUserData(string mounth, int countAllUsers, int countAdmins, int countLibrarys)
        {
            this.Mounth = mounth;
            this.CountAllUsers = countAllUsers;
            this.CountAdmins = countAdmins;
            this.CountLibrarys = countLibrarys;
        }

        public string Mounth { get; set; }

        public int CountAllUsers { get; set; }

        public int CountAdmins { get; set; }

        public int CountLibrarys { get; set; }

    }
}
