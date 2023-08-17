using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Admin.NavyTypeInfo
{
    public class AdminRoomTypeInfo : PageModel
    {
        public Db_SmartRoutingContext db;
        public TglcnavyRoomTypeInfoServices _glcnavyRoomTypeInfoServices { get; set; }
        public TGlcnavyTypeInfoServices _glcnavyTypeInfoServices { get; set; }
        [BindProperty]
        public List<TglcnavyRoomTypeInfoViewModel>? tglcnavyRoomTypeInfoViewModel { get; set; }
        [BindProperty]
        public TglcnavyRoomTypeInfoViewModel addRoom { get; set; }
        public IMapper _mapper { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminRoomTypeInfo(IMapper mapper)
        {
            db = new Db_SmartRoutingContext();
            _mapper = mapper;
            _glcnavyRoomTypeInfoServices = new TglcnavyRoomTypeInfoServices(db);
            _glcnavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            addRoom = new TglcnavyRoomTypeInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditRoomType(bool isEdit, int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcnavyRoomTypeInfo tglcnavyRoomTypeInfo = new TGlcnavyRoomTypeInfo();
            tglcnavyRoomTypeInfo = _mapper.Map<TglcnavyRoomTypeInfoViewModel, TGlcnavyRoomTypeInfo>(addRoom);
            bool res;
            if (isEdit)
            {
                tglcnavyRoomTypeInfo.GlcnavyRoomTypeId = id;
                res = _glcnavyRoomTypeInfoServices.Update(tglcnavyRoomTypeInfo);
            }
            else
            {
                bool isExist = _glcnavyRoomTypeInfoServices.GetAll().Any(t => t.GlcnavyRoomTypeName == tglcnavyRoomTypeInfo.GlcnavyRoomTypeName);
                if (isExist)
                {
                    LoadData();
                    ErrorMessage = "نوع سوخت با این نام قبلا در سامانه ثبت شده است.";
                    return Page();
                }
                res = _glcnavyRoomTypeInfoServices.Add(tglcnavyRoomTypeInfo);
            }
            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            _glcnavyRoomTypeInfoServices.Save();
            return RedirectToPage("AdminRoomTypeInfo");
        }
        public ActionResult OnPostDeleteRoomType(int id)
        {
            try
            {
                if (id > 0)
                {
                    _glcnavyRoomTypeInfoServices.Delete(id);
                    _glcnavyRoomTypeInfoServices.Save();
                    return RedirectToPage("AdminRoomTypeInfo");
                }
            }
            catch
            {
                LoadData();
                ErrorMessage = "این نوع اتاق در جای دیگر استفاده شده است.";
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
                TGlcnavyRoomTypeInfo tglcnavyRoomTypeInfo = _glcnavyRoomTypeInfoServices.GetEntity(id);
                addRoom = _mapper.Map<TGlcnavyRoomTypeInfo, TglcnavyRoomTypeInfoViewModel>(tglcnavyRoomTypeInfo);
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
            tglcnavyRoomTypeInfoViewModel = _mapper.Map<List<TGlcnavyRoomTypeInfo>, List<TglcnavyRoomTypeInfoViewModel>>(_glcnavyRoomTypeInfoServices.GetAll());
        }
    }
}
