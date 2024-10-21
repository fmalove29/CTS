using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Models.app;
using System.Linq;
using CrudAsp.Services.Movies;
using CrudAsp.Services.Genres;
using CrudAsp.Repository;
using Microsoft.AspNetCore.Authorization;
using CrudAsp.resource.request;
using CrudAsp.resource.response;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;




namespace CrudAsp.Controllers.app;

[Authorize]
public class MovieController : Controller
{
    private readonly MovieService movieService;
    private readonly GenreService genreService;
    private readonly IRepository<Movie> repository;

    public MovieController
    (
        IRepository<Movie> repository,
        IRepository<Genre> genreRepo

    )
    {
        this.repository = repository;
        movieService = new MovieService((Repository<Movie>)repository, (Repository<Genre>)genreRepo);


    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var movies = await movieService.GetAll(); // Movies loaded without related entities

        var movieRequests = movies.Select(movie => new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            EndDate = movie.EndDate,
            Genres = movie.Genres.Select(g => new GenreResponse
            {
                Id = g.Id,
                GenreName = g.GenreName
            }).ToList(), // Genres will be lazy-loaded here
            MovieImages = movie.MovieImages.Select(img => new MovieImageResponse
            {
                Path = img.Path,
                MovieId = img.MovieId
            }).ToList() // MovieImages will be lazy-loaded here
        }).ToList();

        return View("Movie-list", movieRequests);
    }


    [HttpGet]
    public async Task<IActionResult> Details(Guid Id)
    {

        // Fetch all genres
        var gService = await movieService.GetAllGenre();
        // return Json(gService);
        // Initialize savedId to hold genre IDs for the current movie
        var savedId = "";

        // Get the movie with its genres and images
        var movie = await movieService.GetById(Id);


        // Return 404 if the movie is not found
        if (movie == null)
        {
            return NotFound();
        }

        // Populate savedId with the genre IDs linked to the current movie
        if (movie.Genres != null && movie.Genres.Any())
        {
            savedId = string.Join(",", movie.Genres.Select(g => g.Id.ToString()));
        }

        // Map the movie entity to the MovieRequest model
        var data = new MovieResponse
        {
            Id = movie.Id,
            Title = movie.Title,
            Description = movie.Description,
            ReleaseDate = movie.ReleaseDate,
            EndDate = movie.EndDate,
            Genres = gService?.Select(g => new GenreResponse
            {
                Id = g.Id,
                GenreName = g.GenreName
            }).ToList() ?? new List<GenreResponse>(),
            MovieImages = movie.MovieImages?.Select(img => new MovieImageResponse
            {
                Id = img.Id,
                Name = img.Name,
                Size = img.Size,
                Type = img.Type,
                Path = img.Path,
                MovieId = img.MovieId
            }).ToList() ?? new List<MovieImageResponse>()
        };

        // Pass the saved genre IDs to the view using ViewBag
        ViewBag.SaveId = savedId;
        // return Json(data);
        // Return the view with the movieRequest model
        return View("Movie-Details", data);
    }








    // public async Task<IActionResult> Details(Guid Id)
    // {
    //     var data = await movieService.GetByIdAsync(Id);
    //     return Json(data);
    //     return View("Movie-details", data);
    // }

    public async Task<IActionResult> Create()
    {
        var gService = await movieService.GetAllGenre();


        var data = new MovieResponse
        {
            Genres = gService.Select(b => new GenreResponse
            {
                GenreName = b.GenreName,
                Id = b.Id,
                // Movies = m.Select(mo => new MovieRequest{
                //     Title = mo.Title
                // }).ToList()
            }).ToList()
        };

        // return Json(data);

        return View("Movie-Details", data);
    }


    [HttpPost]
    public async Task<IActionResult> PostMovie([FromBody] MovieRequest movieRequest)
    {
        // return Json(movieRequest);
        if (ModelState.IsValid)
        {
            try
            {
                var newMovie = new Movie
                {
                    Id = Guid.NewGuid(),
                    Title = movieRequest.Title,
                    Description = movieRequest.Description,
                    ReleaseDate = movieRequest.ReleaseDate,
                    EndDate = movieRequest.EndDate,
                    Genres = new List<Genre>(),
                    MovieImages = new List<MovieImage>()
                };
                // return Json(newMovie);
                foreach (var gen in movieRequest.Genres)
                {
                    var newGenre = new Genre
                    {
                        Id = gen.Id,
                        GenreName = gen.GenreName
                    };
                    newMovie.Genres.Add(newGenre);
                }

                string pathFolder = Path.Combine("wwwroot", "app", "images", "movie");
                string savePathFolder = Path.Combine("app", "images", "movie");

                // return Json(savePathFolder);

                if (!Directory.Exists(pathFolder))
                {
                    Directory.CreateDirectory(pathFolder);

                }

                foreach (var mg in movieRequest.MovieImages)
                {
                    byte[] imageBytes = Convert.FromBase64String(mg.Base64File);

                    // Generate file name and path
                    string fileName = newMovie.Id + Path.GetExtension(mg.Name);
                    string filePath = Path.Combine(pathFolder, fileName);
                    // Save image to the folder
                    await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                    var newImage = new MovieImage
                    {
                        Name = mg.Name,
                        Size = mg.Size,
                        Type = mg.Type,
                        Base64File = mg.Base64File,
                        MovieId = newMovie.Id,
                        Path = filePath
                    };
                    newMovie.MovieImages.Add(newImage);
                }
                // return Json(newMovie);
                await movieService.AddAsync(newMovie);

                return Json(new { success = true, message = "Movie created successfully", result = newMovie });
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message;
                return Json(new { success = false, message = ex.Message, innerMessage = innerException });
            }
        }

        return Json(new { success = false, message = "Invalid data", errors = ModelState });
    }

    [HttpPut]
    public async Task<IActionResult> PutMovie([FromBody] MovieRequest movieRequest)
    {
        try
        {
            var existingMovie = await movieService.GetById(movieRequest.Id);

            if (existingMovie == null)
            {
                return Json(new { success = false, message = "Movie Not Found", result = existingMovie });
            }

            existingMovie.Title = movieRequest.Title;
            existingMovie.Description = movieRequest.Description;
            existingMovie.ReleaseDate = movieRequest.ReleaseDate;
            existingMovie.EndDate = movieRequest.EndDate;
            existingMovie.Genres = new List<Genre>();

            foreach (var gen in movieRequest.Genres)
            {
                var genId = await genreService.GetAllAsync();
                foreach(var i in genId)
                {
                    if(gen.Id == i.Id)
                    {
                        return Json("Yes");
                    }
                }
            }
            return Json(existingMovie);
            // Save changes
            await movieService.Update(existingMovie);

            return Json(new { success = true, message = "Movie updated successfully", result = existingMovie });
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException?.Message;
            return Json(new { success = false, message = ex.Message, innerMessage = innerException });
        }
    }



}