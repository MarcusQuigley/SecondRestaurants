using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SecondRestaurants.Core;
using SecondRestaurants.Data;

namespace MyApp.Namespace
{
    public class EditModel : PageModel
    {
        readonly IRestaurantDataService service;
        readonly ILogger<EditModel> logger;
        readonly IHtmlHelper htmlHelper;

        public EditModel(IRestaurantDataService service,
                         IHtmlHelper htmlHelper,
                         ILogger<EditModel> logger)
        {
            this.logger = logger;
            this.service = service;
            this.htmlHelper = htmlHelper;
        }

        public IEnumerable<SelectListItem> Cuisines { get; set; }
        
        [BindProperty]
        public string EditMessage { get; set; }

        [BindProperty]
        public Restaurant Restaurant { get; set; }
       
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>(); 
            if (restaurantId.HasValue)
            {
                Restaurant = service.GetById(restaurantId.Value);
                EditMessage = $"Editing {Restaurant.Name}";
            }
            else
            {
                Restaurant = new Restaurant();
                EditMessage = "Add new restaurant";
            }
            if (Restaurant == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            try
            {
                if (Restaurant.Id == 0)
                {
                    Restaurant = service.Add(Restaurant);
                }
                else
                {
                    Restaurant = service.Update(Restaurant);
                }
                service.Commit();
                return RedirectToPage("Detail",new { restaurantId=Restaurant.Id});
            }
            catch (Exception ex)
            {
                logger.LogError(0, ex, ex.Message);
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
        }
    }
}
