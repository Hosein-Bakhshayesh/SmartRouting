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
        [BindProperty]
        public List<TGlcnavyTypeInfoViewModel>? GlcnavyTypeInfoViewModel { get; set; }
        public List<TGlcnavyTypeInfo>? GlcNavyTypeInfos { get; set; }
        public Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public TGlcnavyTypeInfoServices _glcNavyTypeInfoServices { get; set; }
        public IMapper _mapper { get; set; }
        [BindProperty]
        public TGlcnavyTypeInfoViewModel AddNavyType { get; set; }
        public string ErrorMessage = "";
        public bool IsEdit;
        public int EditId;
        public AdminNavyTypeInfoModel(IMapper mapper)
        {
            _glcNavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            _mapper = mapper;
            AddNavyType = new TGlcnavyTypeInfoViewModel();
        }
        public void OnGet()
        {
            IsEdit = false;
            LoadData();
        }
        public ActionResult OnPostAddOrEditNavyType(bool isEdit,int id)
        {
            if (!ModelState.IsValid)
            {
                LoadData();
                ErrorMessage = "لطفا فیلد ها را بدرستی پر کنید.";
                return Page();
            }
            TGlcnavyTypeInfo glcnavyTypeInfo = new TGlcnavyTypeInfo();
            glcnavyTypeInfo = _mapper.Map<TGlcnavyTypeInfoViewModel, TGlcnavyTypeInfo>(AddNavyType);
            if (string.IsNullOrEmpty(glcnavyTypeInfo.GlcnavyTypeModel))
            {
                glcnavyTypeInfo.GlcnavyTypeModel = "ندارد";
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
                _glcNavyTypeInfoServices.Save();
            }

            if (!res)
            {
                LoadData();
                ErrorMessage = "در عملیات ذخیره سازی مشکلی پیش آمد.";
                return Page();
            }
            return RedirectToPage("AdminNavyTypeInfoModel");
        }

        public ActionResult OnPostDeleteNavyType(int id)
        {
            if (id > 0)
            {
                _glcNavyTypeInfoServices.Delete(id);
                _glcNavyTypeInfoServices.Save();
                return RedirectToPage("AdminNavyTypeInfoModel");
            }
            LoadData();
            ErrorMessage = "در عملیات حذف مشکلی پیش آمد.";
            return Page();
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
        }
    }
}
