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
                    Id=0, Name="Marea", Location="Manhattan", Cusine=CusineType.Italian
                },
                  new Restaurant {
                    Id=1, Name="Kati", Location="Astoria", Cusine=CusineType.Thai
                },
                    new Restaurant {
                    Id=2, Name="Acupulco", Location="Brooklyn", Cusine=CusineType.Mexican
                }

            };

        }
        public Restaurant Add(Restaurant restaurant)
        {
           if (restaurant == null)
            {
                throw new ArgumentNullException("restaurant");
            }
            restaurant.Id = restaurantList.Count();
            restaurantList.Add(restaurant);
            return restaurant;
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

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return restaurantList;
        }

        public Restaurant GetById(int restaurantId)
        {
            return restaurantList.SingleOrDefault(r => r.Id == restaurantId);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            if (restaurant == null)
            {
                throw new ArgumentNullException("restaurant");
            }
            var rest = restaurantList.SingleOrDefault(r => r.Id == restaurant.Id);
            if (rest != null)
            {
                rest.Name = restaurant.Name;
                rest.Location = restaurant.Location;
                rest.Cusine = restaurant.Cusine;
            }

            return restaurant;
        }
    }
}
