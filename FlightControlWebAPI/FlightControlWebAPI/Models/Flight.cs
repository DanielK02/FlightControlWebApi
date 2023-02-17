using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControlWebAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public string FlightName { get; set; }
        [Required]
        public string SerialNumber { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public BrandType Brand { get; set; }
        public bool? IsDeparted { get; set; }
        //[NotMapped]
        //public bool? IsWaiting { get; set; }
    }
}
