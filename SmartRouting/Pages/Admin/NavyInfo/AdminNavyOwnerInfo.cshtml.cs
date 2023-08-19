using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.NavyInfo
{
    public class AdminNavyOwnerInfoModel : PageModel
    {
        public Db_SmartRoutingContext db;
        public TGlcnavyOwnerInfoServices _glcnavyOwnerInfoServices { get; set; }
        [BindProperty]
        public List<TGlcnavyOwnerInfoViewModel>? glcnavyOwnerInfoViewModels { get; set; }
        [BindProperty]
        public TGlcnavyOwnerInfoViewModel addOwner { get; set; }
        public IMapper _mapper { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;

        public AdminNavyOwnerInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            db = new Db_SmartRoutingContext();
            addOwner = new TGlcnavyOwnerInfoViewModel();
            _glcnavyOwnerInfoServices = new TGlcnavyOwnerInfoServices(db);
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditNavyOwner(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcnavyOwnerInfo glcNavyOwner = new TGlcnavyOwnerInfo();
            glcNavyOwner = _mapper.Map<TGlcnavyOwnerInfoViewModel, TGlcnavyOwnerInfo>(addOwner);
            bool res;
            if (isEdit)
            {
                glcNavyOwner.GlcnavyOwnerId = id;
                res = _glcnavyOwnerInfoServices.Update(glcNavyOwner);
            }
            else
            {
                bool isExist = _glcnavyOwnerInfoServices.GetAll().Any(t => t.GlcnavyOwnerName == glcNavyOwner.GlcnavyOwnerName);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "نوع مالکیت با این نام قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcnavyOwnerInfoServices.Add(glcNavyOwner);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcnavyOwnerInfoServices.Save();
            return RedirectToPage("AdminNavyOwnerInfo");
        }

        public ActionResult OnPostDeleteNavyOwner(int id)
        {
            if (id > 0)
            {
                try
                {
                    _glcnavyOwnerInfoServices.Delete(id);
                    _glcnavyOwnerInfoServices.Save();
                    return RedirectToPage("AdminNavyOwnerInfo");
                }
                catch
                {
                    ErrorMessage = "از این مالکیت در جای دیگر استفاده شده است و امکان حذف وجود ندارد.";
                    LoadData();
                    return Page();

                }
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }
        public ActionResult OnPostEditNavyOwner(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcnavyOwnerInfo glcNavyOwnerInfo = _glcnavyOwnerInfoServices.GetEntity(id);
                addOwner = _mapper.Map<TGlcnavyOwnerInfo, TGlcnavyOwnerInfoViewModel>(glcNavyOwnerInfo);
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
            glcnavyOwnerInfoViewModels = _mapper.Map<List<TGlcnavyOwnerInfo>, List<TGlcnavyOwnerInfoViewModel>>(_glcnavyOwnerInfoServices.GetAll());
        }
    }
}
