
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using CrudAsp.resource.ViewModels;
using CrudAsp.Models.app;
using CrudAsp.Services.Menu;
using CrudAsp.Repository;
using CrudAsp.resource.response;

namespace CrudAsp.Controllers.app.security;

public class MenuController : Controller
{
    private readonly MenuService _menuService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public MenuController(MenuService menuService,  RoleManager<IdentityRole> roleManager)
    {
        _menuService = menuService;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> GetUserMenus()
    {
        // Assuming you are fetching a list of movies (or some other data)
        var userMenus = await _menuService.GetMenusForUserRoleAsync();

        // Transform the data to match the expected model type (MenuItemVM)
        var menuItems =  userMenus?.Select(m => new MenuItemVM
        {
            Name = m.Name,
            Path = m.Path,
            // Ensure other properties are mapped as necessary
        }).ToList();
        ViewData["Message"] = "Welcome to the Razor View!";
        // Make sure you pass the right model to the view
        return Json(menuItems);
        // Return the correct partial view
        return PartialView("/Views/Shared/_SecurityLayout.cshtml", menuItems);
    }




}
