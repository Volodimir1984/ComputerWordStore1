using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComputerWordStore.Models;
using ComputerWordStore.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ComputerWordStore.Controllers
{
    // Class of controller to views index page, list products and details product.
    public class ProductController : Controller
    {
        // Database context.
        private readonly ComputersWorldContext _db;

        public ProductController(ComputersWorldContext db)
        {
            _db = db;
        }
        
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        
        // Controller to views list products.
        // If everything links do not correctly, redirect to error page.
        [Route("{category}/{brand}/{page:int?}")] 
        [Route("{category}/{page:int?}")]
        public async Task<IActionResult> ListProducts(string category, string brand, string sort, 
            decimal? min, decimal? max, int countProductPage = 12, int page = 1)
        {
            // Number of products on the page.
            ViewBag.CountProductPage = countProductPage;
            ViewBag.ValuesLink = Request.QueryString.Value;
            ViewData["category_name"] = _db.Categories.FirstOrDefault(i => i.Slug == category)?.Name ?? "";
            ViewData["brand_name"] = _db.Brands.FirstOrDefault(i => i.Slug == brand)?.Name ?? "";
            ViewData["category_slug"] = category;
            ViewBag.Brands = await GetListBrands(category);

            if (!_db.Categories.Any(i => i.Slug == category) || !_db.Brands.Any(i => i.Slug == brand)
                && brand != null)
            {
                return Redirect("~/error");
            }

            (IList<Product> result, string link) = await GetSelectProduct(category, brand, sort);

            ViewData["href"] = link;
            // Values for slider for filter of price.
            decimal priceMin = result.Min(i => i.Price);
            decimal priceMax = result.Max(i => i.Price);
            ViewData["priceMin"] = priceMin;
            ViewData["priceMax"] = priceMax;
            ViewData["startPriceMin"] = min ?? priceMin;
            ViewData["startPriceMax"] = max ?? priceMax;
            
             if (min != null || max != null)
             { 
                 result = GetFilterListProductsForPrice(result, min ?? priceMin, max ?? priceMax);
             }
            
            int countProducts = result.Count;
            ViewData["count"] = countProducts;
            int countPage = (int) Math.Ceiling(countProducts / (double) countProductPage);

            if (page > countPage)
            {
                page = countPage;
            } 
            
            List<Product> productsPage = !result.Any() ? new List<Product>(): result.Skip((page - 1) * countProductPage)
                .Take(countProductPage)
                .ToList();
            
            PageModel pageModel = new PageModel(page, countPage);
            PaginationModel pagination = new PaginationModel
            {
                Products = productsPage,
                PageModel = pageModel,
            };

            return View(pagination);
        }
        
        // Controller to views details product.
        // If everything links do not correctly, redirect to error page.
        [Route("/{category}/{brand}/{productSlag}")]
        public async Task<IActionResult> Product(string productSlag)
        {
            Product product = await _db.Products.Include(i => i.ProductImageses)
                .Include(i => i.Category)
                .Include(i => i.Brand)
                .SingleOrDefaultAsync(i => i.Slug == productSlag);

            if (product is null)
            {
                return Redirect("~/error/");
            }

            return View(product);
        }
        
        // Controller for Error page.
        [Route("error/")]
        public IActionResult ErrorPage()
        {
            string hello = "Hello World";
            return View();
        }
        
        // Return brands list
        private async Task<IList<Brand>> GetListBrands(string category)
        {
            return await _db.BrandCategories
                .Include(i => i.Brand)
                .ThenInclude(i => i.Products)
                .ThenInclude(i => i.Category)
                .Where(i => i.Category.Slug == category)
                .Select(i => i.Brand).ToListAsync();
        }
        
        // The function returns list of products on dependencies of selected parameters.
        private async Task<(IList<Product>, string)> GetSelectProduct(string category, string brand, string sort)
        {
            IQueryable<Product> result;
            string link;
            
            if (brand is null)
            { 
                result = _db.Products.Where(i => i.Category.Slug == category)
                    .Include(i => i.ProductImageses)
                    .Include(i => i.Brand);
                
                link = "/" + category + "/";
            }
            else
            {
                result = _db.Products.Where(i => i.Brand.Slug == brand && i.Category.Slug == category)
                    .Include(i => i.ProductImageses)
                    .Include(i => i.Brand);
                
                link = "/" + category + "/" + brand + "/";
            }

            if (sort == null)
            {
                return (await result.ToListAsync(), link);
            }

            return (await GetSortListProducts(result, sort), link);

        }

        // The function sorting products list
        private async Task<IList<Product>> GetSortListProducts(IQueryable<Product> listProduct, string sort)
        {
            IList<Product> result;
             
            switch (sort)
            {
                case "cheap":
                    result = await listProduct.OrderBy(i => i.Price).ToListAsync();
                    break;
                case "expensive":
                    result = await listProduct.OrderByDescending(i => i.Price).ToListAsync();
                    break;
                default:
                    result = await listProduct.ToListAsync();
                    break;
            }

            return result;
        }
        
        // The function filtering products for price
        private IList<Product> GetFilterListProductsForPrice(IList<Product> products, decimal min, decimal max)
        {
            return products.Where(i => i.Price >= min && i.Price <= max).ToList();
        }
        
    }
    
}