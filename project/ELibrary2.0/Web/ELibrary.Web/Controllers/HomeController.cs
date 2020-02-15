namespace ELibrary.Web.Controllers
{
    using System.Diagnostics;
    using ELibrary.Services.Contracts.BaseServices;
    using ELibrary.Web.ViewModels;
    using ELibrary.Web.ViewModels.User;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly IViewBookService viewBookService;

        public HomeController(
            IHomeService homeService,
            IViewBookService viewBookService)
        {
            this.homeService = homeService;
            this.viewBookService = viewBookService;
        }

        public IActionResult Index()
        {
            var model = this.homeService.PreparedPage();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult HomeBooksSearch(AllAddedBooksViewModel model)
        {
            var returnModel = this.homeService.GetBooks(model);
            return this.View("Index", returnModel);
        }

        [HttpPost]
        public IActionResult ChangePageHome(AllAddedBooksViewModel model, int id)
        {
            var returnModel = this.homeService.ChangeActivePage(model, id);
            return this.View("Index", returnModel);
        }

        public IActionResult ViewBook(AllAddedBooksViewModel model, string id)
        {
            var returnModel = this.viewBookService.PreparedPage(id);
            return this.View(returnModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
