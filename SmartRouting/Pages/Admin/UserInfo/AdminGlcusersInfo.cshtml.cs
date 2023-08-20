using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.UserInfo
{
    public class AdminGlcusersInfoModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcusersInfoServices _glcusersInfoServices { get; set; }
        public TGlcuserRoleInfoServices _glcuserRoleInfoServices { get; set; }
        [BindProperty]
        public List<TGlcusersInfoViewModel>? glcusersInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcuserRoleInfoViewModel>? glcuserRoleInfoViewModels { get; set; }
        [BindProperty]
        public TGlcusersInfoViewModel AddUser { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;

        public AdminGlcusersInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcuserRoleInfoServices = new TGlcuserRoleInfoServices(db);
            _glcusersInfoServices = new TGlcusersInfoServices(db);
            AddUser = new TGlcusersInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditUser(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcusersInfo glcuserInfo = new TGlcusersInfo();
            glcuserInfo = _mapper.Map<TGlcusersInfoViewModel, TGlcusersInfo>(AddUser);
            bool res;
            if (isEdit)
            {
                glcuserInfo.GlcusersInfoId = id;
                res = _glcusersInfoServices.Update(glcuserInfo);
            }
            else
            {
                bool isExist = _glcusersInfoServices.GetAll().Any(t => t.GlcusersNationalCode == glcuserInfo.GlcusersNationalCode);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "یک کاربر با این کدملی قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcusersInfoServices.Add(glcuserInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcusersInfoServices.Save();
            return RedirectToPage("AdminGlcusersInfo");
        }

        public ActionResult OnPostDeleteUser(int id)
        {
            try
            {
                if (id > 0)
                {
                    _glcusersInfoServices.Delete(id);
                    _glcusersInfoServices.Save();
                    return RedirectToPage("AdminGlcusersInfo");
                }
            }
            catch
            {
                LoadData();
                ErrorMessage = "این کاربر در جای دیگر استفاده شده است.";
                return Page();
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditUser(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcusersInfo glcUserInfo = _glcusersInfoServices.GetEntity(id);
                AddUser = _mapper.Map<TGlcusersInfo, TGlcusersInfoViewModel>(glcUserInfo);
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
            glcusersInfoViewModels = _mapper.Map<List<TGlcusersInfo>, List<TGlcusersInfoViewModel>>(_glcusersInfoServices.GetAll());

        }
    }
}
