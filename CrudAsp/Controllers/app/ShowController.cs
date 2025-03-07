using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CrudAsp.Services.Movies;
using CrudAsp.Repository;
using CrudAsp.Models.app;
using CrudAsp.Services.Shows;
using CrudAsp.resource.response;




namespace CrudAsp.Controllers.app;


public class ShowController : Controller
{
    private readonly ShowService showService;
    private readonly MovieService movieService;
    private readonly IRepository<Show> repository;

    public ShowController(IRepository<Show> repository, MovieService movieService)
    {
        this.repository = repository;
        this.movieService = movieService;

        showService = new ShowService((Repository<Show>)repository);
    }

    public async Task<IActionResult> Index()
    {
        var movie = await movieService.GetAll();
        return Json(movie);
    }   

    [HttpGet("show/{Id}")]
    public async Task<IActionResult> GetShowsById(Guid Id)
    {
        var data = await showService.GetById(Id);
        return Json(Id);
    }
    
    // [HttpGet("/movies/getAll")]
    // public async Task<IActionResult> GetAllMovies()
    // {
    //     var movies = await movieService.GetAll();
    //     return Json(movies.Select(m => new { id = m.Id, title = m.Title }));
    // }
    [HttpGet("show/movies/getAll")]
    public async Task<IActionResult> GetAllMovies(string search)
    {
        var movies = await movieService.GetAll();

        if (!string.IsNullOrEmpty(search))
        {
            movies = movies.Where(m => m.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        return Json(movies.Select(m => new { id = m.Id, title = m.Title,  }));
    }

}
