using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SecondRestaurants.Core;
using System;
using System.Linq;
using Xunit;

namespace SecondRestaurants.Data.Test
{
    public class SqliteDbRestaurantServiceShould
    {
        [Fact]
        public void AddNewRestaurant()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
            .UseSqlite(connection)
            .Options;
 
            using (var context = new RestaurantDBContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                context.Restaurants.Add(new Restaurant
                {
                    Id = 1,
                    Name = "Kapo",
                    Location = "Manhattan",
                    Cuisine = CuisineType.Italian
                });
                context.Restaurants.Add(new Restaurant
                {
                    Id = 2,
                    Name = "Laut",
                    Location = "Manhattan",
                    Cuisine = CuisineType.Thai
                });
                context.SaveChanges();
            }
            using (var context = new RestaurantDBContext(options))
            {
                context.Restaurants.Add(new Restaurant
                {
                    Id = 3,
                    Name = "Taboo",
                    Location = "Queens",
                    Cuisine = CuisineType.Mexican
                });
                context.SaveChanges();
            }
            using (var context = new RestaurantDBContext(options))
            {
                Assert.Equal(3, context.Restaurants.Count());
            }
        }

        [Fact]
        public void Return_all_restaurants_if_Taboo_passed()
        {

            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new RestaurantDBContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Restaurants.Add(new Restaurant
                {
                    Id = 3,
                    Name = "Taboo",
                    Location = "Queens",
                    Cuisine = CuisineType.Mexican
                });
                context.SaveChanges();
            }
            using (var context = new RestaurantDBContext(options))
            {
                var service = new SqlLiteRestaurantService(context);
                var restaurants = service.GetRestaurantsByName("Taboo");
               Assert.Contains(restaurants, restaurant => restaurant.Name.Equals("Taboo"));
            }
        }

        [Fact]
        public void Return_all_restaurants_if_null_passed()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new RestaurantDBContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
                context.Restaurants.Add(new Restaurant
                {
                    Id = 3,
                    Name = "Taboo",
                    Location = "Queens",
                    Cuisine = CuisineType.Mexican
                });
                context.Restaurants.Add(new Restaurant
                {
                    Id = 2,
                    Name = "Laut",
                    Location = "Manhattan",
                    Cuisine = CuisineType.Thai
                });
                context.SaveChanges();
            }
            using (var context = new RestaurantDBContext(options))
            {
                var service = new SqlLiteRestaurantService(context);
                var restaurants = service.GetRestaurantsByName(null);
                Assert.Equal(2, restaurants.Count());
            }
        }
    }
}
