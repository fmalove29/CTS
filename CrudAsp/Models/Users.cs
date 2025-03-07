using Microsoft.AspNetCore.Identity;

namespace CrudAsp.Models
{
    public class Users : IdentityUser
    {
        public  string FirstName {get; set;}
        public  string? MiddleName {get; set;}
        public  string LastName {get; set;}
        public int Age {get; set;}
    }
} 