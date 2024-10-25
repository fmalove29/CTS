
using System.ComponentModel.DataAnnotations;
namespace CrudAsp.resource.response;

public class BaseResponse
{
    public Guid Id {get; set;}

    public DateTime created_at {get; set;}
}