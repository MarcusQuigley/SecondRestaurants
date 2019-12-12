using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecondRestaurants.Core;
using SecondRestaurants.Data;

namespace MyApp.Namespace
{
    public class DetailModel : PageModel
    {
        readonly IRestaurantDataService service;

        public DetailModel(IRestaurantDataService service)
        {
            this.service = service;
        }

        public Restaurant Restaurant { get; set; }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = service.GetById(restaurantId);
            if (Restaurant==null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }
    }
}
