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
        try
        {
            var list = await _cinemaFormatService.GetAllAsync();

            var formattedList = list.Select(e => new CinemaFormatDTO
            {
                Id         = e.Id,
                ScreenType = e.ScreenType,
                ScreenTypeName = e.ScreenType.ToString(), // Convert enum to string
                Description = e.Description,
                Price = e.Price
            }).ToList();

            return Ok(new { success = true, data = formattedList });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
    }

    [HttpPost("cinema-format")]
    public async Task<IActionResult> PostFormat([FromBody] CinemaFormatDTO cinemaFormat)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); 
        }

        try
        {
            var dbSet = await _cinemaFormatService.GetDbSet(); // Await the DbSet

            
            bool exists = await dbSet.AnyAsync(e => e.ScreenType == cinemaFormat.ScreenType);
            if (exists)
            {
                return BadRequest(new { success = false, message = "Screen type already exists." });
            }

            CinemaFormat cf = new CinemaFormat();
            cf.ScreenType = cinemaFormat.ScreenType;
            cf.ScreenTypeName = cinemaFormat.ScreenTypeName;
            cf.Description = cinemaFormat.Description;
            cf.Price = cinemaFormat.Price;

            var response = await _cinemaFormatService.AddAsync(cf);

            return Ok(new { success = true, message = "New Format Added Successfully" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }

        


        return Json(cinemaFormat);
    }

    [HttpPut("cinema-format")]
    public async Task<IActionResult> PutFormat([FromBody] CinemaFormatDTO cinemaFormat)
    {
        // return Json(cinemaFormat.ScreenType);
        try
        {
    
            var findFormat = await _cinemaFormatService.GetByIdAsync(cinemaFormat.Id);

            if (findFormat == null) // Fix: Check if it's null
            {
                return NotFound(new { success = false, message = "Format not found." });
            }


            findFormat.Description = cinemaFormat.Description;
            findFormat.Price = cinemaFormat.Price;

            var updatedCinemaFormat = await _cinemaFormatService.UpdateAsync(findFormat);

            var cf = new CinemaFormatDTO()
            {
                Id = updatedCinemaFormat.Id,
                ScreenType = updatedCinemaFormat.ScreenType,
                ScreenTypeName = updatedCinemaFormat.ScreenTypeName,
                Description = updatedCinemaFormat.Description,
                Price = updatedCinemaFormat.Price
            };

            return Ok(new { success = true, message = $"{cinemaFormat.Id} format has been updated successfully", data = cf });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
    }

}
