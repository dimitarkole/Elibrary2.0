namespace ELibrary.Services.LibraryServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.LibraryServices;
    using ELibrary.Web.ViewModels.Library;

    public class UserService : IUserService
    {
        private ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public AllUsersViewModel PreparedPage()
        {
            var model = new AllUsersViewModel();
            var returnModel = this.GetUsers(model);
            return returnModel;
        }

        public AllUsersViewModel GetUsers(AllUsersViewModel model)
        {
            var firstName = model.SearchUser.FirstName;
            var lastName = model.SearchUser.LastName;
            var sortMethodId = model.SortMethodId;
            var currentPage = model.CurrentPage;
            var countUsersOfPage = model.CountUsersOfPage;
            var email = model.SearchUser.Email;

            var users = this.context.Users
                .Select(u => new UserViewModel()
                {
                    Avatar = u.Avatar,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserId = u.Id,
                    Email = u.Email,
                });

            users = this.SelectUsers(firstName, lastName, email, users);
            users = this.SortUsers(sortMethodId, users);

            int maxCountPage = users.Count() / countUsersOfPage;
            if (users.Count() % countUsersOfPage != 0)
            {
                maxCountPage++;
            }

            var viewUsers = users.Skip((currentPage - 1) * countUsersOfPage)
                                .Take(countUsersOfPage);

            var searchUser = new UserViewModel()
            {
                Email = email,
                LastName = lastName,
                FirstName = firstName,
            };

            var returnModel = new AllUsersViewModel()
            {
                Users = viewUsers,
                SearchUser = searchUser,
                SortMethodId = sortMethodId,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
                CountUsersOfPage = countUsersOfPage,
            };
            return returnModel;
        }

        public AllUsersViewModel ChangeActivePage(AllUsersViewModel model, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetUsers(model);
        }

        private IQueryable<UserViewModel> SelectUsers(
          string firstName,
          string lastName,
          string email,
          IQueryable<UserViewModel> users)
        {

            if (firstName != null)
            {
                users = users.Where(u => u.FirstName.Contains(firstName));
            }

            if (lastName != null)
            {
                users = users.Where(u => u.LastName.Contains(lastName));
            }

            if (email != null)
            {
                users = users.Where(u => u.Email.Contains(email));
            }

            return users;
        }

        private IQueryable<UserViewModel> SortUsers(
          string sortMethodId,
          IQueryable<UserViewModel> users)
        {
            if (sortMethodId == "Email а-я")
            {
                users = users.OrderBy(u => u.Email);
            }
            else
            {
                users = users.OrderByDescending(u => u.Email);
            }

            return users;
        }
    }
}
