using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcSmsViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcsmsId { get; set; }
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("نام راننده")]
        public string GlcsmsMobileNumber { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("متن پیام")]
        public string GlcsmsDiscription { get; set; } = null!;
    }
}
