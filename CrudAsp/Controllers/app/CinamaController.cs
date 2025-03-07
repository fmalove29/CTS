using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CrudAsp.Services.Cinemas;
using CrudAsp.Services.Movies;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.resource.response;
using CrudAsp.resource.request;
using CrudAsp.Extensions;


namespace CrudAsp.Controllers.app;

[Authorize]
public class CinemaController : Controller
{
    private readonly CinemaService cinemaService;
    private readonly MovieService movieService;

    private readonly IRepository<Cinema> repository;
    private readonly IRepository<Movie> _movieRepo;
    private readonly IRepository<Genre> _genRepo;
    public CinemaController
    (
        IRepository<Cinema> repository
    )
    {
        this.repository = repository;
        cinemaService = new CinemaService((Repository<Cinema>) repository);
        // movieService = new MovieService((Repository<Movie>) _movieRepo, (Repository<Genre>) _genRepo);

    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> IndexData()
    {
        var cinemas = await cinemaService.GetAllAsync();

        var cineResponse = cinemas.Select(c => new CinemaResponse {
            Id          = c.Id,
            CinemaName  = c.CinemaName,
            Location    = c.Location,
            created_at  = c.created_at
        }).ToList();
        

        return Ok(cineResponse);
    }

    [HttpGet]
    public async Task<IActionResult> CinemaSearch(string searchQuery)
    {
        var cinemas = await cinemaService.GetAllAsync();

        // Filter cinemas based on searchQuery
        if (!string.IsNullOrEmpty(searchQuery))
        {
            cinemas = cinemas
                .Where(c => c.CinemaName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) 
                        || c.Location.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        var cineResponse = cinemas.Select(c => new CinemaResponse {
            Id          = c.Id,
            CinemaName  = c.CinemaName,
            Location    = c.Location,
            created_at  = c.created_at
        }).ToList();

        return Json(cineResponse); // Return JSON response
    }

    


    public async Task<IActionResult> Details(Guid Id)
    {
        var db = await(await cinemaService.GetDbSet())
                .Include(e => e.Halls)
                .FirstOrDefaultAsync(e => e.Id == Id);

        // return Json(db);
        return View(db.ToCinemaResponse());
    }

    // public async Task<IActionResult> CinemaDetails(Guid Id)
    // {
    //     var cinema = await cinemaService.
    // }

    [HttpPost]
    public async Task<IActionResult> PostCinema([FromBody] CinemaRequest cinemaRequest)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var cinema = new Cinema
                {   
                    CinemaName      = cinemaRequest.CinemaName,
                    Location        = cinemaRequest.Location
                };
    
                
                if (string.IsNullOrEmpty(cinemaRequest.CinemaName) || string.IsNullOrEmpty(cinemaRequest.Location))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Unable to save with empty fields ",
                        result = cinema
                    });
                }
                    // return Json(cinema);
                    await cinemaService.AddAsync(cinema);
                    return Json(new { success = true, message = "Cinema created successfully", result = cinema , Id = cinema.Id });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { success = false, message = ex.Message, innerMessage = innerException });
            }
        }
        return Json(cinemaRequest);
    }

}
