using System.Text.Json.Serialization;
namespace CrudAsp.Models.app;

public class MovieImage : BaseEntity
{
    public string Name {get; set;}

    public int Size {get; set;}

    public string Type {get; set;}

    public string? Path { get; set;}

    public Guid? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }
}