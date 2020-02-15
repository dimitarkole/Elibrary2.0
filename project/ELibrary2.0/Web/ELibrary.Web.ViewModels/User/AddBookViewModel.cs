namespace ELibrary.Web.ViewModels.User
{
    using ELibrary.Web.ViewModels.CommonResurces;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Currencys = new List<string>();
            this.Currencys.Add("$");
            this.Currencys.Add("€");
            this.Currencys.Add("LV");

            this.VirtualOrReal = "Real";
        }

        public string AvatarLocation { get; set; }
        public IFormFile Photo { get; set; }

        public string LogoLocation { get; set; }

        
        public IFormFile Logo { get; set; }

        public string PDFLocation { get; set; }

        [Display(Name = "PDF Online version")]
        public IFormFile PDF { get; set; }

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


        public List<GenreListViewModel> Genres { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public double Price { get; set; }

        public List<string> Currencys { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        public string EFormatString { get; set; }

        [Display(Name = "Review Text")]
        [StringLength(5000, MinimumLength = 10)]
        public string Review { get; set; }

        [Display(Name = "Book location at library")]
        [StringLength(100)]
        public string WhereIsBook { get; set; }

        [Display(Name = "Virtual or Rela")]
        [StringLength(100)]
        public string VirtualOrReal { get; set; }
    }
}
