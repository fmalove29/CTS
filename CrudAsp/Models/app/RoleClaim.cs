namespace CrudAsp.Models.app;

public class RoleClaim
{
    public int Id {get; set;}
    public Guid RoleId {get; set;}
    public string ClaimType {get; set;}
    public string ClaimValue {get; set;}
}
