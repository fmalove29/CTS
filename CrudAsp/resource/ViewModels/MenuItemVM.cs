using CrudAsp.resource.response;

namespace CrudAsp.resource.ViewModels;

public class MenuItemVM
{
    public string Name {get; set;}

    public string Path {get; set;}

    public string? ParentId {get; set;}
}
