// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using CrudAsp.Repository;
// using CrudAsp.Models;
// using CrudAsp.Services;
// using Microsoft.AspNetCore.Mvc;

// namespace CrudAsp.Controllers.Users
// {
//     public class UserController : Controller
//     {
//         // private readonly UserService userService;

//         // public UserController(IRepository<CrudAsp.Models.Users> repository)
//         // {
//         //     userService = new UserService((Repository<CrudAsp.Models.Users>) repository);
//         // }

//         // [HttpGet]
//         // public async Task<IActionResult> Index()
//         // {
//         //     var users = await userService.GetAllAsync();
//         //     return View("users-list", users);
//         // }

//         // [HttpGet]
//         // public IActionResult Create()
//         // {
//         //     var model = new CrudAsp.Models.Users();
//         //     return View(model);
//         // }

//         // [HttpPost]
//         // public async Task<IActionResult> Create(CrudAsp.Models.Users users)
//         // {
//         //     return Ok(users);
//         //     if (ModelState.IsValid)
//         //     {
//         //         // Create a new user instance
//         //         var newUser = new CrudAsp.Models.Users
//         //         {
//         //             firstName = users.firstName,
//         //             lastName = users.lastName,
//         //             MiddleName = users.MiddleName,
//         //             Age = users.Age
//         //         };

//         //         // Add the new user to the database
//         //         await userService.AddAsync(newUser);

//         //         // Redirect to the Index action to display the updated user list
//         //         return RedirectToAction("Index");
//         //     }

//         //     // If the model state is invalid, return the same view with the current model
//         //     return View(users);
//         // }

//         // [HttpGet]
//         // public async Task<IActionResult> GetUserById(Guid Id)
//         // {
//         //     var u = await userService.GetByIdAsync(Id);

//         //     return View("create", u);
//         // }

//         // [HttpPost]
//         // public async Task<IActionResult> UpdateUser(CrudAsp.Models.Users users)
//         // {
//         //     if (ModelState.IsValid)
//         //     {
//         //         var existingUser = await userService.GetByIdAsync(users.Id);
//         //         // If the user is not found, return a 404 NotFound response
//         //         if (existingUser == null)
//         //         {
//         //             return NotFound();
//         //         }

//         //         // Update the properties of the existing user with the new values
//         //         existingUser.firstName = users.firstName;
//         //         existingUser.lastName = users.lastName;
//         //         existingUser.MiddleName = users.MiddleName;
//         //         existingUser.Age = users.Age;

//         //         // Save the updated user in the database
//         //         await userService.Update(existingUser);

//         //         // Redirect to the Index action to display the updated user list
//         //         return RedirectToAction("Index");
//         //     }

//         //     // If the model state is invalid, return the same view with the current model
//         //     return View(users);
//         // }

//         // public async Task<IActionResult> Delete(Guid Id)
//         // {
//         //     var data = await userService.GetByIdAsync(Id);
        

//         //     if(data == null)
//         //     {
//         //         return NotFound();
//         //     }
            
//         //     await userService.Delete(Id);
//         //     var response = new Dictionary<string, object>
//         //     {
//         //         { "data", data }, // Optionally include the deleted data
//         //         { "message", "Deleted Successfully" }
//         //     };

//         //     return Ok(response);
//         // }

//     }
// }

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using CrudAsp.Repository;
using CrudAsp.Models;
using CrudAsp.resource.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CrudAsp.Controllers.Users
{
    public class AccountController : Controller
    {
        private readonly SignInManager<CrudAsp.Models.Users> _signInManager;
        private readonly UserManager<CrudAsp.Models.Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        // Constructor with dependency injection
        public AccountController(SignInManager<CrudAsp.Models.Users> signInManager, UserManager<CrudAsp.Models.Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Display the login page
        public IActionResult Login()
        {
            return View("Login");
        }

        // Display the registration page
        public IActionResult Register()
        {
            return View("Register");
        }

        // Handle user logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Handle login post request
        [HttpPost]
        public async Task<IActionResult> PostLogin(LoginVM log)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? user = null;

                // Check if input is an email (simple email validation)
                if (log.UserName!.Contains("@"))
                {
                    // Try to find user by email
                    user = await _userManager.FindByEmailAsync(log.UserName);
                }
                else
                {
                    // Try to find user by username
                    user = await _userManager.FindByNameAsync(log.UserName);
                }

                // If the user is found, proceed with password sign-in
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, log.Password!, log.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movie");
                    }

                    ModelState.AddModelError("", "Invalid login attempt");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                }
            }

            // If we got this far, something failed, redisplay the form
            return View("Login", log);
        }


        // Handle registration post request
        [HttpPost]
        public async Task<IActionResult> PostRegister(RegisterVM model)
        {
            var role =  _roleManager.Roles
                        .FirstOrDefault(r => r.NormalizedName == "ADMIN");

            // return Json(model);
            // // return Ok(model);
            if (ModelState.IsValid)
            {
                var user = new CrudAsp.Models.Users
                {
                    FirstName           = model.FirstName,
                    MiddleName          = model.MiddleName,
                    LastName            = model.LastName,
                    UserName            = model.UserName,
                    Email               = model.Email,
                    Age                 = model.Age
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                // return Ok(result);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, role.Name);
                    return RedirectToAction("Index", "Movie");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                return Ok("Invalid");
            }

            return View("Register", model);
        }

    }
}
