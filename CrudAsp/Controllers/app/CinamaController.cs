using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CrudAsp.Services.Cinemas;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.resource.response;
using CrudAsp.resource.request;


namespace CrudAsp.Controllers.app;

[Authorize]
public class CinemaController : Controller
{
    private readonly CinemaService cinemaService;

    private readonly IRepository<Cinema> repository;
    public CinemaController
    (
        IRepository<Cinema> repository
    )
    {
        this.repository = repository;
        cinemaService = new CinemaService((Repository<Cinema>) repository);
    }

    public async Task<IActionResult> Index(string searchQuery)
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

        // Pass the search query back to the view so it can be displayed in the input
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

        var cinemaById  =  new CinemaResponse{
            Id          = cinema.Id,
            CinemaName  = cinema.CinemaName,
            Location    = cinema.Location,
            created_at  = cinema.created_at
        };

        return View(cinemaById);
    }
    [HttpPost]
    public async Task<IActionResult> PostCinema([FromBody] CinemaRequest cinemaRequest)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var cinema = new Cinema
                {
                    CinemaName = cinemaRequest.CinemaName,
                    Location = cinemaRequest.Location
                };

                if (string.IsNullOrEmpty(cinemaRequest.CinemaName) || string.IsNullOrEmpty(cinemaRequest.Location))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Unable to save with empty fields",
                        result = cinema
                    });
                }
                else
                {
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
