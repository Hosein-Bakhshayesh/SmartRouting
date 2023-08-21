using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartRouting.Models
{
    public class ExcelUploadModel
    {
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("فایل اکسل")]
        public IFormFile ExcelFile { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون شماره حواله")]
        public int Excel_DossierDetailNumber { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون نام مشتری")]
        public int Excel_DossierDetailCustomer { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون تلفن همراه مشتری")]
        public int Excel_DossierDetailCmobile { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون آدرس مشتری")]
        public int Excel_DossierDetailCaddress { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون طول")]
        public int Excel_DossierDetailLength { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون عرض")]
        public int Excel_DossierDetailWidth { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون کالا")]
        public int Excel_DossierDetailProduct { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون تعداد")]
        public int Excel_DossierDetailQuantity { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون ساعت")]
        public int Excel_DossierDetailHour { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون خدمت")]
        public int Excel_DossierDetailType { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("شماره ستون کد ملی راننده")]
        public int Excel_DossierDetailDriverId { get; set; }
        [Required(ErrorMessage = "فیلد {0} ضروری میباشد")]
        [DisplayName("تاریخ")]
        public string Exce_DossierDetailDateTime { get; set; } = null!;
    }
}
