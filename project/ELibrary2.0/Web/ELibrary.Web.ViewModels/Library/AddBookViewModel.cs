using ELibrary.Web.ViewModels.CommonResurces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ELibrary.Web.ViewModels.Library
{
    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Currencys = new List<string>();
            this.Currencys.Add("$");
            this.Currencys.Add("€");
            this.Currencys.Add("LV");

            this.VirtualOrReal = "Реална";
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
        [Required(ErrorMessage = "Моля въведете заглавие!")]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [StringLength(50, MinimumLength = 5)]

        [Required(ErrorMessage = "Моля въведете автор!")]
        public string Author { get; set; }

        [Display(Name = "Catalog number")]
        //[StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Моля каталожен номер!")]
        public string CatalogNumber { get; set; }

        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Моля изберете жанр!")]
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
        [StringLength(5000, MinimumLength = 5)]
        public string Review { get; set; }

        [Display(Name = "Локация на книгата в библиотеката")]
        [StringLength(500)]
        public string WhereIsBook { get; set; }

        [Display(Name = "Virtual or Rela")]
        [StringLength(100)]
        public string VirtualOrReal { get; set; }
    }
}
