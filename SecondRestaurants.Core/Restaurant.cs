using System;
using System.ComponentModel.DataAnnotations;

namespace SecondRestaurants.Core
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required,StringLength(60)]
        public string Name { get; set; }

        [Required, StringLength(150)]
        public string Location { get; set; }

        public CuisineType Cuisine { get; set; }
    }
}
