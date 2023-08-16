using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TglcfuelTypeInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcfuelTypeId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "نام سوخت")]
        public string GlcfurlTypeName { get; set; } = null!;
    }
}
