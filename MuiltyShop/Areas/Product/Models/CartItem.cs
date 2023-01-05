using MuiltyShop.Models.Product;

namespace MuiltyShop.Areas.Product.Models
{
    public class CartItem
    {
        public int quantity { set; get; }
        public ProductModel product { set; get; }
    }
}
