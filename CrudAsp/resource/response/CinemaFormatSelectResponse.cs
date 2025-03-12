using CrudAsp.Models.app.Enum;
namespace CrudAsp.resource.response;

public class CinemaFormatSelectResponse : BaseResponse
{
    public ScreenType ScreenType { get; set; }
    
    public string ScreenTypeName { get; set; }

    public decimal Price { get; set; }
}
