using ELibrary.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.Administration
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel()
        {
            this.SortMethods = new List<string>();
            this.SortMethods.Add("Email на потребителя а-я");
            this.SortMethods.Add("Email на потребителя я-а");
            this.SortMethodId = this.SortMethods[0];

            this.CountUsersOfPageList = new List<int>();
            this.CountUsersOfPageList.Add(10);
            this.CountUsersOfPageList.Add(15);
            this.CountUsersOfPageList.Add(20);

            this.CountUsersOfPage = this.CountUsersOfPageList[0];
            this.SortMethodId = this.SortMethods[0];
            this.CurrentPage = 1;
            this.SearchUser = new UserViewModel();
        }

        public UserViewModel SearchUser { get; set; }

        public string SortMethodId { get; set; }

        public List<string> SortMethods { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; }

        public int CurrentPage { get; set; }

        public int MaxCountPage { get; set; }

        public int CountUsersOfPage { get; set; }

        public List<int> CountUsersOfPageList { get; set; }
    }
}
