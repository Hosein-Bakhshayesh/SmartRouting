using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Models;
using SmartRouting.Models.Context;
using SmartRouting.Services.Services;
using AutoMapper;
using SmartRouting.Models.ViewModels;

namespace SmartRouting.Pages.Admin
{
    public class AdminNavyTypeInfoModel : PageModel
    {
        public Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        [BindProperty]
        public List<TGlcnavyTypeInfoViewModel>? GlcnavyTypeInfoViewModel { get; set; }
        public List<TGlcnavyTypeInfo>? GlcNavyTypeInfos { get; set; }
        public TGlcnavyTypeInfoServices _glcNavyTypeInfoServices { get; set; }
        public TglcfuelTypeInfoServices _glcfuelTypeInfoServices { get; set; }
        public TglcnavyRoomTypeInfoServices _glcnavyRoomTypeInfoServices  { get; set; }
        public List<TglcfuelTypeInfoViewModel>? tglcfuelTypeInfoViewModels { get; set; }
        public List<TglcnavyRoomTypeInfoViewModel>? tglcnavyRoomTypeInfoViewModels{ get; set; }
        public IMapper _mapper { get; set; }
        [BindProperty]
        public TGlcnavyTypeInfoViewModel AddNavyType { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminNavyTypeInfoModel(IMapper mapper)
        {
            _glcNavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            _glcfuelTypeInfoServices = new TglcfuelTypeInfoServices(db);
            _glcnavyRoomTypeInfoServices = new TglcnavyRoomTypeInfoServices(db);
            _mapper = mapper;
            AddNavyType = new TGlcnavyTypeInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditNavyType(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcnavyTypeInfo glcnavyTypeInfo = new TGlcnavyTypeInfo();
            glcnavyTypeInfo = _mapper.Map<TGlcnavyTypeInfoViewModel, TGlcnavyTypeInfo>(AddNavyType);
            if (string.IsNullOrEmpty(glcnavyTypeInfo.GlcnavyTypeModel.ToString()))
            {
                glcnavyTypeInfo.GlcnavyTypeModel = 1;
            }
            bool res;
            if (isEdit)
            {
                glcnavyTypeInfo.GlcnavyTypeInfoId = id;
                res = _glcNavyTypeInfoServices.Update(glcnavyTypeInfo);
            }
            else
            {
                res = _glcNavyTypeInfoServices.Add(glcnavyTypeInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcNavyTypeInfoServices.Save();
            return RedirectToPage("AdminNavyTypeInfo");
        }

        public ActionResult OnPostDeleteNavyType(int id)
        {
            try
            {
                if (id > 0)
                {
                    _glcNavyTypeInfoServices.Delete(id);
                    _glcNavyTypeInfoServices.Save();
                    return RedirectToPage("AdminNavyTypeInfo");
                }
                LoadData();
                ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
                return Page();
            }
            catch
            {
                LoadData();
                ErrorMessage = "این نوع ناوگان در جای دیگر استفاده شده است.";
                return Page();
            }
        }

        public ActionResult OnPostEditNavyType(int id)
        {
            if (id > 0)
            {
                IsEdit = true;
                TGlcnavyTypeInfo glcnavyTypeInfo = _glcNavyTypeInfoServices.GetEntity(id);
                AddNavyType = _mapper.Map<TGlcnavyTypeInfo, TGlcnavyTypeInfoViewModel>(glcnavyTypeInfo);
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
            GlcNavyTypeInfos = _glcNavyTypeInfoServices.GetAll();
            GlcnavyTypeInfoViewModel = _mapper.Map<List<TGlcnavyTypeInfo>, List<TGlcnavyTypeInfoViewModel>>(GlcNavyTypeInfos);
            tglcfuelTypeInfoViewModels = _mapper.Map<List<TGlcfuelTypeInfo>, List<TglcfuelTypeInfoViewModel>>(_glcfuelTypeInfoServices.GetAll());
            tglcnavyRoomTypeInfoViewModels = _mapper.Map<List<TGlcnavyRoomTypeInfo>, List<TglcnavyRoomTypeInfoViewModel>>(_glcnavyRoomTypeInfoServices.GetAll());
        }
    }
}
