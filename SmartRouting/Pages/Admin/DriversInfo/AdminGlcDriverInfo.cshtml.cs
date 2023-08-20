using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.DriversInfo
{
    public class AdminGlcDriverInfoModel : PageModel
    {
        public IMapper _mapper { get; set; }
        public IWebHostEnvironment _env { get; set; }
        public Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public TGlcdriverInfoServices _glcdriverInfoServices { get; set; }
        public TGlcnavyInfoServices _glcnavyInfoServices { get; set; }
        public TGlcnavyTypeInfoServices _glcnavyTypeInfoServices { get; set; }
        [BindProperty]
        public List<TGlcdriverInfoViewModel>? glcdriverInfoViewModels { get; set; }
        [BindProperty]
        public List<TGlcnavyInfoViewModel>? glcnavyInfoViewModels { get; set; }        
        [BindProperty]
        public TGlcdriverInfoViewModel AddDriver { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminGlcDriverInfoModel(IMapper mapper,IWebHostEnvironment env)
        {
            _mapper = mapper;
            _glcnavyInfoServices = new TGlcnavyInfoServices(db);
            _glcdriverInfoServices = new TGlcdriverInfoServices(db);
            _glcnavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            AddDriver = new TGlcdriverInfoViewModel();
            _env = env;
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }

        public ActionResult OnPostAddOrEditDriverInfo(bool isEdit, int id)
        {
            string uniqueFileName;
            if (AddDriver.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "DriversImage");
                uniqueFileName = Guid.NewGuid().ToString().Replace("-","") + "_" + AddDriver.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    AddDriver.ImageFile.CopyTo(fileStream);
                }
                AddDriver.GlcdriverPhotoPath = uniqueFileName;
            }
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcdriverInfo glcDriverInfo = new TGlcdriverInfo();
            glcDriverInfo = _mapper.Map<TGlcdriverInfoViewModel, TGlcdriverInfo>(AddDriver);
            bool res;
            if (isEdit)
            {
                glcDriverInfo.GlcdriverInfoId = id;
                res = _glcdriverInfoServices.Update(glcDriverInfo);
            }
            else
            {
                bool isExist = _glcdriverInfoServices.GetAll().Any(t => t.GlcdriverNationalCode == glcDriverInfo.GlcdriverNationalCode);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "راننده با این شماره ملی قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcdriverInfoServices.Add(glcDriverInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcdriverInfoServices.Save();
            return RedirectToPage("AdminGlcDriverInfo");
        }

        public ActionResult OnPostDeleteDriverInfo(int id)
        {
            if (id > 0)
            {
                try
                {
                    var ImageName = _glcdriverInfoServices.GetEntity(id).GlcdriverPhotoPath;
                    string uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "DriversImage");
                    var filePath = Path.Combine(uploadsFolder, ImageName);
                    _glcdriverInfoServices.Delete(id);
                    _glcdriverInfoServices.Save();
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    return RedirectToPage("AdminGlcDriverInfo");
                }
                catch
                {
                    ErrorMessage = "از این راننده در جای دیگر استفاده شده است و امکان حذف وجود ندارد.";
                    LoadData();
                    return Page();

                }
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
        }

        public ActionResult OnPostEditDriverInfo(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcdriverInfo glcDriverInfo = _glcdriverInfoServices.GetEntity(id);
                AddDriver = _mapper.Map<TGlcdriverInfo, TGlcdriverInfoViewModel>(glcDriverInfo);
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
            glcdriverInfoViewModels = _mapper.Map<List<TGlcdriverInfo>,List<TGlcdriverInfoViewModel>>(_glcdriverInfoServices.GetAll());
            glcnavyInfoViewModels = _mapper.Map<List<TGlcnavyInfo>,List<TGlcnavyInfoViewModel>>(_glcnavyInfoServices.GetAll());
        }
    }
}
