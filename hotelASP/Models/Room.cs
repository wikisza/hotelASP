using System.ComponentModel.DataAnnotations;

namespace hotelASP.Models
{
    public class Room
    {
        [Key]
        public int IdRoom { get; set; }
        public required float Price { get; set; }
        public required string Description { get; set; }
        public int IsTaken { get; set; }
    }
}
