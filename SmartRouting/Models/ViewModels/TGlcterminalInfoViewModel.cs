using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcterminalInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcterminalInfoId { get; set; }
        [Required(ErrorMessage ="فیلد {0} الزامی میباشد.")]
        [DisplayName("کد پایانه")]
        public int GlcterminalId { get; set; }
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("نام پایانه")]
        public string GlcterminalName { get; set; } = null!;
    }
}
