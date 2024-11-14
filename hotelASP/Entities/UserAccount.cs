using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace hotelASP.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]  
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
