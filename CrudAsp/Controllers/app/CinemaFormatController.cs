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

namespace CrudAsp.Controllers.app;

public class CinemaFormatController : Controller
{
    public CinemaFormatController()
    {
        
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
}
