﻿namespace ELibrary.Services.CommonResurcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.CommonResurces;

    public class NotificationService : INotificationService
    {
        private ApplicationDbContext context;

        private ISendMail sendMail;

        public NotificationService(ApplicationDbContext context, ISendMail sendMail)
        {
            this.context = context;
            this.sendMail = sendMail;
        }

        public string AddNotificationAtDB(string userId, string textOfNotification)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id == userId);

            Notification notification = new Notification()
            {
                UserId = userId,
                User = user,
                TextOfNotification = textOfNotification,
                SeenOn = null,
            };

            this.context.Notifications.Add(notification);
            this.context.SaveChanges();

            this.sendMail.SendingMail(user.Email, "Ново известие в профила ви", textOfNotification);

            return notification.Id;
        }

        public NotificationsNavBarViewModel GetNotificationsNavBar(string userId)
        {
            var notifications = this.context.Notifications
                .Where(m =>
                    m.DeletedOn == null
                    && m.UserId == userId
                    && m.SeenOn == null)
                .Select(m => new NotificationNavBarViewModel()
                {
                    CreatedOn = m.CreatedOn,
                    Id = m.Id,
                    TextOfNotification = m.TextOfNotification,
                    SeenOn = m.SeenOn,
                })
                .ToList();

            var result = new NotificationsNavBarViewModel(notifications);

            return result;
        }

        public NotificationsViewModel GetNotificationsChangePage(NotificationsViewModel model, string userId, int pageIndex)
        {
            var notifications = this.context.Notifications
               .Where(m =>
                   m.DeletedOn == null
                   && m.UserId == userId)
               .OrderBy(m => m.SeenOn)
               .ThenByDescending(m => m.CreatedOn)
               .Select(m => new NotificationViewModel()
               {
                   Id = m.Id,
                   CreatedOn = m.CreatedOn,
                   TextOfNotification = m.TextOfNotification,
                   SeenOn = m.SeenOn,
               })
               .ToList();
            var seenChacker = notifications.FirstOrDefault(m => m.SeenOn == null);
            if (seenChacker != null)
            {
                notifications = notifications.OrderBy(m => m.SeenOn)
                   .ThenByDescending(m => m.CreatedOn).ToList();
            }
            else
            {
                notifications = notifications.OrderByDescending(m => m.CreatedOn).ToList();
            }

            int countBooksOfPage = model.CountNotificationsOfPage;
            int currentPage = pageIndex;

            int maxCountPage = notifications.Count() / countBooksOfPage;
            if (notifications.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewNotifications = notifications.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage).ToList();

            foreach (var notification in viewNotifications)
            {
                var notificationContext = this.context.Notifications.FirstOrDefault(m => m.Id == notification.Id);
                notificationContext.SeenOn = DateTime.UtcNow;
                this.context.SaveChanges();
            }

            var result = new NotificationsViewModel()
            {
                Notifications = viewNotifications,
                CountNotificationsOfPage = countBooksOfPage,
                MaxCountPage = maxCountPage,
            };

            return result;
        }

        public NotificationsViewModel GetNotificationsPreparedPage(string userId)
        {
            var model = new NotificationsViewModel();
            var returnModel = this.GetNotificationsChangePage(model, userId, 1);
            return returnModel;
        }
    }
}
