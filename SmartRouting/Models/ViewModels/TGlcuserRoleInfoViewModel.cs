using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcuserRoleInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcuserRoleInfoId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد.")]
        [DisplayName("نام نقش")]
        public string GlcuserRoleName { get; set; } = null!;
    }
}
