using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcusersInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcusersInfoId { get; set; }
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("کدملی")]
        [MaxLength(10,ErrorMessage = "فیلد {0} باید 10 کاراکتر باشد.")]
        [MinLength(10, ErrorMessage = "فیلد {0} باید 10 کاراکتر باشد.")]
        public string GlcusersNationalCode { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("نام")]
        public string GlcusersName { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("نام خانوادگی")]
        public string GlcusersLastName { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("شماره همراه")]
        public string GlcusersMobile { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} الزامی میباشد.")]
        [DisplayName("نقش")]
        public int GlcusersRoleId { get; set; }
    }
}
