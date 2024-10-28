using System.ComponentModel.DataAnnotations;

namespace EFMBL.Entities
{
    public class FilteredOrderResult
    {
        [Key] 
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }


        public ushort Weight { get; set; }

        public string DistrictName { get; set; }
        
        public DateTime DeliveryTime { get; set; }


        public DateTime RequestTime { get; set; }
    }
}
