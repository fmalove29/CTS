namespace CrudAsp.Models.app;

public class ActionRoute : BaseEntity
{
    public string RouteName { get; set; }
    public string RouteText { get; set; }
    public string RouteDescription { get; set; }
    public string HttpMethod { get; set; }
    public string PermissionRequired { get; set; }
    public string ControllerName { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid Syscreator { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
