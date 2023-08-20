using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.NavyInfo
{
    public class AdminNavyInfoModel : PageModel
    {
        public Db_SmartRoutingContext db;
        public TGlcnavyInfoServices _glcnavyInfoServices { get; set; }
        public TGlcnavyTypeInfoServices _glcnavyTypeInfoServices { get; set; }
        public TGlcnavyOwnerInfoServices _glcnavyOwnerInfoServices { get; set; }
        public TPelakCharServices _pelakCharServices { get; set; }
        [BindProperty]
        public List<TGlcnavyInfoViewModel>? glcnavyInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcnavyTypeInfoViewModel>? glcnavyTypeInfoViewModels { get; set; }
        [BindProperty]
        public TGlcnavyInfoViewModel addNavy { get; set; }
        public IMapper _mapper { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminNavyInfoModel(IMapper mapper)
        {
            db = new Db_SmartRoutingContext();
            _glcnavyInfoServices = new TGlcnavyInfoServices(db);
            _glcnavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            _glcnavyOwnerInfoServices = new TGlcnavyOwnerInfoServices(db);
            _pelakCharServices = new TPelakCharServices(db);
            addNavy = new TGlcnavyInfoViewModel();
            _mapper = mapper;
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditNavy(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcnavyInfo glcnavyInfo = new TGlcnavyInfo();
            glcnavyInfo = _mapper.Map<TGlcnavyInfoViewModel, TGlcnavyInfo>(addNavy);
            bool res;
            if (isEdit)
            {
                glcnavyInfo.GlcnavyInfoId = id;
                res = _glcnavyInfoServices.Update(glcnavyInfo);
            }
            else
            {
                res = _glcnavyInfoServices.Add(glcnavyInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcnavyInfoServices.Save();
            return RedirectToPage("AdminNavyInfo");
        }

        public ActionResult OnPostDeleteNavy(int id)
        {
            if (id > 0)
            {
                try
                {
                    _glcnavyInfoServices.Delete(id);
                    _glcnavyInfoServices.Save();
                    return RedirectToPage("AdminNavyInfo");
                }
                catch
                {
                    ErrorMessage = "از این ناوگان در جای دیگر استفاده شده است و امکان جذف وجود ندارد.";
                    LoadData();
                    return Page();

                }
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }
        public ActionResult OnPostEditNavy(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcnavyInfo glcnavyInfo = _glcnavyInfoServices.GetEntity(id);
                addNavy = _mapper.Map<TGlcnavyInfo, TGlcnavyInfoViewModel>(glcnavyInfo);
                EditId = id;
                LoadData();
                return Page();
            }
            LoadData();
            ErrorMessage = "در عملیات ویرایش مشکلی پیش آمد.";
            return Page();
        }

        private void LoadData()
        {
            glcnavyInfoViewModels = _mapper.Map<List<TGlcnavyInfo>, List<TGlcnavyInfoViewModel>>(_glcnavyInfoServices.GetAll());
            glcnavyTypeInfoViewModels = _mapper.Map<List<TGlcnavyTypeInfo>,List<TGlcnavyTypeInfoViewModel>>(_glcnavyTypeInfoServices.GetAll());
        }
    }
}
