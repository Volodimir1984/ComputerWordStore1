using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerWordStore.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace ComputerWordStore.Components
{
    // View component for views the list categories and brands.
    public class ListCategoriesViewComponent : ViewComponent
    {
        private readonly ComputersWorldContext _db;

        public ListCategoriesViewComponent(ComputersWorldContext db)
        {
            _db = db;
        }

        // Return list all categories and brands
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList<Category> result = await _db.Categories.Include(i => i.BrandCategories)
                                           .ThenInclude(i => i.Brand)
                                           .ToListAsync();
            
            return View(result);
        }
    }
}