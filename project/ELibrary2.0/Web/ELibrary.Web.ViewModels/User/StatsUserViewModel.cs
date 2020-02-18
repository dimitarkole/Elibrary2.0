﻿using ELibrary.Data.Models;
using ELibrary.Web.ViewModels.CommonResurces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Web.ViewModels.User
{
    public class StatsUserViewModel
    {
        public StatsUserViewModel()
        {
            this.SearchBook = new Book();
        }

        public Book SearchBook { get; set; }

        public List<GenreListViewModel> Genres { get; set; }

        public ChartGettenBookSinceSixМonth ChartGettenBookSinceSixМonth { get; set; }

        public ChartViewModel ChartAddedBookSinceSixМonth { get; set; }
    }
}
