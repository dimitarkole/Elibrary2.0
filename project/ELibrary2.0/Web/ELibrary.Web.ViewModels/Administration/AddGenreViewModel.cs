namespace ELibrary.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AddGenreViewModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage = "Моля въведете име на жанра!")]
        [StringLength(50, MinimumLength = 5)]
        public string Name{ get; set; }

        public string Id { get; set; }

    }
}
