using Microsoft.Extensions.Logging;
using SecondRestaurants.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondRestaurants.Data
{
    public class InMemoryRestaurantService : IRestaurantDataService
    {
        readonly IList<Restaurant> restaurantList;
        readonly ILogger logger;
        public InMemoryRestaurantService(ILogger<InMemoryRestaurantService> logger)
        {
            this.logger = logger;

            restaurantList = new List<Restaurant> {
                new Restaurant {
                    Id=1, Name="Marea", Location="Manhattan", Cuisine=CuisineType.Italian
                },
                  new Restaurant {
                    Id=2, Name="Kati", Location="Astoria", Cuisine=CuisineType.Thai
                },
                    new Restaurant {
                    Id=3, Name="Acupulco", Location="Brooklyn", Cuisine=CuisineType.Mexican
                }

            };

        }
        public Restaurant Add(Restaurant newRestaurant)
        {
           if (newRestaurant == null)
            {
                throw new ArgumentNullException("newRestaurant");
            }
            newRestaurant.Id = restaurantList.Max(r => r.Id) + 1;
            restaurantList.Add(newRestaurant);
            return newRestaurant;
        }

        public bool Delete(int restaurantId)
        {
            var restaurant = restaurantList.SingleOrDefault(r => r.Id == restaurantId);
            if (restaurant != null)
            {
                restaurantList.Remove(restaurant);
                return true;
            }
            return false;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return restaurantList.Where(r=> string.IsNullOrEmpty(name) || r.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) )
                                 .OrderBy(r=>r.Name);
        }

        public Restaurant GetById(int restaurantId)
        {
            return restaurantList.SingleOrDefault(r => r.Id == restaurantId);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            if (updatedRestaurant == null)
            {
                throw new ArgumentNullException("restaurant");
            }
            var restaurant = restaurantList.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }
    }
}
