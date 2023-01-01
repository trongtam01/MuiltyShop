using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Product
{
    [Table("ProductCategoryProduct")]
    public class ProductCategoryProductModel
    {
        public int ProductID { set; get; }

        public int CategoryID { set; get; }


        [ForeignKey("ProductID")]
        public ProductModel Product { set; get; }

        [ForeignKey("CategoryID")]
        public CategoryProductModel Category { set; get; }
    }
}
