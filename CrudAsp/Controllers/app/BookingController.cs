
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
using CrudAsp.Services.Shows;
using CrudAsp.resource.response;
using CrudAsp.Extensions;
using CrudAsp.Services.Cinemas;

namespace CrudAsp.Controllers.app;

public class BookingController : Controller
{
    private readonly IRepository<Booking> repository;
    private readonly MovieService movieService;
    private readonly CinemaService cinemaService;
    private readonly ShowService _showService;


    public BookingController
    (
        IRepository<Booking> repository,
        IRepository<Cinema> cinemaRepository,
        MovieService movieService,
        ShowService showService
        // GenreService genreService
    )
    {
        this.repository = repository;
        this.movieService = movieService;
        cinemaService = new CinemaService(cinemaRepository);
        _showService = showService;
        // this.genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await movieService.GetAll();

        return Json(movies);
    }

    // public async Task<IActionResult> Show(Guid Id)
    // {
    //     var findMovie = await movieService.GetById(Id);
        

    //     if (findMovie == null)
    //     {
    //         return NotFound(); // Handle the case when the movie is not found
    //     }

    //     var dto = new MovieResponse
    //     {
    //         Id = findMovie.Id,
    //         Title = findMovie.Title,
    //         Description = findMovie.Description,
    //         ReleaseDate = findMovie.ReleaseDate,
    //         EndDate = findMovie.EndDate,
    //         Genres = findMovie.MovieGenres.Select(mg => new GenreResponse 
    //         {
    //             Id = mg.Genres.Id,
    //             GenreName = mg.Genres.GenreName
    //         }).ToList(),
    //         MovieImages = findMovie.MovieImages.Select(img => new MovieImageResponse
    //         {
    //             Name = img.Name,
    //             Size = img.Size,
    //             Type = img.Type,
    //             Path = img.Path,
    //             MovieId = img.MovieId
    //         }).ToList()
    //     };

    //     return Json(dto);
    // }
    public async Task<IActionResult> Show(Guid Id)
    {
        var movie = await (await movieService.GetDbSet())
            .Include(e => e.MovieGenres)
                .ThenInclude(e => e.Genres)
            .Include(e => e.MovieImages)
            .FirstOrDefaultAsync(e => e.Id == Id);

        // Check if movie is null before converting
        if (movie == null)
        {
            return NotFound(new { message = "Movie not found" });
        }

        var response = movie.ToMovieResponse(); 
        return View("booking-details",response);
    }

