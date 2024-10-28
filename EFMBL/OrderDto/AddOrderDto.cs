using System.ComponentModel.DataAnnotations;

namespace EFMBL.OrderDto
{
    public class AddOrderDto
    {
        [Required]
        public string DistrictName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Вес должен быть положительным числом")]
        public ushort Weight { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryTime { get; set; }
    }

}
