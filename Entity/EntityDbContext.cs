using HotelBookingSystem.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.Entity
{
    public class EntityDbContext : DbContext
    {
        public EntityDbContext(DbContextOptions option) : base(option)
        {
            
        }

        public DbSet<HotelTbl> HotelTbls { get; set; }
        public DbSet<BookingTbl> BookingTbls { get; set; }
    }
}
