using Microsoft.EntityFrameworkCore;
using SecondRestaurants.Core;
using System;
using System.Linq;
using Xunit;

namespace SecondRestaurants.Data.Test
{
    public class SqlLiteRestaurantServiceShould : IDisposable
    {
        readonly RestaurantDBContext context;
        public SqlLiteRestaurantServiceShould()
        {
            var options = new DbContextOptionsBuilder<RestaurantDBContext>()
            .UseInMemoryDatabase($"DbFortesting{Guid.NewGuid()}")
            .Options;

            context = new RestaurantDBContext(options);


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

        [Fact]
        public void AddNewRestaurant()
        {
            var service = new SqlLiteRestaurantService(context);
            service.Add(new Restaurant
            {
                Id = 3,
                Name = "Taboo",
                Location = "Queens",
                Cuisine = CuisineType.Mexican
            });

            context.SaveChanges();

            Assert.Equal(3, service.Count);

        }

        [Fact]
        public void Return_all_restaurants_if_Taboo_passed()
        {
            var service = new SqlLiteRestaurantService(context);
            var restaurants = service.GetRestaurantsByName("Taboo");
            Assert.Equal(1, restaurants.Count());
        }

        [Fact]
        public void Return_1_restaurant_if_null_passed()
        {
            var service = new SqlLiteRestaurantService(context);
            var restaurants = service.GetRestaurantsByName(null);
            Assert.Equal(2, restaurants.Count());
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
