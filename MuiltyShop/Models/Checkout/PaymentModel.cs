using Bogus.DataSets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Checkout
{
    [Table("Payment")]
    public class PaymentModel
    {
        [Key]
        public int Id { get; set; }
        // Tiều đề Category
        [Required(ErrorMessage = "Phải có tên thanh toán")]
        [Display(Name = "Tên thanh toán")]
        public string Title { get; set; }

        // Nội dung, thông tin chi tiết về Category
        [DataType(DataType.Text)]
        [Display(Name = "Nội dung thanh toán")]
        public string Description { get; set; }
    }
}
