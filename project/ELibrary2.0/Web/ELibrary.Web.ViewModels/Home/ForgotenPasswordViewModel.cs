namespace ELibrary.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ForgotenPasswordViewModel
    {
        [Required(ErrorMessage = "Моля въведете правилен email адрес!")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
