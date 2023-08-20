using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.DossierDetailInfo
{
    public class AdminGlcDossierDetailInfoModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcdossierDetailInfoServices _glcdossierDetailInfoServices { get; set; }
        public TGlcdriverInfoServices _glcdriverInfoServices {get;set; }
        [BindProperty]
        public List<TGlcdossierDetailInfoViewModel>? glcdossierDetailInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcdriverInfoViewModel>? glcdriverInfoViewModels { get; set; }
        [BindProperty]
        public TGlcdossierDetailInfoViewModel AddDsossierDetail { get; set; }
        [BindProperty]
        public ExcelUploadModel excelUploadModel { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;

        public AdminGlcDossierDetailInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcdriverInfoServices = new TGlcdriverInfoServices(db);
            _glcdossierDetailInfoServices = new TGlcdossierDetailInfoServices(db);
            AddDsossierDetail = new TGlcdossierDetailInfoViewModel();
            excelUploadModel = new ExcelUploadModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditDossierDetail(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
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
                    ErrorMessage = "یک حواله با این شماره قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcdossierDetailInfoServices.Add(glcdossierDetailInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
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
                ErrorMessage = "این حواله در جای دیگر استفاده شده است.";
                return Page();
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
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
            ErrorMessage = "در عملیات ویرایش مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostExcelUploadDossierDetail()
        {
            if(!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            return Page();
        }
        private void LoadData()
        {
            glcdossierDetailInfoViewModels = _mapper.Map<List<TGlcdossierDetailInfo>, List<TGlcdossierDetailInfoViewModel>>(_glcdossierDetailInfoServices.GetAll());
            glcdriverInfoViewModels = _mapper.Map<List<TGlcdriverInfo>, List<TGlcdriverInfoViewModel>>(_glcdriverInfoServices.GetAll());
        }
    }
}
