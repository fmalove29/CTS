using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using CrudAsp.Services.Halls;
using CrudAsp.Models.app;
using CrudAsp.Repository;
using CrudAsp.resource.response;
using CrudAsp.resource.request;
using CrudAsp.Extensions;

namespace CrudAsp.Controllers.app;

public class HallController : Controller
{ 
    private readonly IRepository<Hall> repository;
    private readonly HallService hallService;
    public HallController(IRepository<Hall> repository)
    {
        this.repository = repository;
        hallService = new HallService((Repository<Hall>) repository);
    }

    public async Task<IActionResult> getHallByCinemaId(Guid Id)
    {
        var hallDbSet = await hallService.GetDbSet();

        // Fetch all halls associated with the given CinemaId
        var halls = await hallDbSet
            .Where(c => c.CinemaId == Id)
            .ToListAsync();

        // Return the list of halls as a JSON response
        return Json(halls);
    }

    public async Task<IActionResult> postHall(HallResponse hallResponse)
    {
        return Json(hallResponse);
    }
}
