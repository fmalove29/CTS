using Microsoft.AspNetCore.Identity;
namespace CrudAsp.Models.app;

public class AccessMenu
{
    public string RoleId {get; set;}
    public virtual IdentityRole Role { get; set; }

    public Guid MenuItemId {get; set;}
    public virtual MenuItem MenuItem {get; set;}
}
