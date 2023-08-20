using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models.ViewModels
{
    public class TGlcdossierDetailInfoViewModel
    {
        [Key]
        [DisplayName("آیدی")]
        public int GlcdossierDetailInfoId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره حواله")]
        public string DossierDetailNumber { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("نام مشتری")]
        public string DossierDetailCustomer { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("تلفن همراه مشتری")]
        public string DossierDetailCmobile { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("آدرس مشتری")]
        public string DossierDetailCaddress { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("طول")]
        public double DossierDetailLength { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("عرض")]
        public double DossierDetailWidth { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("کالا")]
        public string DossierDetailProduct { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("تعداد")]
        public int DossierDetailQuantity { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("ساعت")]
        public string DossierDetailHour { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("خدمت")]
        public string DossierDetailType { get; set; } = null!;
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("کد ملی راننده")]
        public int DossierDetailDriverId { get; set; }
    }
}
