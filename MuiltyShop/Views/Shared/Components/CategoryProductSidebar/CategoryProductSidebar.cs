
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MuiltyShop.Models.Product;

namespace MuiltyShop.Components
{
    [ViewComponent]
    public class CategoryProductSidebar : ViewComponent {

        public class CategorySidebarData 
        {
            public List<CategoryProductModel> Categories { get; set; }
            public int level { get; set; }

            public string categoryslug { get; set;}

        }

        public IViewComponentResult Invoke(CategorySidebarData data)
        {
            return View(data);
        }

    }
}