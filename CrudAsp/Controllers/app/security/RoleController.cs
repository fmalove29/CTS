using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using CrudAsp.Models.app.Enum;
using CrudAsp.Models.app;

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
        if (string.IsNullOrWhiteSpace(createRoleDTO.Name) || createRoleDTO.Name != createRoleDTO.Name.ToUpper())
        {
            return BadRequest("Role name cannot be empty or should be in uppercase");
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
    
    [Authorize(Roles = "Admin")]
    [HttpGet("management-level")]
    public async Task<IActionResult> GetManagementRole()
    {
        var managementLevel = Enum.GetValues(typeof(ManagementLevel))
            .Cast<ManagementLevel>()
            .Select(e => new { Id = (int)e, Name = e.ToString() })
            .ToList();

        return Json(managementLevel);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("role-claims")]
    public async Task<IActionResult> PostRoleClaims([FromBody] RoleClaim roleClaim)
    {
        var findRole = await _roleManager.FindByIdAsync(roleClaim.RoleId.ToString());
        
        if(findRole == null || roleClaim.ClaimValue == null)
        {
            return NotFound(new { code = 404, message = "No Role Found or No Management Level Selected"});
        }

        var claim = new Claim(roleClaim.ClaimType, roleClaim.ClaimValue);

        var existingClaims = await _roleManager.GetClaimsAsync(findRole);
        bool claimExists = existingClaims.Any(c => c.Type == roleClaim.ClaimType && c.Value == roleClaim.ClaimValue);

        if(!claimExists)
        {
            var result = await _roleManager.AddClaimAsync(findRole, claim);
            return result.Succeeded
            ? Ok(new { success = true, message = $"Claim { roleClaim.ClaimValue } Added successfully" })
            : BadRequest("Error adding claim");
        }
        return BadRequest(new { success = false, message = $"Claim {roleClaim.ClaimValue} Already exist"});
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("role-claims/{roleId}")]
    public async Task<IActionResult> GetClaimById(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return NotFound(new { statusCode = 404, message = "Role not found" });
        }
        var claims = await _roleManager.GetClaimsAsync(role);

        return Ok(claims.Select(c => new {c.Type, c.Value }));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete-roleClaim/{roleId}")]
    public async Task<IActionResult> RemoveClaim(string roleId, [FromBody] RoleClaim roleClaim)
    {
        if (roleClaim == null)
        {
            return BadRequest(new { statusCode = 400, message = "Invalid role claim data" });
        }

        var role = await _roleManager.FindByIdAsync(roleId); // Use roleId from URL
        if (role == null)
        {
            return NotFound(new { statusCode = 404, message = "Role not found" });
        }

        var claim = new Claim(roleClaim.ClaimType, roleClaim.ClaimValue);
        var result = await _roleManager.RemoveClaimAsync(role, claim);

        return result.Succeeded 
            ? Ok(new { success = true, statusCode = 200, message = $"Claim {roleClaim.ClaimValue} removed successfully" }) 
            : BadRequest(new { statusCode = 400, message = "Error removing claim" });
    }


}
