using Microsoft.EntityFrameworkCore;
using SecondRestaurants.Core;

namespace SecondRestaurants.Data
{
    public class RestaurantDBContext : DbContext
    {
        public RestaurantDBContext(DbContextOptions<RestaurantDBContext> options)
            : base(options)
        { }

        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
