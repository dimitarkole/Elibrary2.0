namespace ELibrary.Services.CommonResurcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ELibrary.Data;
    using ELibrary.Data.Models;
    using ELibrary.Services.Contracts.CommonResurcesServices;
    using ELibrary.Web.ViewModels.CommonResurces;
    using Microsoft.EntityFrameworkCore;

    public class GenreService : IGenreService
    {
        private ApplicationDbContext context;

        public GenreService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<GetBook> GetBooks { get; set; }

        public DbSet<Notification> Notification { get; set; }

        public List<GenreListViewModel> GetAllGenres()
        {
            var genres = this.context.Genres.Select(g => new GenreListViewModel()
            {
                Id = g.Id,
                Name = g.Name,
            }).ToList();
            var result = genres.OrderBy(x => x.Name).ToList();
            return result;
        }
    }
}
