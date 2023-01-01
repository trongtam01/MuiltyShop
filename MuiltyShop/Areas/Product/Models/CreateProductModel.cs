using Bogus.DataSets;
using MuiltyShop.Models.Product;
using System.ComponentModel.DataAnnotations;

namespace MuiltyShop.Areas.Product.Models
{
    public class CreateProductModel : ProductModel
    {
        [Display(Name = "Chuyên mục")]
        public int[] CategoryIDs { get; set; }
    }
}
