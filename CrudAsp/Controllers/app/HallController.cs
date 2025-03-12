using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using CrudAsp.Services.Halls;
using CrudAsp.Services.CinemaFormat;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.resource.response;
using CrudAsp.resource.request;
using CrudAsp.Extensions;

namespace CrudAsp.Controllers.app;

public class HallController : Controller
{ 
    private readonly IRepository<Hall> repository;
    private readonly IRepository<CinemaFormat> _cinemaFormat;
    private readonly HallService hallService;
    private readonly CinemaFormatService _cinemaFormatService;
    public HallController(IRepository<Hall> repository, CinemaFormatService cinemaFormatService)
    {
        this.repository = repository;
        hallService = new HallService((Repository<Hall>) repository);
        _cinemaFormatService = cinemaFormatService;
    }

    [HttpGet("hall/{Id}")]
    public async Task<IActionResult> Hall(Guid Id)
    {
        var halls = await hallService.GetDbSet(); // Await the task first

        var data = halls
                    .AsQueryable() // Convert to IQueryable to use Include
                    .Include(e => e.CinemaFormat) // Include related data
                    .Where(e => e.CinemaId == Id)
                    .Select(e => new HallResponse
                    {
                        Id = e.Id,
                        CinemaId = e.CinemaId,
                        HallName = e.HallName,
                        SeatCapacity = e.SeatCapacity,
                        CinemaFormatId = e.CinemaFormatId,
                        ScreenTypeName = e.CinemaFormat.ScreenTypeName // Access from CinemaFormat
                    })
                    .ToList(); // Execute query

        return Ok(data);
    }






    [HttpPost("hall")]
    public async Task<IActionResult> postHall([FromBody] HallResponse hallResponse)
    {
        try
        {
            var hall = new CrudAsp.Models.app.Hall(){
                CinemaId = hallResponse.CinemaId,
                HallName = hallResponse.HallName,
                SeatCapacity = hallResponse.SeatCapacity,
                CinemaFormatId = hallResponse.CinemaFormatId
            };

            var response = await hallService.AddAsync(hall);
            return Ok(new { success = true, message = "New Hall Added Successfully" , data = response});
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }

        return Ok(hallResponse);
    }
}
