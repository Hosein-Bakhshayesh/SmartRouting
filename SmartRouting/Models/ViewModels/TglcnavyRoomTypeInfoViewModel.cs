using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TglcnavyRoomTypeInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcnavyRoomTypeId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [Display(Name = "نام اتاق")]
        public string GlcnavyRoomTypeName { get; set; } = null!;
    }
}
