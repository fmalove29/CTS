using CrudAsp.Models.app.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAsp.resource.response;

public class CinemaFormatDTO : BaseResponse
{
        [Required(ErrorMessage = "ScreenType is required.")]
        public ScreenType ScreenType { get; set; }
        public string ScreenTypeName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

}
