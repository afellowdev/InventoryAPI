using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<InventoryAPI.Models.Item> Items { get; set; }

        public DbSet<InventoryAPI.Models.ItemType> ItemTypes { get; set; }

        public DbSet<InventoryAPI.Models.ItemLocation> ItemLocations { get; set; }

        public DbSet<InventoryAPI.Models.ItemImage> ItemImage { get; set; }
    }
}
