namespace ELibrary.Services.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
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

        public Dictionary<string, object> DeleteUser(AllUsersViewModel model, string userId, string adminId)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
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
            result.Add("model", returnMoodel);
            if (flag == true)
            {
                result.Add("message", "Успешно изтрит потребител!");
                var message = $"Вашият профил беше изтрит успешно!";
                this.notificationService.AddNotificationAtDB(userId, message);
            }
            else
            {
                result.Add("message", "Не може да си изтриите собствения профил!");
            }

            return result;
        }

        public Dictionary<string, object> MakeUserAdmin(string userId, string adminId)
        {
            var flag = false;
            if (userId != adminId)
            {
                var adminRoleId = this.context.Roles.FirstOrDefault(r => r.Name == "Administrator").Id;
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);

                if (userRole == null)
                {
                    userRole = new IdentityUserRole<string>();
                    userRole.UserId = userId;
                    userRole.RoleId = adminRoleId;

                    this.context.UserRoles.Add(userRole);
                    this.context.SaveChanges();
                }
                else
                {
                    this.context.UserRoles.Remove(userRole);
                    var newUserRole = new IdentityUserRole<string>();
                    newUserRole.UserId = userId;
                    newUserRole.RoleId = adminRoleId;

                    this.context.UserRoles.Add(newUserRole);
                    this.context.SaveChanges();
                }

                flag = true;
            }

            AllUsersViewModel model = new AllUsersViewModel();

            var returnMoodel = this.GetUsers(model);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("model", returnMoodel);
            if (flag == true)
            {
                result.Add("message", "Успешно променени права на потребител!");
                var message = $"Вашите права бяха променени на администратор!";
                this.notificationService.AddNotificationAtDB(userId, message);
            }
            else
            {
                result.Add("message", "Не може да променяте собствените си права!");
            }

            return result;
        }

        public AllUsersViewModel PreparedPage()
        {
            var model = new AllUsersViewModel();
            var returnModel = this.GetUsers(model);
            return returnModel;
        }

        public Dictionary<string, object> MakeAdminUser(string userId, string adminId)
        {
            var flag = false;
            if (userId != adminId)
            {
                var userRoleId = this.context.Roles.FirstOrDefault(r => r.Name == "User").Id;
                var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
                if (userRole == null)
                {
                    userRole = new IdentityUserRole<string>();
                    userRole.UserId = userId;
                    userRole.RoleId = userRoleId;

                    this.context.UserRoles.Add(userRole);
                    this.context.SaveChanges();
                }
                else
                {
                    this.context.UserRoles.Remove(userRole);
                    var newUserRole = new IdentityUserRole<string>();
                    newUserRole.UserId = userId;
                    newUserRole.RoleId = userRoleId;

                    this.context.UserRoles.Add(newUserRole);
                    this.context.SaveChanges();
                }

                flag = true;
            }
            AllUsersViewModel model = new AllUsersViewModel();

            var returnMoodel = this.GetUsers(model);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("model", result);
            if (flag == true)
            {
                result.Add("message", "Успешно променени права на потребител!");
                var message = $"Вашите права бяха променени на потребителски!";
                this.notificationService.AddNotificationAtDB(userId, message);
            }
            else
            {
                result.Add("message", "Не може да променяте собствените си права!");
            }

            return result;
        }

        public Dictionary<string, object> MakeUserLibrary(string userId, string adminId)
        {
            var adminRoleId = this.context.Roles.FirstOrDefault(r => r.Name == "Library").Id;
            var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            var flag = false;
            if (userId != adminId)
            {
                if (userRole == null)
                {
                    userRole = new IdentityUserRole<string>();
                    userRole.UserId = userId;
                    userRole.RoleId = adminRoleId;

                    this.context.UserRoles.Add(userRole);
                    this.context.SaveChanges();
                }
                else
                {
                    this.context.UserRoles.Remove(userRole);
                    var newUserRole = new IdentityUserRole<string>();
                    newUserRole.UserId = userId;
                    newUserRole.RoleId = adminRoleId;

                    this.context.UserRoles.Add(newUserRole);
                    this.context.SaveChanges();
                }
                flag = true;
            }

            AllUsersViewModel model = new AllUsersViewModel();

            var returnMoodel = this.GetUsers(model);
            Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("model", returnMoodel);

            if (flag == true)
            {
                result.Add("message", "Успешно променени права на библиотека!");
                var message = $"Вашите права бяха променени на библиотека!";
                this.notificationService.AddNotificationAtDB(userId, message);
            }
            else
            {
                result.Add("message", "Не може да променяте собствените си права!");
            }

            return result;
        }

        private AllUsersViewModel GetUsers(AllUsersViewModel model)
        {
            var email = model.SearchUser.Email;
            var firstName = model.SearchUser.FirstName;
            var lastName = model.SearchUser.LastName;

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
                    Avatar = u.Avatar,
                });

            users = this.SelectUsers(
                users,
                email,
                firstName,
                lastName);

            users = this.SortBooks(sortMethodId, users);
            int maxCountPage = users.Count() / countUsersOfPage;
            if (users.Count() % countUsersOfPage != 0)
            {
                maxCountPage++;
            }

            List<UserViewModel> viewUsers = users.Skip((currentPage - 1) * countUsersOfPage)
                                .Take(countUsersOfPage)
                                .ToList();

            for (int i = 0; i < viewUsers.Count; i++)
            {
                try
                {
                    var roleId = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == viewUsers[i].UserId).RoleId;
                    var role = this.context.Roles.FirstOrDefault(r => r.Id == roleId);
                    viewUsers[i].Type = role.Name;
                }
                catch (Exception)
                {

                    viewUsers[i].Type = "без роля";

                }

            }

            var searchUser = new UserViewModel()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
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
            string lastName)
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

            return users;
        } 
    }
}
