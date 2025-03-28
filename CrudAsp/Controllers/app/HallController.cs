using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using CrudAsp.Services.Halls;
using CrudAsp.Services.CinemaFormat;
using CrudAsp.Services.Shows;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.resource.response;
using CrudAsp.resource.request;
using CrudAsp.Extensions;

namespace CrudAsp.Controllers.app;


[Authorize(Roles = "Admin")]
public class HallController : Controller
{ 
    private readonly IRepository<Hall> repository;
    private readonly IRepository<CinemaFormat> _cinemaFormat;
    private readonly HallService hallService;
    private readonly CinemaFormatService _cinemaFormatService;
    private readonly ShowService _showService;
    public HallController(IRepository<Hall> repository, CinemaFormatService cinemaFormatService, ShowService showService)
    {
        this.repository = repository;
        _showService = showService;
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
                        ScreenTypeName = e.CinemaFormat.ScreenTypeName, // Access from CinemaFormat
                        TicketPrice = e.CinemaFormat.Price
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

    [HttpPut("hall")]
    public async Task<IActionResult> PutHall([FromBody] HallResponse hallResponse)
    {
        try
        {
            var findHall = await hallService.GetHallByCinemaId(hallResponse.Id);
            var findShows = await (await hallService.GetDbSet())
                .Include(e => e.Shows)
                .Where(h => h.Id == hallResponse.Id)
                .SelectMany(h => h.Shows) 
                .ToListAsync(); 

            if (findHall == null)
            {
                return NotFound(new { success = false, message = "Hall not found." });
            }


            foreach (var show in findShows)
            {
                show.TicketPrice = hallResponse.TicketPrice;
                await _showService.UpdateAsync(show);
            }
            findHall.HallName = hallResponse.HallName;
            findHall.SeatCapacity = hallResponse.SeatCapacity;
            findHall.CinemaFormatId = hallResponse.CinemaFormatId;

            var updatedHall = await hallService.UpdateAsync(findHall);

            
            var response = new HallResponse
            {
                Id = updatedHall.Id,
                CinemaId = updatedHall.CinemaId,
                HallName = updatedHall.HallName,
                SeatCapacity = updatedHall.SeatCapacity,
                CinemaFormatId = updatedHall.CinemaFormatId,
                ScreenTypeName = updatedHall.CinemaFormat?.ScreenTypeName
            };

            return Ok(new { success = true, message = $"{hallResponse.Id} has been updated successfully", data = response });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message, innerMessage = ex.InnerException?.Message });
        }
    }

}
