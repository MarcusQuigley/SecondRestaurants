using System;

namespace SecondRestaurants.Data
{
    public interface IRestaurantDataService
    {
        Restaurant GetById(int Id);
    }
}
