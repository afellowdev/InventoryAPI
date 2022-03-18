using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Notes { get; set; } = string.Empty;

        public DateTime UpdatedDate { get; set; }

        public int ItemTypeId { get; set; }

        public ItemType? ItemType { get; set; }

        public int ItemLocationId { get; set; }

        public ItemLocation? ItemLocation { get; set; }
    }
}
