using Microsoft.EntityFrameworkCore;
using SecondRestaurants.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecondRestaurants.Data
{
    public class SqlLiteRestaurantService : IRestaurantDataService
    {
        readonly RestaurantDBContext dbContext;

        public SqlLiteRestaurantService(RestaurantDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Count => dbContext.Restaurants.Count();
        public Restaurant Add(Restaurant newRestaurant)
        {
            dbContext.Add(newRestaurant);
            return newRestaurant;
        }

        public Restaurant Delete(int restaurantId)
        {
            var restaurant = GetById(restaurantId);
            if (restaurant != null)
            {
                dbContext.Remove(restaurant);
            }
            return restaurant;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Restaurant GetById(int restaurantId)
        {
            return dbContext.Restaurants.Find(restaurantId);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return dbContext.Restaurants.Where(r => string.IsNullOrEmpty(name) 
                                            || r.Name.StartsWith(name))
                                        .OrderBy(r=>r.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = dbContext.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
