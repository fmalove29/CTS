using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CrudAsp.Extensions;
using CrudAsp.Models;
using CrudAsp.resource.response;
using CrudAsp.Extensions;

namespace CrudAsp.Controllers.Users;

public class ProfileController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<CrudAsp.Models.Users> _userManager;
    public ProfileController(RoleManager<IdentityRole> role, UserManager<CrudAsp.Models.Users> userManager)
    {
        _roleManager = role;
        _userManager = userManager;
    }

    public async Task<IActionResult> UserProfile(string Id)
    {
        var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            
            var userResponse = user.ToUserResponse();

            
            userResponse.Roles = new List<RoleResponseDTO>();

        
            var roles = await _userManager.GetRolesAsync(user);

        
            foreach (var roleName in roles)
            {
                // Find role details by role name
                var role = await _roleManager.FindByNameAsync(roleName);
                // return Json(role);
                if (role != null)
                {
                    userResponse.Roles.Add(new RoleResponseDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizeName = role.NormalizedName // Example of including additional details
                    });
                }
            }
            return Json(userResponse);
    }
}
