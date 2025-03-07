using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app
{
    public class Cinema : BaseEntity
    {
        [Required(ErrorMessage = "Cinema Name is Required")]
        public string CinemaName { get; set; }

        [Required(ErrorMessage = "Locatione is required.")]
        public string Location { get; set; }

        public virtual ICollection<Hall>? Halls { get; set; }
    }
}
