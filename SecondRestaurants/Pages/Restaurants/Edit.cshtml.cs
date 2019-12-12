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
        public Restaurant Restaurant { get; set; }
        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();// as IEnumerable<CuisineType>;
            Restaurant = service.GetById(restaurantId);
            if (Restaurant == null)
            {
                return RedirectToPage("NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                Restaurant = service.Update(Restaurant);
                return RedirectToPage("Detail");
            }
            catch (Exception ex)
            {
                logger.LogError(0, ex, ex.Message);
                return Page();
                 
            }
        }
    }
}
