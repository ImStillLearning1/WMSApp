using System.ComponentModel.DataAnnotations;

namespace WMSApp.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
    }
}