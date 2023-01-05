using MuiltyShop.Models.Product.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Product.Color
{
    [Table("ProductColor")]
    public class ProductColorModel
    {
        public int ProductID { set; get; }
        public int ColorID { set; get; }

        [ForeignKey("ProductID")]
        public ProductModel Product { set; get; }

        [ForeignKey("SizeID")]
        public ColorModel Size { set; get; }
    }
}
