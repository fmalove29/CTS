
using CrudAsp.resource.response;
namespace CrudAsp.resource.response;

public class UserResponseDTO 
{
        public string Id {get; set;}
        public  string FirstName {get; set;}
        public  string? MiddleName {get; set;}
        public  string LastName {get; set;}
        public  string Email {get; set;}
        public int Age {get; set;}
        public List<RoleResponseDTO> Roles { get; set; }

}
