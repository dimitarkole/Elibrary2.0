using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.Library
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
            this.UserId = null;
        }

        public string Avatar { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
    }
}
