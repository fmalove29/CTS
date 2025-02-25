using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using CrudAsp.Extensions;
using CrudAsp.Models;
using CrudAsp.resource.response;
using CrudAsp.Extensions;

namespace CrudAsp.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserManager<CrudAsp.Models.Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<CrudAsp.Models.Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Action to get all users

        [Authorize(Roles = "Admin")]
        [HttpGet("User")]
        public async Task<IActionResult> Index()
        {
            return View("User/Index"); 
        }

        [HttpGet("user-role")]
        public async Task<IActionResult> GetAllUser()
        {
            var users = _userManager.Users.ToList(); // Get all users
            var userDataList = new List<UserResponseDTO>();

            foreach (var q in users)
            {
                var userData = new UserResponseDTO
                {
                    Id        = q.Id,
                    FirstName = q.FirstName,
                    LastName = q.LastName,
                    MiddleName = q.MiddleName != null ? q.MiddleName : string.Empty,
                    Email = q.Email,
                };

                userDataList.Add(userData);
            }
            return Json(userDataList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user-role/list/{Id}")]
        public async Task<IActionResult> GetRoleByUserId(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if(user == null)
            return NotFound("User not found.");


            // return Json(user); 
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
            return Json(userResponse.Roles);
        }

        // [HttpGet("/User/Details/{Id}")]
        // public async Task<IActionResult> GetUserRole(string Id)
        // {
        //     var user = await _userManager.FindByIdAsync(Id);
        //     if (user == null)
        //     {
        //         return NotFound("User not found");
        //     }

        //     var roles = await _userManager.GetRolesAsync(user);
        //     var roleDetails = roles.Select(r => new 
        //     {
        //         RoleId = r,
        //         RoleName = r,
        //         Description = r // Adjust if you need more properties from role
        //     }).ToList();

        //     return Json(roles);
        // }
        
        [HttpGet("user/details/{id}")]
        public async Task<IActionResult> GetUserRole(string id)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Map user details to the UserResponseDTO
            var userResponse = user.ToUserResponse();

            // Initialize the Roles property as a list of RoleResponseDTO
            userResponse.Roles = new List<RoleResponseDTO>();

            // Retrieve the roles the user belongs to
            var roles = await _userManager.GetRolesAsync(user);

            // Retrieve detailed role information
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
            // return Json(userResponse);
            return View("User/UserDetails",userResponse);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPost("delete/user-role")]
        public async Task<IActionResult> deleteUserRole([FromBody] RoleDTO roleDTO)
        {
            return Json(roleDTO.UserId);
            var user = await _userManager.FindByIdAsync(roleDTO.UserId);
            if (user == null)
            {
                return NotFound();
            }

            // Find the role by its ID or Name (assuming your RoleDTO includes roleId)
            var role = await _roleManager.FindByIdAsync(roleDTO.RoleId) ?? await _roleManager.FindByNameAsync(roleDTO.RoleId);
            if (role == null)
            {
                return BadRequest(new { message = "Role not found" });
            }

            // Remove user from role
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return Ok(new { message = "User removed from role successfully" });
            }
            else
            {
                return BadRequest(new { errors = result.Errors });
            }
        }
        
        [HttpPost("user/claims")]
        public async Task<IActionResult> GetUserClaims([FromBody] UserClaimDTO userClaimDTO)
        {
            return Ok(userClaimDTO);
        }
    }
}
