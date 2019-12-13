using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SecondRestaurants.Core;
using SecondRestaurants.Data;

namespace SecondRestaurants
{
    public class DeleteModel : PageModel
    {

        readonly IRestaurantDataService service;
        readonly ILogger<DeleteModel> logger;
        public DeleteModel(IRestaurantDataService service,
            ILogger<DeleteModel> logger)
        {
            this.logger = logger;
            this.service = service;
        }
       
        public Restaurant Restaurant { get; set; }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = service.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int restaurantId)//Restaurant restaurant)
        {
            Restaurant = service.Delete(restaurantId);
            service.Commit();
            if (Restaurant == null)
                return RedirectToPage("./NotFound");
            return RedirectToPage("./List");
        }
    }
}