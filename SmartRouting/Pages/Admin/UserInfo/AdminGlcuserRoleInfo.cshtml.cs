using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.UserInfo
{
    public class AdminGlcuserRoleInfoModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcuserRoleInfoServices _glcuserRoleInfoServices { get; set; }
        [BindProperty]
        public List<TGlcuserRoleInfoViewModel>? glcuserRoleInfoViewModels { get; set; }
        [BindProperty]
        public TGlcuserRoleInfoViewModel AddRole { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminGlcuserRoleInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcuserRoleInfoServices = new TGlcuserRoleInfoServices(db);
            AddRole = new TGlcuserRoleInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditUserRole(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcuserRoleInfo glcuserRoleInfo = new TGlcuserRoleInfo();
            glcuserRoleInfo = _mapper.Map<TGlcuserRoleInfoViewModel, TGlcuserRoleInfo>(AddRole);
            bool res;
            if (isEdit)
            {
                glcuserRoleInfo.GlcuserRoleInfoId = id;
                res = _glcuserRoleInfoServices.Update(glcuserRoleInfo);
            }
            else
            {
                bool isExist = _glcuserRoleInfoServices.GetAll().Any(t => t.GlcuserRoleName == glcuserRoleInfo.GlcuserRoleName);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "یک نقش با این نام قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcuserRoleInfoServices.Add(glcuserRoleInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcuserRoleInfoServices.Save();
            return RedirectToPage("AdminGlcuserRoleInfo");
        }

        public ActionResult OnPostDeleteUserRole(int id)
        {
            try
            {
                if (id > 0)
                {
                    _glcuserRoleInfoServices.Delete(id);
                    _glcuserRoleInfoServices.Save();
                    return RedirectToPage("AdminGlcuserRoleInfo");
                }
            }
            catch
            {
                LoadData();
                ErrorMessage = "این نقش در جای دیگر استفاده شده است.";
                return Page();
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditUserRole(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcuserRoleInfo glcuserRoleInfo = _glcuserRoleInfoServices.GetEntity(id);
                AddRole = _mapper.Map<TGlcuserRoleInfo, TGlcuserRoleInfoViewModel>(glcuserRoleInfo);
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
            glcuserRoleInfoViewModels = _mapper.Map<List<TGlcuserRoleInfo>, List<TGlcuserRoleInfoViewModel>>(_glcuserRoleInfoServices.GetAll()); 
        }
    }
}
