using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcnavyInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcnavyInfoId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نوع ناوگان")]
        public int GlcnavyType { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("شماره پلاک - ایران")]
        public int GlcnavyPelakIran { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("شماره پلاک - 2 رقم")]
        public int GlcnavyPelak2 { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("شماره پلاک - حرف")]

        public string GlcnavyPelakChar { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("شماره پلاک - سه رقم")]

        public int GlcnavyPelak3 { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نوع مالکیت")]

        public int GlcnavyOwnerType { get; set; }

    }
}
