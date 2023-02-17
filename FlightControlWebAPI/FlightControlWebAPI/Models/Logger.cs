using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightControlWebAPI.Models
{
    public class Logger
    {
        public int Id { get; set; }
        [Required]
        public virtual Terminal Terminal { get; set; }
        [Required]
        public virtual Flight Flight { get; set; }
        [Required]
        public DateTime? Inbound { get; set; }
        public DateTime? Outbound { get; set; }
    }
}
