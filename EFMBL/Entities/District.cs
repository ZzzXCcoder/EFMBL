using System.ComponentModel.DataAnnotations;

namespace EFMBL.Entities
{
    public class District
    {
        
        [Key]
        public Guid Id { get; set; }

        public string? DistrictName { get; set; }
    }
}
