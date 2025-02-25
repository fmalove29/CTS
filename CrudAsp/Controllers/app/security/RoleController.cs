using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace CrudAsp.Controllers.app.security;

[Authorize]
public class RoleController : Controller
{
    private readonly UserManager<CrudAsp.Models.Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(UserManager<CrudAsp.Models.Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task<IActionResult> Index()
    {
        return View();
    }
    [HttpGet("role/list")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = _roleManager.Roles;
        return Ok(roles);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRoleToUser([FromBody] CrudAsp.resource.response.RoleDTO roleDTO)
    {
        // Find the user by UserId
        // return Json(roleDTO);
        var user = await _userManager.FindByIdAsync(roleDTO.UserId);
        // return Json(user);
        if (user == null)
            return NotFound("Not" + user);
        
        // Find the role by RoleId
        var role = await _roleManager.FindByIdAsync(roleDTO.RoleId);
        // return Json(role);
        if (role == null)
            return NotFound($"Role with ID '{ roleDTO.RoleId}' not found.");

        // return Json(role);
        // Assign the role to the user
        var result = await _userManager.AddToRoleAsync(user, role.Name);

        return Json(result);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

    }

    [HttpPost("create-role")]
    public async Task<IActionResult> AddNewRole([FromBody] CrudAsp.resource.response.CreateRoleDTO createRoleDTO)
    {
        if (string.IsNullOrWhiteSpace(createRoleDTO.Name))
        {
            return BadRequest("Role name cannot be empty.");
        }

        // Check if the role already exists
        var roleExists = await _roleManager.RoleExistsAsync(createRoleDTO.Name);
        if (roleExists)
        {
            return BadRequest($"The role '{createRoleDTO.Name}' already exists.");
        }

        // Create the role
        var result = await _roleManager.CreateAsync(new IdentityRole(createRoleDTO.Name));
        if (result.Succeeded)
        {
            return Ok($"Role '{createRoleDTO.Name}' created successfully.");
        }

        // Handle errors
        return BadRequest(result.Errors);
    }

    
}
