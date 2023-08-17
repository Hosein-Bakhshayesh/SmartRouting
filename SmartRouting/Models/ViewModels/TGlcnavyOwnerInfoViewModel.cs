using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcnavyOwnerInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcnavyOwnerId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نوع مالکیت")]

        public string GlcnavyOwnerName { get; set; } = null!;
    }
}
