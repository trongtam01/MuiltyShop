using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MuiltyShop.Models.Product.Category;

namespace MuiltyShop.Components
{
    [ViewComponent]
    public class CategoryProductNav : ViewComponent 
    {
        public class CategoryNavData 
        {
            public List<CategoryProductModel> Categories { get; set; }
            public int level { get; set; }
            public string categoryslug { get; set;}

        }

        public IViewComponentResult Invoke(CategoryNavData data)
        {
            return View(data);
        }

    }
}