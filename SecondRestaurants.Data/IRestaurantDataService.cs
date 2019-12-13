using SecondRestaurants.Core;
using System;
using System.Collections.Generic;

namespace SecondRestaurants.Data
{
    public interface IRestaurantDataService
    {
        Restaurant GetById(int restaurantId);

        IEnumerable<Restaurant> GetRestaurantsByName(string name);

        Restaurant Add(Restaurant newRestaurant);

        Restaurant Update(Restaurant updatedRestaurant);

        Restaurant Delete(int restaurantId);

        int Commit();
        int Count { get; }
    }
}
