using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcnavyTypeInfoViewModel
    {
        [Key]
        [Display(Name ="آیدی نوع ناوگان")]
        public int GlcnavyTypeInfoId { get; set; }
        [Required(ErrorMessage ="فیلد {0} ضروری میباشد.")]
        [Display(Name = "نام ناوگان")]
        public string GlcnavyTypeName { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "طول ناوگان")]
        public double GlcnavyTypeLength { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "عرض ناوگان")]
        public double GlcnavyTypeWidth { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "عرض ناوگان")]
        public double GlcnavyTypeHeight { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "نوع سوخت")]
        public int GlcnavyTypeFuel { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "مدل اتاق")]
        public int GlcnavyTypeModel { get; set; }
    }
}
