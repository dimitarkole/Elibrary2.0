namespace ELibrary.Services.Contracts.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.Home;
    using ELibrary.Web.ViewModels.Library;
    using ELibrary.Web.ViewModels.User;

    public interface IViewBooksService
    {
        AllAddedBooksViewModel PreparedPage();

        AllAddedBooksViewModel GetBooks(AllAddedBooksViewModel model);

        AllAddedBooksViewModel ChangeActivePage(AllAddedBooksViewModel model, int newPage);



        public bool CheckVerifedEmail(string userId);

        public void SendVerifyCodeToEmail(string userId);

        public Dictionary<string, string> VerifyEmail(VerifyEmailViewModel model);

        public string ForgotenPasswordSendCode(ForgotenPasswordViewModel model);

    }
}
