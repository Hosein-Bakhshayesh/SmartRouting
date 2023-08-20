using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcdriverShiftInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcdriverShiftInfoId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("روز")]
        public string? GlcdriverShiftDay { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("راننده")]
        public int GlcdriverShiftDriverId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("ساعت شروع")]
        [DataType(DataType.Time)]
        public string? GlcdriverShiftBeginHour { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("ساعت پایان")]
        [DataType(DataType.Time)]
        public string? GlcdriverShiftEndHour { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("پایانه")]
        public int GlcdriverShiftTerminalId { get; set; }
    }
}
