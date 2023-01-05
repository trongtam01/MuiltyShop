using MuiltyShop.Models.Product;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuiltyShop.Models.Checkout
{
    [Table("Checkout")]
    public class CheckoutModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Ngày tạo đơn hàng")]
        public DateTime DateCreated { set; get; }

        [Display(Name = "Ngày cập nhật đơn hàng")]
        public DateTime DateUpdated { set; get; }

        // [Required]
        [Display(Name = "Người đăng")]
        public string AuthorId { set; get; }

        [ForeignKey("AuthorId")]
        [Display(Name = "Người đăng")]
        public AppUser Author { set; get; }
        [Display(Name = "Thanh toán")]
        public int PaymentID { set; get; }

        [ForeignKey("PaymentID")]
        public PaymentModel Payment { set; get; }
    }
}
