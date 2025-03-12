using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using CrudAsp.Models.app.Enum;
using CrudAsp.Models.app;
using CrudAsp.resource.response;
using CrudAsp.Services.CinemaFormat;
using CrudAsp.Repository;

namespace CrudAsp.Controllers.app;

public class CinemaFormatController : Controller
{
    private readonly IRepository<CrudAsp.Models.app.CinemaFormat> _repository;
    private readonly CinemaFormatService _cinemaFormatService;
    public CinemaFormatController(IRepository<CrudAsp.Models.app.CinemaFormat> repository)
    {
        _repository = repository;
        _cinemaFormatService = new CinemaFormatService((Repository<CinemaFormat>) repository);
    }

    [HttpGet]
    public IActionResult GetScreenTypes()
    {
        var screenTypes = Enum.GetValues(typeof(ScreenType))
            .Cast<ScreenType>()
            .Select(e => new { Id = (int)e, Name = e.ToString() })
            .ToList();

        return Json(screenTypes);
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet("cinema-format/api/list")]
    public async Task<IActionResult> List()
    {
        var list = await _cinemaFormatService.GetAllAsync();
        return Json(list);
    }
    [HttpPost("cinema-format/Add")]
    public async Task<IActionResult> PostFormat([FromBody] CinemaFormatDTO cinema)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            var dbSet = await _cinemaFormatService.GetDbSet(); // Await the DbSet

            
            bool exists = await dbSet.AnyAsync(e => e.ScreenType == cinema.ScreenType);
            if (exists)
            {
                return BadRequest(new { success = false, message = "Screen type already exists." });
            }

            CinemaFormat cf = new CinemaFormat();
            cf.ScreenType = cinema.ScreenType;
            cf.ScreenTypeName = cinema.ScreenTypeName;
            cf.Description = cinema.Description;
            cf.Price = cinema.Price;

            var response = await _cinemaFormatService.AddAsync(cf);

            return Ok(new { success = true, message = "New Format Added Successfully" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }

        


        return Json(cinema);
    }
}
