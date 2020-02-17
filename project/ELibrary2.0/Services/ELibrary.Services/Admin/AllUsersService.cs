namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Services.Contracts.Admin;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.Administration;

    public class AllUsersService : IAllUsersService
    {
        private ApplicationDbContext context;

        private INotificationService notificationService;

        public AllUsersService(
            ApplicationDbContext context,
            INotificationService notificationService)
        {
            this.context = context;
            this.notificationService = notificationService;
        }

        public AllUsersViewModel ChangeActivePage(AllUsersViewModel model, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetUsers(model);
        }

        public List<object> DeleteUser(AllUsersViewModel model, string userId, string adminId)
        {
            List<object> result = new List<object>();
            var flag = false;
            if (userId != adminId)
            {
                var user = this.context.Users
                    .FirstOrDefault(u => u.Id == userId && u.DeletedOn == null);
                user.DeletedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                flag = true;
            }

            var returnMoodel = this.GetUsers(model);
            result.Add(result);
            if (flag == true)
            {
                result.Add("Успешно изтрит потребител!");
                var message = $"Вашият профил беше изтрит успешно";
                this.notificationService.AddNotificationAtDB(userId, message);
            }
            else
            {
                result.Add("Не може да си изтриите собствения профил!");
            }
            return result;
        }

    

        public List<object> MakeUserAdmin(AllUsersViewModel model, string userId)
        {
            var adminRoleId = this.context.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;
            var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            userRole.RoleId = adminRoleId;
            this.context.SaveChanges();
            var returnMoodel = this.GetUsers(model);
            List<object> result = new List<object>();
            result.Add(result);
            result.Add("Успешно променени права на потребител!");
            var message = $"Вашите права бяха променени на администратор";
            this.notificationService.AddNotificationAtDB(userId, message);
            return result;
        }


        public AllUsersViewModel PreparedPage()
        {
            var model = new AllUsersViewModel();
            var returnModel = this.GetUsers(model);
            return returnModel;
        }

        public List<object> MakeAdminUser(AllUsersViewModel model, string userId)
        {
            var adminRoleId = this.context.Roles.FirstOrDefault(r => r.Name == "User").Id;
            var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            userRole.RoleId = adminRoleId;
            this.context.SaveChanges();
            var returnMoodel = this.GetUsers(model);
            List<object> result = new List<object>();
            result.Add(result);
            result.Add("Успешно променени права на потребител!");
            var message = $"Вашите права бяха променени на потребителски!";
            this.notificationService.AddNotificationAtDB(userId, message);
            return result;
        }

        private AllUsersViewModel GetUsers(AllUsersViewModel model)
        {
            var email = model.SearchUser.Email;
            var firstName = model.SearchUser.FirstName;
            var lastName = model.SearchUser.LastName;
            var libraryName = model.SearchUser.LibraryName;

            var sortMethodId = model.SortMethodId;
            var countUsersOfPage = model.CountUsersOfPage;
            var currentPage = model.CurrentPage;

            var users = this.context.Users.Where(u =>
                u.DeletedOn == null)
                .Select(u => new UserViewModel()
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserId = u.Id,
                    //Type = u.Type,
                });

            users = this.SelectUsers(
                users,
                email,
                firstName,
                lastName,
                libraryName);

            users = this.SortBooks(sortMethodId, users);
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
                FirstName = firstName,
                LastName = lastName,
                LibraryName = libraryName,
            };

            var returnModel = new AllUsersViewModel()
            {
                SearchUser = searchUser,
                CountUsersOfPage = countUsersOfPage,
                SortMethodId = sortMethodId,
                MaxCountPage = maxCountPage,
                Users = viewUsers,
                CurrentPage = currentPage,
            };
            return returnModel;
        }

        private IQueryable<UserViewModel> SortBooks(string sortMethodId, IQueryable<UserViewModel> users)
        {
            if (sortMethodId == "Email на потребителя а-я")
            {
                users = users.OrderBy(u => u.Email);
            }
            else if (sortMethodId == "Email на потребителя я-а")
            {
                users = users.OrderByDescending(u => u.Email);
            }

            return users;
        }

        private IQueryable<UserViewModel> SelectUsers(
            IQueryable<UserViewModel> users,
            string email,
            string firstName,
            string lastName,
            string libraryName)
        {
            if (email != null)
            {
                users = users.Where(b => b.Email.Contains(email));
            }

            if (firstName != null)
            {
                users = users.Where(b => b.FirstName.Contains(firstName));
            }

            if (lastName != null)
            {
                users = users.Where(b => b.LastName.Contains(lastName));
            }

            if (libraryName != null)
            {
                users = users.Where(b => b.LibraryName.Contains(libraryName));
            }

            return users;
        }

      
    }
}
