using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MuiltyShop.Models;
using MuiltyShop.Models.Product.Category;


namespace MuiltyShop.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = _context.CategoryProducts
                             .Include(c => c.CategoryChildren)
                             .AsEnumerable()
                             .Where(c => c.ParentCategory == null)
                             .ToList();

            ViewBag.categories = categories;

            var product = _context.Products
                               .Include(p => p.Author)
                               .Include(p => p.Photos)
                               .Include(p => p.ProductCategoryProducts)
                               .ThenInclude(pc => pc.Category)
                               .FirstOrDefault();

            if (product == null)
            {
                return NotFound("Không thấy bài viết");
            }

            CategoryProductModel category = product.ProductCategoryProducts.FirstOrDefault()?.Category;
            ViewBag.category = category;
            return View();
        }
    }
}
