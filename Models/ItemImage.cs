using System.ComponentModel.DataAnnotations;

namespace InventoryAPI.Models
{
    public class ItemImage
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string ImageFile { get; set; } = string.Empty;

        [StringLength(200)]
        public string Notes { get; set; } = string.Empty;

        public int ItemId { get; set; }

        public Item? Item { get; set; }


    }
}
