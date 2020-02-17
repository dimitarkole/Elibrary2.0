namespace ELibrary.Web.ViewModels.CommonResurces
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ProfilViewModel
    {
        public ProfilViewModel()
        {
            this.ResetPasswordViewModel = new ResetPasswordViewModel();
        }

        public string AvatarLocation { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IFormFile Photo { get; set; }

        public ResetPasswordViewModel ResetPasswordViewModel { get; set; }

        public Dictionary<string, int> ActivityData { get; set; }

    }
}
