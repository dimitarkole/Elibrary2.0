namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GiveBookViewModel
    {
        public BookViewModel SelectedBook { get; set; }

        public UserViewModel SelectedUser { get; set; }

        public AllAddedBooksViewModel AllBooks { get; set; }

        public AllUsersViewModel AllUsers { get; set; }
    }
}
