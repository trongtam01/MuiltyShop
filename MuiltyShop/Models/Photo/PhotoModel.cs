using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using MuiltyShop.Models.Product;

namespace MuiltyShop.Models
{
    [Table("Photo")]
    public class PhotoModel
    {
        [Key]
        public int Id { set; get; }
        public string FileName { set; get; }
        public int ProductID { set; get; }
        [ForeignKey("ProductID")]
        public ProductModel Product { set; get; }
        public int CategoryID { set; get; }
        public CategoryProductModel CategoryProduct { set; get; }
    }
}
