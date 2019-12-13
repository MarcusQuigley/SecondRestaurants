using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecondRestaurants.Core;
using SecondRestaurants.Data;

namespace SecondRestaurants
{
    public class ListModel : PageModel
    {
        readonly IRestaurantDataService service;

        public ListModel(IRestaurantDataService service)
        {
            this.service = service;
        }
        [BindProperty]
        public String SearchTerm { get; set; }

        public IEnumerable<Restaurant> Restaurants { get; set; }

        public IActionResult OnGet()
        {
            Restaurants = service.GetRestaurantsByName(SearchTerm);

            return Page();
        }

        public void OnPost()
        {
            Restaurants = service.GetRestaurantsByName(SearchTerm);
     
        }
    }
}