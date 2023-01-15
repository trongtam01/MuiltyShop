using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MuiltyShop.Models.Photo
{
    public class UploadOneFile
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif")]
        [Display(Name = "Chọn file update")]
        public IFormFile FileUpload { get; set; } 
    }
}
