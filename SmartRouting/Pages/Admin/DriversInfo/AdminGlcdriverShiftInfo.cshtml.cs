using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.DriversInfo
{
    public class AdminGlcdriverShiftInfoModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcdriverShiftInfoServices _glcdriverShiftInfoServices { get; set; }
        public TGlcterminalInfoServices _glcterminalInfoServices { get; set; }
        public TGlcdriverInfoServices _glcdriverInfoServices { get; set; }
        [BindProperty]
        public List<TGlcdriverShiftInfoViewModel>? glcdriverShiftInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcdriverInfoViewModel>? glcdriverInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcterminalInfoViewModel> glcterminalInfoViewModels { get; set; }
        [BindProperty]
        public TGlcdriverShiftInfoViewModel AddShift { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;

        public AdminGlcdriverShiftInfoModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcdriverShiftInfoServices = new TGlcdriverShiftInfoServices(db);
            _glcdriverInfoServices = new TGlcdriverInfoServices(db);
            _glcterminalInfoServices = new TGlcterminalInfoServices(db);
            AddShift = new TGlcdriverShiftInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditDriverShiftInfo(bool isEdit, int id)
        { 
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcdriverShiftInfo glcDriverShiftInfo = new TGlcdriverShiftInfo();
            glcDriverShiftInfo = _mapper.Map<TGlcdriverShiftInfoViewModel, TGlcdriverShiftInfo>(AddShift);
            bool res;
            if (isEdit)
            {
                glcDriverShiftInfo.GlcdriverShiftInfoId = id;
                res = _glcdriverShiftInfoServices.Update(glcDriverShiftInfo);
            }
            else
            {
                res = _glcdriverShiftInfoServices.Add(glcDriverShiftInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcdriverShiftInfoServices.Save();
            return RedirectToPage("AdminGlcdriverShiftInfo");
        }

        public ActionResult OnPostDeleteDriverShiftInfo(int id)
        {
            if (id > 0)
            {
                try
                {
                    _glcdriverShiftInfoServices.Delete(id);
                    _glcdriverShiftInfoServices.Save();
                    return RedirectToPage("AdminGlcdriverShiftInfo");
                }
                catch
                {
                    ErrorMessage = "از این شیفت کاری در جای دیگر استفاده شده است و امکان حذف وجود ندارد.";
                    LoadData();
                    return Page();

                }
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditDriverShiftInfo(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcdriverShiftInfo glcDriverShiftInfo = _glcdriverShiftInfoServices.GetEntity(id);
                AddShift = _mapper.Map<TGlcdriverShiftInfo, TGlcdriverShiftInfoViewModel>(glcDriverShiftInfo);
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
            glcdriverShiftInfoViewModels = _mapper.Map<List<TGlcdriverShiftInfo>,List<TGlcdriverShiftInfoViewModel>>(_glcdriverShiftInfoServices.GetAll());
            glcdriverInfoViewModels = _mapper.Map<List<TGlcdriverInfo>, List<TGlcdriverInfoViewModel>>(_glcdriverInfoServices.GetAll());
            glcterminalInfoViewModels = _mapper.Map<List<TGlcterminalInfo>, List<TGlcterminalInfoViewModel>>(_glcterminalInfoServices.GetAll());
        }
    }
}
