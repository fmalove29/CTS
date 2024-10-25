using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAsp.Models.app
{
    public class Cinema : BaseEntity
    {
        [Required]
        public string CinemaName { get; set; }

        [Required]
        public string Location { get; set; }

        public virtual ICollection<Hall> Halls { get; set; }
    }
}
