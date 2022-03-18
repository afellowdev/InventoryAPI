using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class ItemType
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Type { get; set; } = string.Empty;
    }
}
