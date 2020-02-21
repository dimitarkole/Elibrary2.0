namespace ELibrary.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Library;
 
    public class ViewLibraryViewModel
    {
        public ViewLibraryViewModel()
        {
            this.ActivityData = new Dictionary<string, int>();
            this.AllAddedBooks = new AllAddedBooksViewModel();
        }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public Dictionary<string, int> ActivityData { get; set; }

        public AllAddedBooksViewModel AllAddedBooks { get; set; }

    }
}
