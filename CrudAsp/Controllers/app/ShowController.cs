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
using CrudAsp.Services.Halls;
using CrudAsp.resource.response;




namespace CrudAsp.Controllers.app;


public class ShowController : Controller
{
    private readonly ShowService showService;
    private readonly MovieService movieService;
    private readonly HallService _hallService;
    private readonly IRepository<Show> repository;
    private readonly IRepository<Hall> _hallRepo;

    public ShowController(IRepository<Show> repository, MovieService movieService, HallService hallService)
    {
        this.repository = repository;
        this.movieService = movieService;

        _hallService = hallService;
        showService = new ShowService((Repository<Show>)repository);
    }

    public async Task<IActionResult> Index()
    {
        var movie = await movieService.GetAll();
        return Json(movie);
    }   

    [HttpGet("show/{showId}")]
    public async Task<IActionResult> GetShowsById(Guid showId)
    {
        // var data = await showService.GetById(Id);
        var data = await(await showService.GetDbSet())
                    .Include(e => e.Movie)
                    .FirstOrDefaultAsync(s => s.Id == showId);

        if(data == null)
        {
            return NotFound(new {success = false, message = $"{showId} Not Found"});
        }
        var show = new ShowResponse
        {
            Id = data.Id,
            MovieId = data.MovieId,
            HallId = data.HallId,
            ShowDate = data.ShowDate,
            TicketPrice = data.TicketPrice,
            Movie = new MovieResponse{
                Id = data.Movie.Id,
                Title = data.Movie.Title,
                Description = data.Movie.Description,
                ReleaseDate = data.Movie.ReleaseDate,
                EndDate = data.Movie.EndDate
            }
        };
        return Ok(show);
    }
    
    // [HttpGet("/movies/getAll")]
    // public async Task<IActionResult> GetAllMovies()
    // {
    //     var movies = await movieService.GetAll();
    //     return Json(movies.Select(m => new { id = m.Id, title = m.Title }));
    // }
    [HttpGet("hall/{HallId}/show")]
    public async Task<IActionResult> GetShowByHallId(Guid HallId)
    {
        var hall = await (await _hallService.GetDbSet())
                        .Include(h => h.Shows)
                            .ThenInclude(s => s.Movie)
                                .ThenInclude(m => m.MovieGenres)
                                    .ThenInclude(mg => mg.Genres)
                        .Include(h => h.Shows)
                            .ThenInclude(s => s.Movie)
                                .ThenInclude(m => m.MovieImages)
                        .FirstOrDefaultAsync(h => h.Id == HallId);

        if (hall == null || hall.Shows == null || !hall.Shows.Any())
        {
            return NotFound("No shows found for this hall.");
        }

        var response = hall.Shows.Select(show => new ShowResponse
        {
            Id = show.Id,
            MovieId = show.MovieId,
            HallId = show.HallId,
            ShowDate = show.ShowDate,
            TicketPrice = show.TicketPrice,
            Movie = new MovieResponse
            {
                Id = show.Movie.Id,
                Title = show.Movie.Title,
                Description = show.Movie.Description,
                ReleaseDate = show.Movie.ReleaseDate,
                EndDate = show.Movie.EndDate,
                MovieImages = show.Movie.MovieImages.Select(mi => new MovieImageResponse{
                    Name = mi.Name,
                    Size = mi.Size,
                    Type = mi.Type,
                    Path = mi.Path,
                    MovieId = mi.MovieId
                }).ToList(),
                Genres = show.Movie.MovieGenres.Select(mg => new GenreResponse
                {
                    Id = mg.Genres.Id,  
                    GenreName = mg.Genres.GenreName  
                }).ToList()
            }
        }).ToList();

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ShowResponse show)
    {
        if (show == null) 
        {
            return BadRequest(new { success = false, message = "Unable to Save with empty fields" });
        }

        var newShow = new Show
        {
            MovieId = show.MovieId,
            HallId = show.HallId,
            ShowDate = show.ShowDate,
            TicketPrice = show.TicketPrice
        };

        var response = await showService.AddAsync(newShow);

        if (response != null)
        {
            return Ok(new { success = true, HallId = newShow.HallId, message = $"New show added to Hall {show.HallId}" });
        }

        return BadRequest(new { success = false, message = "Failed to add show" });

    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] ShowResponse show)
    {
        try
        {
            var findShow = await showService.GetById(show.Id);

            if (findShow == null)
            {
                return NotFound(new { success = false, message = $"Show {show.Id} not found" });
            }

            findShow.ShowDate = show.ShowDate;

            var updatedShow = await showService.UpdateAsync(findShow);

            return Ok(new { success = true, HallId = show.HallId, message= $"{show.Id} updated sucessfully"});
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
    }

    [HttpDelete("show/{showId}")]
    public async Task<IActionResult> RemoveShow(Guid showId)
    {
        try
        {
            var findShow = await showService.GetById(showId);
            if (findShow == null)
            {
                return Ok(new { success = false, message = $"Show {showId} not found" });
            }

            var response = await showService.RemoveAsync(findShow);
            
            if (response == null)
            {
                return Ok(new { success = false, message = $"Failed to remove Show {showId}" });
            }

            return Ok(new { success = true, hallId = findShow.HallId, message = $"Show {showId} removed successfully" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
    }

}
