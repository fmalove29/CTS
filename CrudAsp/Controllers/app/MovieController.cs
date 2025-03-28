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
using Newtonsoft.Json;
using CrudAsp.Extensions;





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
        var movies = await movieService.GetDbSet(); 

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
            }).ToList(), 
            MovieImages = movie.MovieImages.Select(img => new MovieImageResponse
            {
                Path = img.Path,
                MovieId = img.MovieId
            }).ToList() 
        }).ToList();

        
        return View("Movie-list", movieRequests);
    }


    public async Task<IActionResult> Show(Guid Id)
    {
        var findMovie = await movieService.GetById(Id);

        if (findMovie == null)
        {
            return NotFound(); // Handle not found case
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




    public async Task<IActionResult> Create()
    {
        var gService = await movieService.GetAllGenre();


        var data = new MovieResponse
        {
            Genres = gService.Select(b => new GenreResponse
            {
                GenreName = b.GenreName,
                Id = b.Id,
            }).ToList()
        };


        return View("Movie-Details", data);
    }


    [HttpPost]
    public async Task<IActionResult> PostMovie([FromForm] MovieRequest movieRequest)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { success = false, message = "Invalid data", errors = ModelState, data = movieRequest });
        }

        try
        {
            var newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = movieRequest.Title,
                Description = movieRequest.Description,
                ReleaseDate = movieRequest.ReleaseDate,
                EndDate = movieRequest.EndDate,
                MovieGenres = new List<MovieGenre>(),
                MovieImages = new List<MovieImage>()
            };

            if (!string.IsNullOrEmpty(movieRequest.MovieGenresJson))
            {
                newMovie.MovieGenres = JsonConvert.DeserializeObject<List<MovieGenre>>(movieRequest.MovieGenresJson);
            }

            string folderPath = Path.Combine("wwwroot", "images", "movies");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in movieRequest.MovieImages)
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(folderPath, fileName);
                    string dbPath = Path.Combine("images", "movies", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    newMovie.MovieImages.Add(new MovieImage
                    {
                        Name = file.FileName,
                        Size = (int)file.Length,
                        Type = file.ContentType,
                        Path = dbPath,
                        MovieId = newMovie.Id
                    });
                }
            }
            // return Json(newMovie);
            // ✅ Save Movie to Database (Fix placement of `await`)
            await movieService.AddAsync(newMovie);

            return Json(new { success = true, message = "Movie created successfully", result = newMovie });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
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
            await movieService.Update(existingMovie);

            return Json(new { success = true, message = "Movie updated successfully", result = existingMovie });
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException?.Message;
            return Json(new { success = false, message = ex.Message, innerMessage = innerException });
        }
    }

    [HttpGet("movie-select")]
    public async Task<IActionResult> MovieToSelect(string searchTerm)
    {
        var movie = await movieService.GetAll();
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            movie = movie.Where(m => m.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        var movieResponseList = movie.Select(m => m.ToMovieSelectResponse()).ToList();

        return Ok(movieResponseList);
    }

}