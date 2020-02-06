namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GivenBookViewModel
    {
        public GivenBookViewModel()
        {
            this.Id = null;
            this.Title = null;
            this.Author = null;
            this.GenreName = null;
            this.GenreId = null;
            this.UserName = null;
            this.FirstName = null;
            this.LastName = null;
            this.Email = null;
            this.ReturnedOn = null;
        }

        public string Id { get; set; }

        public string CatalogNumber { get; set; }


        public string Title { get; set; }

        public string Author { get; set; }

        public string GenreName { get; set; }

        public string Logo { get; set; }


        public string GenreId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime? ReturnedOn { get; set; }
    }
}
