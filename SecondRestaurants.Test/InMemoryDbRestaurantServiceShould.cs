using Microsoft.EntityFrameworkCore;
using SecondRestaurants.Core;
using System;
using System.Linq;
using Xunit;

namespace SecondRestaurants.Data.Test
{
    public class InMemoryDbRestaurantServiceShould
    {
        [Fact]
        public void AddNewRestaurant()
        {
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}")
                .Options;
            using (var context = new RestaurantDBContext(options))
            {
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
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}")
                .Options;

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
                var service = new SqlLiteRestaurantService(context);
                var restaurants = service.GetRestaurantsByName("Taboo");
               Assert.Contains(restaurants, restaurant => restaurant.Name.Equals("Taboo"));
            }
        }

        [Fact]
        public void Return_all_restaurants_if_null_passed()
        {
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
                .UseInMemoryDatabase($"InMemoryDb{Guid.NewGuid()}")
                .Options;

            using (var context = new RestaurantDBContext(options))
            {
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
