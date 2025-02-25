namespace CrudAsp.Models.app;

public class MenuItem : BaseEntity
{
    
    public string  Name{get; set;}

    public string Path {get; set;}

    public string? ParentId {get; set;}


    public virtual ICollection<AccessMenu> AccessMenus { get; set; }
}
