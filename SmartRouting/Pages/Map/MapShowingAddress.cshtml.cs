using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Office.Interop.Excel;
using RestSharp;
using SmartRouting.Models;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace SmartRouting.Pages.Map
{
    public class MapShowingAddressModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcdossierDetailInfoServices _glcdossierDetailInfoServices { get; set; }
        public TGlcsmsServices _glcsmsServices { get; set; }
        public TGlcdriverInfoServices _glcsdriverInfoServices { get;set; }
        public TGlcdriverShiftInfoServices _glcdriverShiftInfoServices { get; set; }
        public TGlcterminalInfoServices _glcterminalInfoServices { get; set; }
        [BindProperty]
        public List<TGlcdossierDetailInfoViewModel>? glcdossierDetailInfoViewModels { get; set; }
        public MapShowingAddressModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcdossierDetailInfoServices = new TGlcdossierDetailInfoServices(db);
            _glcsmsServices = new TGlcsmsServices(db);
            _glcsdriverInfoServices = new TGlcdriverInfoServices(db);
            _glcdriverShiftInfoServices = new TGlcdriverShiftInfoServices(db);
            _glcterminalInfoServices = new TGlcterminalInfoServices(db);
        }
        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnGetAddresses()
        {
            LoadData();
            return new JsonResult(glcdossierDetailInfoViewModels);
        }

        public ActionResult OnPostFindDistanceTrafic([FromBody] Distance distance)
        {
            string baseURL = $"https://api.neshan.org/v1/distance-matrix?type=car&origins={distance.OriginLatLng}&destinations={distance.DistanceLatLng}";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Api-Key", "service.551b79613b42405daadad2dccdda8bb1");
            DistanceJson serialize = httpClient.GetFromJsonAsync<DistanceJson>(baseURL).Result;
            return new JsonResult(serialize);
        }

        public ActionResult OnPostFindDistance([FromBody] Distance distance)
        {
            string baseURL = $"https://api.neshan.org/v1/distance-matrix/no-traffic?type=car&origins={distance.OriginLatLng}&destinations={distance.DistanceLatLng}";
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Api-Key", "service.551b79613b42405daadad2dccdda8bb1");
            DistanceJson serialize = httpClient.GetFromJsonAsync<DistanceJson>(baseURL).Result;
            return new JsonResult(serialize);
        }

        public ActionResult OnPostSubmitAddress()
        {
            List<TGlcSmsViewModel> glcSmsList = new List<TGlcSmsViewModel>();
            List<int> DriversId = new List<int>();
            LoadData();
            foreach (var item in glcdossierDetailInfoViewModels)
            {
                if(!DriversId.Any(t => t == item.DossierDetailDriverId))
                {
                    DriversId.Add(item.DossierDetailDriverId);
                }
            }
            foreach (var driver in DriversId)
            {
                TGlcSmsViewModel glcSms = new TGlcSmsViewModel();
                var driverinfo = _glcsdriverInfoServices.GetEntity(driver);
                glcSms.GlcsmsMobileNumber = driverinfo.GlcdriverMobile;
                var sumQuantitu = glcdossierDetailInfoViewModels.FindAll(t => t.DossierDetailDriverId == driver).Sum(t => t.DossierDetailQuantity);
                var sumAddressCount = glcdossierDetailInfoViewModels.FindAll(t => t.DossierDetailDriverId == driver).Distinct(new AddressComparer()).Count();
                var DateTime = glcdossierDetailInfoViewModels.Find(t => t.DossierDetailDriverId == driver).DossierDetailDateTime;
                var driverShift = _glcdriverShiftInfoServices.GetAll().Find(t => t.GlcdriverShiftDriverId == driver);
                var TerminalName = _glcterminalInfoServices.GetEntity(driverShift.GlcdriverShiftTerminalId).GlcterminalName;
                string smsDiscription = $"همکار گرامی\n {driverinfo.GlcdriverName + " " + driverinfo.GlcdriverLastName} \n تعداد {sumQuantitu} کالا برای {sumAddressCount} آدرس برای شما تعریف شده است.";
                smsDiscription += $"\n تاریخ : {DateTime}";
                smsDiscription += $"\n شیفت : {driverShift.GlcdriverShiftBeginHour} الی {driverShift.GlcdriverShiftEndHour}";
                smsDiscription += $"\nبرای مشاهده آدرس ها به اپلیکیشن مراجعه و جهت دریافت کالا راس ساعت {driverShift.GlcdriverShiftBeginHour} در {TerminalName} حاضر شوید.";
                smsDiscription += "\n واحد توزیع شرکت لجستیک گلدیران";
                glcSms.GlcsmsDiscription = smsDiscription ;
                glcSmsList.Add(glcSms);
            }
            foreach (var sms in glcSmsList)
            {
                var client = new RestClient("http://panel.asanak.com/webservice/v1rest/sendsms");
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded",
                    $"username=goldiransmsuser&password=#tarnavaz3989&source=982151651&destination={sms.GlcsmsMobileNumber}&message={sms.GlcsmsDiscription}",ParameterType.RequestBody);
                RestResponse response = client.ExecutePost(request);
            }
            return RedirectToPage("Index");

        }

        private void LoadData()
        {
            glcdossierDetailInfoViewModels = _mapper.Map<List<TGlcdossierDetailInfo>, List<TGlcdossierDetailInfoViewModel>>(_glcdossierDetailInfoServices.GetAll());
        }
        

    }

    internal class AddressComparer : IEqualityComparer<TGlcdossierDetailInfoViewModel>
    {
        public bool Equals(TGlcdossierDetailInfoViewModel? x, TGlcdossierDetailInfoViewModel? y)
        {
            return x.DossierDetailLength == y.DossierDetailLength && x.DossierDetailWidth == y.DossierDetailWidth;
        }

        public int GetHashCode([DisallowNull] TGlcdossierDetailInfoViewModel obj)
        {
            return obj.GlcdossierDetailInfoId.GetHashCode() ^ (obj.DossierDetailLength == null ? 0 : obj.DossierDetailLength.GetHashCode()) ^ (obj.DossierDetailWidth == null ? 0 : obj.DossierDetailWidth.GetHashCode());
        }
    }
}
