namespace ELibrary.Web.ViewModels.User
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ELibrary.Web.ViewModels.CommonResurces;
    using Microsoft.AspNetCore.Http;
    
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Currencys = new List<string>();
            this.Currencys.Add("$");
            this.Currencys.Add("€");
            this.Currencys.Add("LV");
        }

        public string BookId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please input title!")]
        [StringLength(50, MinimumLength =5)]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [StringLength(50, MinimumLength = 5)]

        [Required(ErrorMessage = "Please input author!")]
        public string Author { get; set; }

        [Display(Name = "Catalog number")]
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Please input catalog number!")]
        public string CatalogNumber { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Please select genre!")]
        public string GenreId { get; set; }

        [Display(Name = "Commentar")]
        public string Commentar { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        public List<string> Currencys { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

        public IFormFile LogoPhoto { get; set; }

        public IFormFile EFormat { get; set; }

        public string EFormatString { get; set; }




        [Display(Name = "Review Text")]
        [StringLength(5000, MinimumLength = 10)]
        public string Review { get; set; }

        [Display(Name = "Book location at library")]
        [StringLength(100)]
        public string WhereIsBook { get; set; }
    }
}
