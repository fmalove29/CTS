namespace CrudAsp.resource.request;

public class MovieImageRequest : BaseRequest
{
    public string Name {get; set;}

    public int Size {get; set;}

    public string Type {get; set;}

    public string Path {get; set;}
    public Guid? MovieId { get; set; }
    public virtual MovieRequest? Movie { get; set; }
}