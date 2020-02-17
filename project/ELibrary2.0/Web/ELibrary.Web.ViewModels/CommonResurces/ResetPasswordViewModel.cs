namespace ELibrary.Web.ViewModels.CommonResurces
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ResetPasswordViewModel
    {
        public ResetPasswordViewModel()
        {
            this.OldPassword = null;
            this.NewPassword = null;
            this.ConfirmNewPassword = null;
        }

        [DataType(DataType.Password)]
        [Display(Name = "Стара парола")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        [MaxLength(20)]
        public string NewPassword { get; set; }

        [Display(Name = "Повтори паролата")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
