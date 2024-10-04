using System.ComponentModel.DataAnnotations;

namespace CrudAsp.resource.ViewModels;

    public class RegisterVM
    {
        [Required]
        public string? FirstName {get; set;}
        [Required]
        public string? LastName {get; set;}
        [Required]
        public string? MiddleName {get; set;}
        [Required]
        public string UserName {get; set;}
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email{get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password{get; set;}

        [Compare("Password", ErrorMessage ="Password does not match.")]
        public string? ConfirmPassword {get; set;}
    }
