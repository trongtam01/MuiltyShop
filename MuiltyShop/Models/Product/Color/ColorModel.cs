using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Product.Color
{
    [Table("Color")]
    public class ColorModel
    {
        [Key]
        public int Id { get; set; }
        // Tiều đề Category
        [Required(ErrorMessage = "Phải có tên màu sắc")]
        [Display(Name = "Tên màu sắc")]
        public string Title { get; set; }

        // Nội dung, thông tin chi tiết về Category
        [DataType(DataType.Text)]
        [Display(Name = "Nội dung màu sắc")]
        public string Description { get; set; }
    }
}
