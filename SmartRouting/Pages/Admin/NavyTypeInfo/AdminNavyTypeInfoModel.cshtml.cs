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
        public TGlcnavyTypeInfoViewModel AddNavyType { get; set; }
        public AdminNavyTypeInfoModel(IMapper mapper)
        {
            _glcNavyTypeInfoServices = new TGlcnavyTypeInfoServices(db);
            _mapper = mapper;
            AddNavyType = new TGlcnavyTypeInfoViewModel();
        }
        public void OnGet()
        {
            GlcNavyTypeInfos = _glcNavyTypeInfoServices.GetAll();
            GlcnavyTypeInfoViewModel = _mapper.Map<List<TGlcnavyTypeInfo>, List<TGlcnavyTypeInfoViewModel>>(GlcNavyTypeInfos);
        }
    }
}
