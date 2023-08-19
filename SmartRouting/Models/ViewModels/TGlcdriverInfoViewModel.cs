using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcdriverInfoViewModel
    {
        [Key]
        [DisplayName("آیدی راننده")]
        public int GlcdriverInfoId { get; set; }
        [Required(ErrorMessage ="فیلد {0} ضروری میباشد.")]
        [MinLength(10,ErrorMessage ="باید 10 رقم باشد.")]
        [MaxLength(10,ErrorMessage ="باید 10 رقم باشد.")]
        [DisplayName("کد ملی")]
        public string GlcdriverNationalCode { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نام")]
        public string GlcdriverName { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نام خانوادگی")]
        public string GlcdriverLastName { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("شماره موبایل")]
        public string GlcdriverMobile { get; set; } = null!;
        [DisplayName("تصویر راننده")]
        public string? GlcdriverPhotoPath { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("ناوگان تحویلی")]
        public int GlcdriverNavyInfoId { get; set; }
    }
}
