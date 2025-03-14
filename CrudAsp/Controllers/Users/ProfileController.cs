using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CrudAsp.Extensions;
using CrudAsp.Models;
using CrudAsp.resource.response;
using System.Collections.Generic;

namespace CrudAsp.Controllers.Users
{
    [Authorize] // Ensures only authenticated users can access this controller
    public class ProfileController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<CrudAsp.Models.Users> _userManager;

        public ProfileController(RoleManager<IdentityRole> roleManager, UserManager<CrudAsp.Models.Users> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> UserProfile()
        {
            var currentUser = await _userManager.GetUserAsync(User); 
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var userResponse = currentUser.ToUserResponse();
            userResponse.Roles = new List<RoleResponseDTO>();

            var roles = await _userManager.GetRolesAsync(currentUser);
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    userResponse.Roles.Add(new RoleResponseDTO
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizeName = role.NormalizedName
                    });
                }
            }
            return View(userResponse);
        }

    }
}
