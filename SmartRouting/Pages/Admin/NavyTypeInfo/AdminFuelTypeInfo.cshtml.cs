using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin
{
    public class AdminFuelTypeInfoModel : PageModel
    {
        public Db_SmartRoutingContext db;
        public TglcfuelTypeInfoServices _tglcfuelTypeInfoServices { get; set; }
        public TGlcnavyTypeInfoServices _glcnavyTypeInfoServices { get; set; }
        [BindProperty]
        public List<TglcfuelTypeInfoViewModel>? tglcfuelTypeInfoViewModels { get; set; }
        [BindProperty]
        public TglcfuelTypeInfoViewModel addFuel { get; set; }
        public IMapper _mapper { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminFuelTypeInfoModel(IMapper mapper)
        {
            db = new Db_SmartRoutingContext();
            _tglcfuelTypeInfoServices = new TglcfuelTypeInfoServices(db);
            _glcnavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            addFuel = new TglcfuelTypeInfoViewModel();
            _mapper = mapper;
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditFuelType(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcfuelTypeInfo tglcfuelTypeInfo = new TGlcfuelTypeInfo();
            tglcfuelTypeInfo = _mapper.Map<TglcfuelTypeInfoViewModel, TGlcfuelTypeInfo>(addFuel);
            bool res;
            if (isEdit)
            {
                tglcfuelTypeInfo.GlcfuelTypeId = id;
                res = _tglcfuelTypeInfoServices.Update(tglcfuelTypeInfo);
            }
            else
            {
                bool isExist = _tglcfuelTypeInfoServices.GetAll().Any(t => t.GlcfurlTypeName == tglcfuelTypeInfo.GlcfurlTypeName);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "نوع سوخت با این نام قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _tglcfuelTypeInfoServices.Add(tglcfuelTypeInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _tglcfuelTypeInfoServices.Save();
            return RedirectToPage("AdminFuelTypeInfo");
        }
        public ActionResult OnPostDeleteFuelType(int id)
        {
            try
            {
                if (id > 0)
                {
                    _tglcfuelTypeInfoServices.Delete(id);
                    _tglcfuelTypeInfoServices.Save();
                    return RedirectToPage("AdminFuelTypeInfo");
                }
            }
            catch
            {
                LoadData();
                ErrorMessage = "این نوع سوخت در جای دیگر استفاده شده است.";
                return Page();
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditFuelType(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcfuelTypeInfo tglcfuelTypeInfo = _tglcfuelTypeInfoServices.GetEntity(id);
                addFuel = _mapper.Map<TGlcfuelTypeInfo, TglcfuelTypeInfoViewModel>(tglcfuelTypeInfo);
                EditId = id;
                LoadData();
                return Page();

            }
            LoadData();
            ErrorMessage = "در عملیات ویرایش مشکلی پیش آمد.";
            return Page();
        }
        public void LoadData()
        {
            tglcfuelTypeInfoViewModels = _mapper.Map<List<TGlcfuelTypeInfo>, List<TglcfuelTypeInfoViewModel>>(_tglcfuelTypeInfoServices.GetAll());
        }
    }
}
