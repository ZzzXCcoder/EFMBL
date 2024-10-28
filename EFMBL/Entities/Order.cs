using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFMBL.Entities
{
    public class Order
    {
        [Required]
        
        public Guid Id { get; set; } 

        
        public ushort Weight { get; set; }

       
        [ForeignKey("District")]
        public Guid DistrictId { get; set; }

        public District? District { get; set; }

        
        public DateTime DeliveryTime { get; set; }
    }
}
