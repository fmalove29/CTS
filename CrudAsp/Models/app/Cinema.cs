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

        public virtual ICollection<Hall>? Halls { get; set; } = new List<Hall>();
        public virtual ICollection<CinemaShow> CinemaShows { get; set; } = new List<CinemaShow>();
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } = new List<CinemaMovie>();
    }
}
