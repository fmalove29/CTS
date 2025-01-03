
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

namespace CrudAsp.Controllers.app;

public class BookingController : Controller
{
    private readonly IRepository<Booking> repository;
    private readonly MovieService movieService;

    public BookingController
    (
        IRepository<Booking> repository,
        MovieService movieService
    )
    {
        this.repository = repository;
        this.movieService = movieService;
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
        return Json(findMovie);
    }
}
