using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.Administration
{
    public class AddedGenreViewModel
    {
        public AddedGenreViewModel()
        {
            this.Name = null;
            this.Id = null;
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}
