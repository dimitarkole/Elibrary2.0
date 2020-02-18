namespace ELibrary.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using ELibrary.Common;
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.Controllers;
    using ELibrary.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
#pragma warning disable SA1649 // File name should match first type name
    public class RegisterModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly ApplicationDbContext context;
        private readonly ISendMail sendMail;
        private readonly IHomeService homeService;



        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ISendMail sendMail,
            ApplicationDbContext context,
            IHomeService homeService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.sendMail = sendMail;
            this.emailSender = emailSender;
            this.context = context;
            this.homeService = homeService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = this.Input.Email,
                    Email = this.Input.Email,
                    LastName = this.Input.LastName,
                    FirstName = this.Input.FirstName,
                    LockoutEnabled = false,
                };
                var result = await this.userManager.CreateAsync(user, this.Input.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("Потребителят е успешно регистриран!");

                    var roleId = this.context.Roles.FirstOrDefault(r =>
                       r.Name == GlobalConstants.UserRoleName
                       && r.DeletedOn == null).Id;
                    IdentityUserRole<string> userRole = new IdentityUserRole<string>()
                    {
                        RoleId = roleId,
                        UserId = user.Id,
                    };
                    this.context.UserRoles.Add(userRole);
                    this.context.SaveChanges();

                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    var userId = user.Id;

                    Notification message = new Notification()
                    {
                        UserId = userId,
                        User = user,
                        TextOfNotification = "Успешно регистриран потребител!",
                    };

                    this.context.Notifications.Add(message);
                    this.context.SaveChanges();

                    var email = this.Input.Email;
                    var info = new Dictionary<string, string>();
                    info.Add("password", this.Input.Password);

                   /* var checkVerificatedCode = this.context.VerificatedCodes
                     .FirstOrDefault(vc => vc.UserId == userId);
                    var verificatedCode = new VerificatedCode();
                    if (checkVerificatedCode != null)
                    {
                        verificatedCode = checkVerificatedCode;
                    }

                    verificatedCode.UserId = userId;
                    Random random = new Random();
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    string varifyCode = userId.Substring(0, Math.Min(3, userId.Length));
                    var length = 8 - varifyCode.Length;

                    varifyCode += new string(Enumerable.Repeat(chars, length)
                        .Select(s => s[random.Next(s.Length)]).ToArray());

                    verificatedCode.Code = varifyCode;
                    if (checkVerificatedCode == null)
                    {
                        this.context.VerificatedCodes.Add(verificatedCode);
                    }

                    this.context.SaveChanges();

                    info.Add("code", varifyCode);*/

                    this.sendMail.SendMailByTemplate(email, "NewRegesterUser", info);

                    var roleName = this.context.Roles.FirstOrDefault(r =>
                       r.Name == GlobalConstants.UserRoleName
                       && r.DeletedOn == null).Name;

                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return this.LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyEmail()
        {
            VerifyEmailViewModel model = new VerifyEmailViewModel();
            return this.Page();
        }

        private IActionResult RedirectToLocal(string userId, string roleName)
        {
            this.HttpContext.Session.SetString("userId", userId);

            var email = this.context.Users.FirstOrDefault(u => u.Id == userId).Email;

            if (this.homeService.CheckVerifedEmail(userId) == false)
            {
                this.HttpContext.Session.SetString("userId", userId);
                this.homeService.SendVerifyCodeToEmail(userId);
                return this.VerifyEmail();
            }

            return this.RedirectToAction(nameof(HomeController.Index), "AdminAccount");
        }

       

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First name")]
            public virtual string FirstName { get; set; }

            [Required]
            [Display(Name = "Last name")]
            public virtual string LastName { get; set; }
        }
    }
}
