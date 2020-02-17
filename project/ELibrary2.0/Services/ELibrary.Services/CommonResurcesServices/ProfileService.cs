namespace ELibrary.Services.CommonResurcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.CommonResurces;

    public class ProfileService : IProfileService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService notificationService;

        public ProfileService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService notificationService)
        {
            this.context = context;
            this.genreService = genreService;
            this.notificationService = notificationService;
        }

        public ProfilViewModel PreparedPage(string userId)
        {
            var user = this.context
               .Users
               .FirstOrDefault(u => u.Id == userId);
            var model = new ProfilViewModel()
            {
                AvatarLocation = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ActivityData = this.GetActivityData(userId),
            };
            return model;
        }

        public List<object> ResetPassword(ProfilViewModel model, string userId)
        {
            throw new NotImplementedException();
        }


        public ProfilViewModel SaveChanges(ProfilViewModel model, string userId)
        {
            var user = this.context.Users
                 .FirstOrDefault(u => u.Id == userId);
            user.Avatar = model.AvatarLocation;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            this.context.SaveChanges();

            var message = $"Успешно променен профил!";
            this.notificationService.AddNotificationAtDB(userId, message);

            var returnModel = this.PreparedPage(userId);
            return returnModel;
        }

        private Dictionary<string, int> GetActivityData(string userId)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            var userRoleObj = this.context.UserRoles.FirstOrDefault(us => us.UserId == userId);
            if (userRoleObj != null)
            {
                var role = this.context.Roles.FirstOrDefault(r => r.Id == userRoleObj.RoleId);
                if (role.Name == "Administrator")
                {
                    result = this.GetActivityAdmin(userId);
                }
                else
                {
                    result = this.GetActivityUser(userId);
                }

            }
            return result;
        }

        private Dictionary<string, int> GetActivityUser(string userId)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Добавени книги", this.CountAddedBooks(userId));
            result.Add("Взети книги", this.CountGettedBooks(userId));
            result.Add("Въенати книги", this.CountTakenBooks(userId));
           // result.Add("Четящи се книги", this.CountReaders(userId));

            result.Add("Прочетени от Вас", this.CountReadedBooks(userId));
            result.Add("Взето от Вас", this.CountGettenBooks(userId));

            return result;
        }

        private int CountReadedBooks(string userId)
        {
            int count = this.context
                .GetBooks
                .Where(gb =>
                    gb.UserId == userId
                    && gb.ReturnedOn != null
                    && gb.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountGettenBooks(string userId)
        {
            int count = this.context
                .GetBooks
                .Where(gb =>
                    gb.UserId == userId
                    && gb.ReturnedOn == null
                    && gb.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountAddedBooks(string userId)
        {
            int count = this.context
                .Books
                .Where(b =>
                    b.UserId == userId
                    && b.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountGettedBooks(string userId)
        {
            int count = this.context
                .GetBooks
                .Where(b =>
                    b.Book.UserId == userId
                    && b.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountTakenBooks(string userId)
        {
            int count = this.context
                .GetBooks
                .Where(b =>
                    b.Book.UserId == userId
                    && b.ReturnedOn == null
                    && b.DeletedOn == null)
                .Count();
            return count;
        }

     /*   private int CountReaders(string userId)
        {
            int count = this.context
                .GetBooks
                .Where(b =>
                    b.Book.UserId == userId
                    && b.DeletedOn == null)
                .GroupBy(b => b.UserId)
                .Count();
            return count;
        }
        */
        private Dictionary<string, int> GetActivityAdmin(string userId)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            result.Add("Брой администрототи", this.CountUsersByType("Administrator"));
            result.Add("Общ брой потребител", this.CountAllUsers());
            result.Add("Брой потребители", this.CountUsersByType("User"));
            result.Add("Брой книги", this.CountAllBooks());

            return result;
        }

        private int CountAllUsers()
        {
            int count = this.context
                .Users
                .Where(u => u.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountAllBooks()
        {
            int count = this.context
                .Books
                .Where(u => u.DeletedOn == null)
                .Count();
            return count;
        }

        private int CountUsersByType(string roleName)
        {
            var role = this.context.Roles.FirstOrDefault(r => r.Name == roleName);
            int count = this.context
               .Users
               .Where(u =>
                    u.DeletedOn == null
                    && u.Roles.FirstOrDefault(r => r.RoleId == role.Id) != null)
               .Count();
            return count;
        }
    }
}
