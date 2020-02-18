namespace ELibrary.Services.BaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.CommonResurces;
    using ELibrary.Web.ViewModels.Home;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Identity;

    public class HomeService : IHomeService
    {
        private ApplicationDbContext context;

        private IGenreService genreService;

        private INotificationService messageService;

        private ISendMail sendMail;


        public HomeService(
            ApplicationDbContext context,
            IGenreService genreService,
            INotificationService messageService,
            ISendMail sendMail)
        {
            this.context = context;
            this.genreService = genreService;
            this.messageService = messageService;
            this.sendMail = sendMail;
        }

        public AllAddedBooksViewModel PreparedPage()
        {
            var model = new AllAddedBooksViewModel();
            var returnModel = this.GetBooks(model);
            return returnModel;
        }

        public AllAddedBooksViewModel GetBooks(AllAddedBooksViewModel model)
        {
            var bookCatalogNumber = model.SearchBook.CatalogNumber;
            var title = model.SearchBook.Title;
            var author = model.SearchBook.Author;
            var genreId = model.SearchBook.GenreId;
            var sortMethodId = model.SortMethodId;
            var countBooksOfPage = model.CountBooksOfPage;
            var currentPage = model.CurrentPage;

            var books = this.context.Books.Where(b =>
              b.DeletedOn == null)
              .Select(b => new BookViewModel()
              {
                  Author = b.Author,
                  BookId = b.Id,
                  Title = b.Title,
                  GenreName = b.Genre.Name,
                  GenreId = b.GenreId,
                  CatalogNumber = b.CatalogNumber,
                  Logo = b.Logo,
                  Review = b.Review,
              });

            books = this.SelectBooks(bookCatalogNumber, title, author, genreId, books);

            books = this.SortBooks(sortMethodId, books);

            var genres = this.genreService.GetAllGenres()
                 .OrderByDescending(x => x.Name).ToList();

            var genre = new GenreListViewModel()
            {
                Id = null,
                Name = "Избери жанр",
            };

            genres.Add(genre);
            genres.Reverse();
            int maxCountPage = books.Count() / countBooksOfPage;
            if (books.Count() % countBooksOfPage != 0)
            {
                maxCountPage++;
            }

            var viewBook = books.Skip((currentPage - 1) * countBooksOfPage)
                                .Take(countBooksOfPage);
            var searchBook = new BookViewModel()
            {
                Author = author,
                Title = title,
                GenreId = genreId,
            };

            var returnModel = new AllAddedBooksViewModel()
            {
                Books = viewBook,
                SearchBook = searchBook,
                SortMethodId = sortMethodId,
                Genres = genres,
                MaxCountPage = maxCountPage,
                CurrentPage = currentPage,
                CountBooksOfPage = countBooksOfPage,
            };
            return returnModel;
        }

        public AllAddedBooksViewModel ChangeActivePage(AllAddedBooksViewModel model, int newPage)
        {
            model.CurrentPage = newPage;
            return this.GetBooks(model);
        }

        private IQueryable<BookViewModel> SortBooks(
          string sortMethodId,
          IQueryable<BookViewModel> books)
        {
            if (sortMethodId == "Заглавие а-я")
            {
                books = books.OrderByDescending(b => b.Title);
            }
            else if (sortMethodId == "Автор а-я")
            {
                books = books.OrderBy(b => b.Author);
            }
            else if (sortMethodId == "Автор я-а")
            {
                books = books.OrderByDescending(b => b.Author);
            }
            else if (sortMethodId == "Жанр а-я")
            {
                books = books.OrderBy(b => b.GenreName);
            }
            else if (sortMethodId == "Жанр я-а")
            {
                books = books.OrderByDescending(b => b.GenreName);
            }
            else
            {
                books = books.OrderBy(b => b.Title);
            }

            return books;
        }

        private IQueryable<BookViewModel> SelectBooks(
          string bookCatalogNumber,
          string title,
          string author,
          string genreId,
          IQueryable<BookViewModel> books)
        {
            if (bookCatalogNumber != null)
            {
                books = books.Where(b => b.CatalogNumber.Contains(bookCatalogNumber));
            }

            if (title != null)
            {
                books = books.Where(b => b.Title.Contains(title));
            }

            if (author != null)
            {
                books = books.Where(b => b.Author.Contains(author));
            }

            if (genreId != null)
            {
                books = books.Where(b => b.GenreId == genreId);
            }

            return books;
        }

        public bool CheckVerifedEmail(string userId)
        {
            var check = this.context.Users
                .FirstOrDefault(u =>
                    u.Id == userId).VerifiedOn;
            if (check == null)
            {
                return false;
            }

            return true;
        }

        public string ForgotenPasswordSendCode(ForgotenPasswordViewModel model)
        {
            var checkEmailAtDB = this.context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (checkEmailAtDB == null)
            {
                return "Няма регистриран потребител с такъв email";
            }

            var userId = checkEmailAtDB.Email;
            var claimType = "ForgotenPasswordSendCode";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string code = userId.Substring(0, Math.Min(3, userId.Length));
            var length = 8 - code.Length;
            Random random = new Random();
            code += new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var claim = new IdentityUserClaim<string>();
            var checkClaimCode = this.context.UserClaims
             .FirstOrDefault(c => c.UserId == userId
              && c.ClaimType == claimType);
            if (checkClaimCode != null)
            {
                claim = checkClaimCode;
            }

            claim.ClaimType = claimType;
            claim.ClaimValue = code;
            claim.UserId = userId;
            this.context.SaveChanges();

            var info = new Dictionary<string, string>();
            info.Add("code", code);
            var userEmail = model.Email;

            this.sendMail.SendMailByTemplate(userEmail, claimType, info);
            return " ";
        }

     

        public void SendVerifyCodeToEmail(string userId)
        {
            var checkVerificatedCode = this.context.VerificatedCodes
                .FirstOrDefault(vc => vc.UserId == userId);
            var verificatedCode = new VerificatedCode();
            if (checkVerificatedCode != null)
            {
                verificatedCode = checkVerificatedCode;
            }

            verificatedCode.UserId = userId;

            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string code = userId.Substring(0, Math.Min(3, userId.Length));
            var length = 8 - code.Length;

            code += new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            verificatedCode.Code = code;
            if (checkVerificatedCode == null)
            {
                this.context.VerificatedCodes.Add(verificatedCode);
            }

            this.context.SaveChanges();

            Dictionary<string, string> info = new Dictionary<string, string>();
            info.Add("code", code);
            var userEmail = this.context.Users.FirstOrDefault(u => u.Id == userId).Email;
            this.sendMail.SendMailByTemplate(userEmail, "VerifyMailTemplate", info);
        }

        public Dictionary<string, string> VerifyEmail(VerifyEmailViewModel model)
        {
            var code = model.Code;
            var check = this.context.VerificatedCodes
                .FirstOrDefault(vf => vf.Code == code);
            var result = new Dictionary<string, string>();
            result.Add("verificating", "No");
            if (check != null)
            {
                var userId = check.UserId;
                var user = this.context.Users.FirstOrDefault(u => u.Id == userId);
                user.VerifiedOn = DateTime.UtcNow;
                this.context.SaveChanges();
                result["verificating"] = "Yes";
                result.Add("userId", user.Id);
                var typeId = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId).RoleId;
                var typeName = this.context.Roles.FirstOrDefault(r => r.Id == typeId).Name;

                result.Add("type", typeName);

            }

            return result;
        }
    }
}
