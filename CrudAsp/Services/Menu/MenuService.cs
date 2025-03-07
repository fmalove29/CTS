using CrudAsp.Models.app;
using CrudAsp.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrudAsp.resource.request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudAsp.Services.Genres;
using CrudAsp.resource.response;

namespace CrudAsp.Services.Menu;

public class MenuService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<MenuItem> _menuRepository;
    private readonly RoleManager<IdentityRole> _roleManager;


    public MenuService(IHttpContextAccessor httpContextAccessor, IRepository<MenuItem> menuRepository, RoleManager<IdentityRole> roleManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _menuRepository = menuRepository;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<MenuItem>> GetMenusForUserRoleAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null)
            return Enumerable.Empty<MenuItem>();

        // Extract roles from user claims
        var roleClaims = user.Claims
            .Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            .Select(c => c.Value)
            .ToList();

        var roles = new List<IdentityRole>();

        foreach (var roleName in roleClaims)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                roles.Add(role);
            }
        }

        var roleIds = roles.Select(role => role.Id).ToList();

        // Fetch menu items accessible to the user's roles
        var menuItems = await (await _menuRepository.GetDbSet())
            .Where(menuItem => menuItem.AccessMenus
                .Any(accessMenu => roleIds.Contains(accessMenu.RoleId)))
            .ToListAsync();

        return menuItems;
    }











}