    [HttpGet]
    public async Task<IActionResult> Cinema(string searchTerm)
    {
        var cinema = await this.cinemaService.GetAllAsync();

        if(!string.IsNullOrEmpty(searchTerm))
        {
            cinema = cinema
                    .Where(c => c.CinemaName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                    || c.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        var cinemaResponseList = cinema.Select(c => c.ToSelectCinemaResponse()).ToList();
        return Ok(cinemaResponseList);
    }
    
    [HttpGet("cinema/{cinemaId}/shows")]
    public async Task<IActionResult> GetShowByCinemaId([FromRoute] Guid cinemaId)
    {
        var getShows = await (await cinemaService.GetDbSet())
                            .Where(e => e.Id == cinemaId)
                            .Include(e => e.CinemaShows)
                                .ThenInclude(cs => cs.Show)
                                    .ThenInclude(s => s.Movie) // Include Movie details
                            .ToListAsync();  

        var shows = getShows
                    .SelectMany(e => e.CinemaShows.Select(cs => cs.Show))
                    .Select(s => new ShowResponse
                    {
                        Id = s.Id,
                        MovieId = s.MovieId,
                        Movie = new MovieResponse // Include Movie details in the response
                        {
                            Id = s.Movie.Id,
                            Title = s.Movie.Title,
                            Description = s.Movie.Description,
                            ReleaseDate = s.Movie.ReleaseDate
                        },
                        HallId = s.HallId,
                        ShowDate = s.ShowDate,
                        TicketPrice = s.TicketPrice
                    })
                    .ToList();

        return Ok(shows);
    }

    [HttpGet("book/cinema")]
    public async Task<IActionResult> BookCinema([FromQuery] string searchTerm, Guid MovieId)
    {
        var query = (await cinemaService.GetDbSet())
                        .Include(e => e.Halls)
                            .ThenInclude(h => h.Shows)
                                .ThenInclude(s => s.Movie)
                        .AsEnumerable(); // Switch to in-memory processing

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower(); // Convert search term to lowercase
            query = query.Where(c => c.CinemaName.ToLower().Contains(searchTerm) ||
                                    c.Location.ToLower().Contains(searchTerm));
        }

        var cinemas = query.ToList(); // Convert to List

        // Transform data into response format
        var response = cinemas
            .Select(cinema => new CinemaResponse
            {
                Id = cinema.Id,
                CinemaName = cinema.CinemaName,
                Location = cinema.Location,
                Halls = cinema.Halls.Select(hall => new HallResponse
                {
                    Id = hall.Id,
                    HallName = hall.HallName,
                    CinemaId = cinema.Id,
                    SeatCapacity = hall.SeatCapacity,
                    CinemaFormatId = hall.CinemaFormatId,
                    Shows = hall.Shows
                        .Where(show => show.MovieId == MovieId) // Filter by MovieId
                        .Select(show => new ShowResponse
                        {
                            Id = show.Id,
                            MovieId = show.MovieId,
                            Movie = new MovieResponse
                            {
                                Id = show.Movie.Id,
                                Title = show.Movie.Title,
                                Description = show.Movie.Description,
                                ReleaseDate = show.Movie.ReleaseDate
                            },
                            ShowDate = show.ShowDate,
                            TicketPrice = show.TicketPrice,
                            HallId = show.HallId
                        }).ToList()
                })
                .Where(hall => hall.Shows.Any()) // Keep only halls with shows
                .ToList()
            })
            .Where(cinema => cinema.Halls.Any()) // Keep only cinemas with halls that have shows
            .ToList();

        return Ok(response);
    }

    [HttpGet("book/{showId}")]
    public async Task<IActionResult> GetShowById(Guid showId)
    {
        var getshow = await (await _showService.GetDbSet())
                        .Include(e => e.Hall)
                            .ThenInclude(h => h.CinemaFormat)
                        .Include(h => h.Hall)
                            .ThenInclude(c => c.Cinema) // Ensure CinemaFormat is loaded
                        .FirstOrDefaultAsync(s => s.Id == showId);

        if (getshow == null)
            return NotFound(new { message = "Show not found" });

        var show = new ShowResponse
        {
            Id = getshow.Id,
            MovieId = getshow.MovieId,
            HallId = getshow.HallId,
            TicketPrice =getshow.TicketPrice,
            Hall = new HallResponse
            {
                Id = getshow.Hall.Id,
                CinemaId = getshow.Hall.CinemaId,
                Cinema = getshow.Hall.Cinema != null ? new CinemaResponse
                {
                    Id = getshow.Hall.Cinema.Id,
                    CinemaName = getshow.Hall.Cinema.CinemaName,
                    Location = getshow.Hall.Cinema.Location
                } : null,
                HallName = getshow.Hall.HallName,
                SeatCapacity = getshow.Hall.SeatCapacity,
                CinemaFormatId = getshow.Hall.CinemaFormatId,
                CinemaFormat = getshow.Hall.CinemaFormat != null ? new CinemaFormatDTO
                {
                    Id  = getshow.Hall.CinemaFormat.Id,
                    ScreenTypeName = getshow.Hall.CinemaFormat.ScreenTypeName,
                    Description = getshow.Hall.CinemaFormat.Description,
                    Price = getshow.Hall.CinemaFormat.Price
                } : null  // Handle null case
            }
        };

        return Ok(show);
    }

}