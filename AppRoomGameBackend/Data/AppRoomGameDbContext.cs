using Microsoft.EntityFrameworkCore;
using AppRoomGameBackend.Models;

namespace AppRoomGameBackend.Data
{
    public class AppRoomGameDbContext : DbContext
    {
        public AppRoomGameDbContext(DbContextOptions<AppRoomGameDbContext> options) : base(options)
        {
        }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
