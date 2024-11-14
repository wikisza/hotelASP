using System.ComponentModel.DataAnnotations;

namespace hotelASP.Models
{
    public class Room
    {
        [Key]
        public int Id_room { get; set; }
        public required string Description { get; set; }
    }
}
