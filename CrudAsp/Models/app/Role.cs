using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CrudAsp.Models.app;

public class Role : IdentityRole<string>
{
    public virtual ICollection<AccessMenu> AccessMenus { get; set; } 
}
