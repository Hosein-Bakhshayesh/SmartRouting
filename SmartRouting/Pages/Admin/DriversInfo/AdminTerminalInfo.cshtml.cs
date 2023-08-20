using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.DriversInfo
{
    public class AdminTerminalInfoModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcterminalInfoServices _glcterminalInfoServices;
        [BindProperty]
        public List<TGlcterminalInfoViewModel>? glcterminalInfoViewModels { get; set; }
        [BindProperty]
        public TGlcterminalInfoViewModel AddTerminal { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminTerminalInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcterminalInfoServices = new TGlcterminalInfoServices(db);
            AddTerminal = new TGlcterminalInfoViewModel();
        }

        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditTerminalInfo(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcterminalInfo glcTerminalInfo = new TGlcterminalInfo();
            glcTerminalInfo = _mapper.Map<TGlcterminalInfoViewModel, TGlcterminalInfo>(AddTerminal);
            bool res;
            if (isEdit)
            {
                glcTerminalInfo.GlcterminalInfoId = id;
                res = _glcterminalInfoServices.Update(glcTerminalInfo);
            }
            else
            {
                bool isExist = _glcterminalInfoServices.GetAll().Any(t => t.GlcterminalId == glcTerminalInfo.GlcterminalId);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "پایانه با این کد قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcterminalInfoServices.Add(glcTerminalInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcterminalInfoServices.Save();
            return RedirectToPage("AdminTerminalInfo");
        }

        public ActionResult OnPostDeleteTerminalInfo(int id)
        {
            if (id > 0)
            {
                try
                {
                    _glcterminalInfoServices.Delete(id);
                    _glcterminalInfoServices.Save();
                    return RedirectToPage("AdminTerminalInfo");
                }
                catch
                {
                    ErrorMessage = "از این پایانه در جای دیگر استفاده شده است و امکان حذف وجود ندارد.";
                    LoadData();
                    return Page();

                }
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditTerminalInfo(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcterminalInfo glcTerminalInfo = _glcterminalInfoServices.GetEntity(id);
                AddTerminal = _mapper.Map<TGlcterminalInfo, TGlcterminalInfoViewModel>(glcTerminalInfo);
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
            glcterminalInfoViewModels = _mapper.Map<List<TGlcterminalInfo>, List<TGlcterminalInfoViewModel>>(_glcterminalInfoServices.GetAll());
        }
    }
}
