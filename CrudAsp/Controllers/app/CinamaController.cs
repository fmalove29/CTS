using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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

    public async Task<IActionResult> Index(string searchQuery)
    {
        var cinemas = await cinemaService.GetAllAsync();

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
        
        ViewBag.SearchQuery = searchQuery;

        return View(cineResponse);
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
        var cinema = await cinemaService.GetByIdAsync(Id);

        

        return Json(cinema.ToCinemaResponse());
        // return Json(cinema.ToCinemaResponse());
    }
    // public async Task<IActionResult> CinemaDetails(Guid Id)
    // {
    //     var cinema = await cinemaService.
    // }

    [HttpPost]
    public async Task<IActionResult> PostCinema([FromBody] CinemaRequest cinemaRequest)
    {
        // return Json(cinemaRequest);
        if (ModelState.IsValid)
        {
            try
            {
                var cinema = new Cinema
                {   
                    Id              = Guid.NewGuid(),
                    CinemaName      = cinemaRequest.CinemaName,
                    Location        = cinemaRequest.Location,
                    Halls           = new List<Hall>()
                };
    
                foreach(var hall in cinemaRequest.Halls)
                {

                    // return Json(hall);
                    var h = new Hall
                    {
                        CinemaId = cinema.Id,
                        HallName = hall.HallName,
                        SeatCapacity = hall.SeatCapacity
                    };
                    cinema.Halls.Add(h);
                }
                
                if (string.IsNullOrEmpty(cinemaRequest.CinemaName) || string.IsNullOrEmpty(cinemaRequest.Location) || cinemaRequest.Halls == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Unable to save with empty fields ",
                        result = cinema
                    });
                }
                else
                {
                    // return Json(cinema);
                    await cinemaService.AddAsync(cinema);
                    return Json(new { success = true, message = "Cinema created successfully", result = cinema });
                }
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { success = false, message = ex.Message, innerMessage = innerException });
            }
        }

        // If ModelState is invalid, return a bad request response
        return BadRequest(new 
        {
            success = false,
            message = "Invalid data",
            errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
        });
    }

}
