using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models;
using SmartRouting.Models.Context;
using SmartRouting.Models.ExcelModels;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SmartRouting.Pages.Admin.DossierDetailInfo
{
    public class AdminGlcDossierDetailInfoModel : PageModel
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcdossierDetailInfoServices _glcdossierDetailInfoServices { get; set; }
        public TGlcdriverInfoServices _glcdriverInfoServices { get; set; }
        [BindProperty]
        public List<TGlcdossierDetailInfoViewModel>? glcdossierDetailInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcdriverInfoViewModel>? glcdriverInfoViewModels { get; set; }
        [BindProperty]
        public TGlcdossierDetailInfoViewModel AddDsossierDetail { get; set; }
        [BindProperty]
        public ExcelUploadModel excelUploadModel { get; set; }
        public List<string> ErrorMessage;
        public string SuccessMessage = "";
        public bool IsEdit;
        public int EditId;

        public AdminGlcDossierDetailInfoModel(IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
            _glcdriverInfoServices = new TGlcdriverInfoServices(db);
            _glcdossierDetailInfoServices = new TGlcdossierDetailInfoServices(db);
            AddDsossierDetail = new TGlcdossierDetailInfoViewModel();
            excelUploadModel = new ExcelUploadModel();
            _hostingEnvironment = hostingEnvironment;
            ErrorMessage = new List<string>();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditDossierDetail(bool isEdit, int id)
        {
            TGlcdossierDetailInfo glcdossierDetailInfo = new TGlcdossierDetailInfo();
            glcdossierDetailInfo = _mapper.Map<TGlcdossierDetailInfoViewModel, TGlcdossierDetailInfo>(AddDsossierDetail);
            bool res;
            if (isEdit)
            {
                glcdossierDetailInfo.DossierDetailDriverId = id;
                res = _glcdossierDetailInfoServices.Update(glcdossierDetailInfo);
            }
            else
            {
                bool isExist = _glcdossierDetailInfoServices.GetAll().Any(t => t.DossierDetailNumber == glcdossierDetailInfo.DossierDetailNumber);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage.Add("یک حواله با این شماره قبلا در سامانه ثبت شده است.");
                    return Page();
                }
                res = _glcdossierDetailInfoServices.Add(glcdossierDetailInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage.Add("در عملیات ذخیره سازی مشکلی پیش آمد.");
                return Page();
            }
            _glcdossierDetailInfoServices.Save();
            return RedirectToPage("AdminGlcDossierDetailInfo");
        }

        public ActionResult OnPostDeleteDossierDetail(int id)
        {
            try
            {
                if (id > 0)
                {
                    _glcdossierDetailInfoServices.Delete(id);
                    _glcdossierDetailInfoServices.Save();
                    return RedirectToPage("AdminGlcDossierDetailInfo");
                }
            }
            catch
            {
                LoadData();
                ErrorMessage.Add("این حواله در جای دیگر استفاده شده است.");
                return Page();
            }
            LoadData();
            ErrorMessage.Add("در عملیات حذف مشکلی پیش آمد.");
            return Page();
        }

        public ActionResult OnPostDeleteAllRows()
        {
            try
            {
                foreach (var item in _glcdossierDetailInfoServices.GetAll())
                {
                    _glcdossierDetailInfoServices.Delete(item.GlcdossierDetailInfoId);
                }
                _glcdossierDetailInfoServices.Save();
                SuccessMessage = "با موفقیت انجام شد.";
                LoadData();
                return Page();
            }
            catch
            {
                LoadData();
                ErrorMessage.Add("این حواله در جای دیگر استفاده شده است.");
                return Page();
            }
        }

        public ActionResult OnPostEditDossierDetail(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcdossierDetailInfo glcdossierDetailInfo = _glcdossierDetailInfoServices.GetEntity(id);
                AddDsossierDetail = _mapper.Map<TGlcdossierDetailInfo, TGlcdossierDetailInfoViewModel>(glcdossierDetailInfo);
                EditId = id;
                LoadData();
                return Page();

            }
            LoadData();
            ErrorMessage.Add("در عملیات ویرایش مشکلی پیش آمد.");
            return Page();
        }

        public ActionResult OnPostExcelUploadDossierDetail()
        {
            try
            {
                if (excelUploadModel.ExcelFile != null)
                {
                    int count = 0;
                    var extension = Path.GetExtension(excelUploadModel.ExcelFile.FileName);
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "ExcelFiles", Guid.NewGuid().ToString().Replace("-", "") + extension);
                    excelUploadModel.ExcelFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    Excel excel = new Excel(filePath, 1);
                    for (int i = 2; i <= excel.Range.Rows.Count; i++)
                    {
                        TGlcdossierDetailInfo ExcelDossierDetailInfo = new TGlcdossierDetailInfo();
                        bool DriverNotFound = false;
                        for (int j = excel.Range.Columns.Count; j > 1; j--)
                        {
                            switch (j)
                            {
                                case var expression when expression == excelUploadModel.Excel_DossierDetailNumber:
                                    ExcelDossierDetailInfo.DossierDetailNumber = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailCustomer:
                                    ExcelDossierDetailInfo.DossierDetailCustomer = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailCmobile:
                                    ExcelDossierDetailInfo.DossierDetailCmobile = excel.ReadIntCell(i, j).ToString();
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailCaddress:
                                    ExcelDossierDetailInfo.DossierDetailCaddress = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailLength:
                                    ExcelDossierDetailInfo.DossierDetailLength = Convert.ToDouble(excel.ReadIntCell(i, j));
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailWidth:
                                    ExcelDossierDetailInfo.DossierDetailWidth = Convert.ToDouble(excel.ReadIntCell(i, j));
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailProduct:
                                    ExcelDossierDetailInfo.DossierDetailProduct = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailQuantity:
                                    ExcelDossierDetailInfo.DossierDetailQuantity = Convert.ToInt32(excel.ReadIntCell(i, j).ToString());
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailHour:
                                    ExcelDossierDetailInfo.DossierDetailHour = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailType:
                                    ExcelDossierDetailInfo.DossierDetailType = excel.ReadCell(i, j);
                                    break;
                                case var expression when expression == excelUploadModel.Excel_DossierDetailDriverId:
                                    {
                                        var NCTemp = excel.ReadIntCell(i, j);
                                        bool isDriverExist = _glcdriverInfoServices.GetAll().Any(t => t.GlcdriverNationalCode == NCTemp.ToString());
                                        if (!isDriverExist)
                                        {
                                            LoadData();
                                            ErrorMessage.Add($"راننده با کد ملی {NCTemp} در سامانه ثبت نشده است. لطفا ابتدا مشخصات تمامی رانندگان را در قسمت رانندگان ثبت کنید.");
                                            DriverNotFound = true;
                                            break;
                                        }
                                        ExcelDossierDetailInfo.DossierDetailDriverId = _glcdriverInfoServices.GetAll().Find(t => t.GlcdriverNationalCode == NCTemp.ToString()).GlcdriverInfoId;
                                        break;
                                    }
                            }
                        }
                        if (DriverNotFound)
                        {
                            continue;
                        }
                        ExcelDossierDetailInfo.DossierDetailDateTime = excelUploadModel.Exce_DossierDetailDateTime;
                        var res = _glcdossierDetailInfoServices.Add(ExcelDossierDetailInfo);
                        if (!res)
                        {
                            excel.Close();
                            LoadData();
                            ErrorMessage.Add("در عملیات آپلود مشکلی پیش آمد.");
                            return Page();
                        }
                        count++;
                        _glcdossierDetailInfoServices.Save();
                    }
                    excel.Close();
                    SuccessMessage = $"تعداد {count} سطر اضافه شد.";
                    LoadData();
                    return Page();
                }
                LoadData();
                ErrorMessage.Add("در عملیات آپلود مشکلی پیش آمد.");
                return Page();
            }
            catch
            {
                LoadData();
                ErrorMessage.Add("مشکلی در آپلود فایل پیش آمده. لطفا فایل اکسل را بررسی و دوباره آپلود کنید.");
                return Page();
            }
        }

        public ActionResult OnPostShowOnMap()
        {
            return Redirect("/Map");
        }

        private void LoadData()
        {
            glcdossierDetailInfoViewModels = _mapper.Map<List<TGlcdossierDetailInfo>, List<TGlcdossierDetailInfoViewModel>>(_glcdossierDetailInfoServices.GetAll());
            glcdriverInfoViewModels = _mapper.Map<List<TGlcdriverInfo>, List<TGlcdriverInfoViewModel>>(_glcdriverInfoServices.GetAll());
        }
    }
}
