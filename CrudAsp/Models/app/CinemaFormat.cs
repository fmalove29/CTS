using CrudAsp.Models.app.Enum;

namespace CrudAsp.Models.app;

public class CinemaFormat : BaseEntity
{
    public ScreenType ScreenType {get; set;}
    public decimal Price { get; set; }
}
