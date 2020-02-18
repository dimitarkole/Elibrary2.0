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
    using ELibrary.Web.ViewModels.CommonResurces;

    public class StatsAdminService : IStatsAdminService
    {
        private ApplicationDbContext context;


        private IRoleService roleService;

        public StatsAdminService(
            ApplicationDbContext context,
            IRoleService roleService)
        {
            this.context = context;
            this.roleService = roleService;
        }

        public StatsAdminViewModel PreparedPage(string userId)
        {
            var model = new StatsAdminViewModel();
            var returnModel = this.SearchStats(model, userId);
            return returnModel;
        }

        public StatsAdminViewModel SearchStats(StatsAdminViewModel model, string userId)
        {
            var searchUser = model.SearchUser;
            var returnModel = new StatsAdminViewModel()
            {
                SearchUser = searchUser,
                ChartAddedUsers = this.GetDataChartAddedUsers(searchUser),
                ChartAddedBookSinceSixМonth = this.ChartAddedBookSinceSixМonth(),
            };
            return returnModel;
        }

        private ChartAddedUsers GetDataChartAddedUsers(ApplicationUser searchUser)
        {
            var chartData = new List<ChartAddedUserData>();

            var groups = this.context.Users
              .Where(u =>
                    u.DeletedOn == null)
              .Select(u => new UserData()
              {
                  Type = this.roleService.GetUserRole(u),
                  CreatedOn = u.CreatedOn,
              })
              .ToList()
              .GroupBy(u => u.CreatedOn.Year + " " + u.CreatedOn.Month)
              .Take(6)
              .ToList();

            foreach (var group in groups)
            {
                List<UserData> addedUsersOfMonth = group.Select(group => group).ToList();
                if (addedUsersOfMonth.Count > 0)
                {
                    var gb = addedUsersOfMonth[0];
                    string createdOnMonth = this.MonthToSring(gb.CreatedOn.Month);
                    int countAllUsers = addedUsersOfMonth.Count;
                    int countAdmins = addedUsersOfMonth.Where(u => u.Type == "Administrator").Count();
                    int countLibrarys = addedUsersOfMonth.Where(u => u.Type == "User").Count();
                    chartData.Add(new ChartAddedUserData(
                       createdOnMonth,
                       countAllUsers,
                       countAdmins,
                       countLibrarys));
                }
            }

            var chartAddedUsers = new ChartAddedUsers("Регистрирани потребители през последните 6 месеца", chartData);
            return chartAddedUsers;
        }

        private ChartViewModel ChartAddedBookSinceSixМonth()
        {
            var chartData = new List<ChartDataViewModel>();
            var groups = this.context.Books
               .Where(gb =>
                   gb.DeletedOn == null)
               .ToList()
               .GroupBy(b => b.CreatedOn.Year + " " + b.CreatedOn.Month)
               .Take(6)
               .ToList();

            foreach (var group in groups)
            {
                List<Book> bookOfMonth = group.Select(group => group).ToList();
                if (bookOfMonth.Count > 0)
                {
                    var gb = bookOfMonth[0];
                    string createdOnMonth = this.MonthToSring(gb.CreatedOn.Month);

                    chartData.Add(new ChartDataViewModel(
                        createdOnMonth,
                        bookOfMonth.Count));
                }
            }

            var chartGettenBookSinceSixМonth = new ChartViewModel("Добавени книги за последните 6 месеца", chartData);
            return chartGettenBookSinceSixМonth;
        }

        private string MonthToSring(int month)
        {
            string result;
            switch (month)
            {
                case 1: result = "Януари"; break;
                case 2: result = "Февруари"; break;
                case 3: result = "Март"; break;
                case 4: result = "Април"; break;
                case 5: result = "Май"; break;
                case 6: result = "Юни"; break;
                case 7: result = "Юли"; break;
                case 8: result = "Август"; break;
                case 9: result = "Септември"; break;
                case 10: result = "Октомври"; break;
                case 11: result = "Ноември"; break;
                case 12: result = "Декември"; break;
                default:
                    result = "null";
                    break;
            }

            return result;
        }
    }
}
