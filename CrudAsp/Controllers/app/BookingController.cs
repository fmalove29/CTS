
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CrudAsp.Services.Movies;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.Services.Genres;
using CrudAsp.resource.response;

namespace CrudAsp.Controllers.app;

public class BookingController : Controller
{
    private readonly IRepository<Booking> repository;
    private readonly MovieService movieService;
    

    // private readonly GenreService genreService;


    public BookingController
    (
        IRepository<Booking> repository,
        MovieService movieService
        // GenreService genreService
    )
    {
        this.repository = repository;
        this.movieService = movieService;
        // this.genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await movieService.GetAll();

        return Json(movies);
    }

    public async Task<IActionResult> Show(Guid Id)
    {
        var findMovie = await movieService.GetById(Id);
        

        if (findMovie == null)
        {
            return NotFound(); // Handle the case when the movie is not found
        }

        var dto = new MovieResponse
        {
            Id = findMovie.Id,
            Title = findMovie.Title,
            Description = findMovie.Description,
            ReleaseDate = findMovie.ReleaseDate,
            EndDate = findMovie.EndDate,
            Genres = findMovie.MovieGenres.Select(mg => new GenreResponse  // ✅ Correct way to fetch genres
            {
                Id = mg.Genres.Id,
                GenreName = mg.Genres.GenreName
            }).ToList(),
            MovieImages = findMovie.MovieImages.Select(img => new MovieImageResponse
            {
                Name = img.Name,
                Size = img.Size,
                Type = img.Type,
                Path = img.Path,
                MovieId = img.MovieId
            }).ToList()
        };

        return Json(dto);
    }

}
