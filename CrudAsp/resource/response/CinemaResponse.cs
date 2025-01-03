namespace CrudAsp.resource.response;

public class CinemaResponse : BaseResponse
{
    public string CinemaName {get; set;}
    public string Location {get; set;}
    public virtual ICollection<HallResponse> Halls { get; set; }
}