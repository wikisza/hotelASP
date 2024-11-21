using System.ComponentModel.DataAnnotations;

namespace hotelASP.Models
{
	public class Reservation
	{
		[Key]
		public int Id_reservation { get; set; }
		[Required]
		public DateTime Date_from { get; set; }
		[Required]
		public DateTime Date_to { get; set; }
		[Required]
		public string First_name { get; set; }
		[Required]
		public string Last_name { get; set; }
		[Required]
		public int Id_room { get; set; }
	}
}
