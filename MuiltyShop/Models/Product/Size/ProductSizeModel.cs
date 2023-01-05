using MuiltyShop.Models.Product.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Product.Size
{
    [Table("ProductSize")]
    public class ProductSizeModel
    {
        public int ProductID { set; get; }
        public int SizeID { set; get; }

        [ForeignKey("ProductID")]
        public ProductModel Product { set; get; }

        [ForeignKey("SizeID")]
        public SizeModel Size { set; get; }
    }
}
