namespace ELibrary.Services.Contracts.CommonResurcesServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ELibrary.Web.ViewModels.CommonResurces;

    public interface IGenreService
    {
        List<GenreListViewModel> GetAllGenres();
    }
}
