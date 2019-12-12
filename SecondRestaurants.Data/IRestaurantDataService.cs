using SecondRestaurants.Core;
using System;
using System.Collections.Generic;

namespace SecondRestaurants.Data
{
    public interface IRestaurantDataService
    {
        Restaurant GetById(int restaurantId);

        IEnumerable<Restaurant> GetRestaurants();

        Restaurant Add(Restaurant restaurant);

        Restaurant Update(Restaurant restaurant);

        bool Delete(int restaurantId);
    }
}
