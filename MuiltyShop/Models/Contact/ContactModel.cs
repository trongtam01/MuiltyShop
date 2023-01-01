using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace MuiltyShop.Models.Contact
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Phải nhập  {0}")]
        [Display(Name = "Họ Tên", Prompt = "UserName")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Phải nhập  {0}")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Phải là địa chỉ email")]
        [Display(Name = "Địa chỉ email", Prompt = "Email")]
        public string Email { get; set; }

        public DateTime DateSent { get; set; }

        [Display(Name = "Nội dung", Prompt = "Message")]
        public string Message { get; set; }

        [StringLength(50)]
        [Phone(ErrorMessage = "Phải là số diện thoại")]
        [Display(Name = "Số điện thoại", Prompt = "PhoneNumber")]
        public string Phone { get; set; }
    }
}
