using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class ItemLocation
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Location { get; set; } = string.Empty;
    }
}
