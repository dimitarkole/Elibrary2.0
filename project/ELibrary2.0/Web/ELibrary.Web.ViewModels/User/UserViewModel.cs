namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserViewModel
    {
        public UserViewModel()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
            this.UserId = null;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
    }
}
