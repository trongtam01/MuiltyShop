using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Product.Size
{
    [Table("Size")]
    public class SizeModel
    {
        [Key]
        public int Id { get; set; }
        // Tiều đề Category
        [Required(ErrorMessage = "Phải có tên kích cỡ")]
        [Display(Name = "Tên kích cỡ")]
        public string Title { get; set; }

        // Nội dung, thông tin chi tiết về Category
        [DataType(DataType.Text)]
        [Display(Name = "Nội dung kích cỡ")]
        public string Description { get; set; }
    }
}
