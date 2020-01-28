namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GenreListViewModel
    {
        public GenreListViewModel()
        {
            this.Id = null;
            this.Name = null;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
