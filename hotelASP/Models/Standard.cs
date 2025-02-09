using System.ComponentModel.DataAnnotations;

namespace hotelASP.Models
{
    public class Standard
    {
        [Key]
        public int IdStandard { get; set; }
        public string StandardName { get; set; }
        public float StandardValue { get; set; }
    }
}
