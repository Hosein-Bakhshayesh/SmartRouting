using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartRouting.Models;
using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;
using SmartRouting.Services.Services;

namespace SmartRouting.Pages.Map
{
    public class MapShowingAddressModel : PageModel
    {
        Db_SmartRoutingContext db = new Db_SmartRoutingContext();
        public IMapper _mapper { get; set; }
        public TGlcdossierDetailInfoServices _glcdossierDetailInfoServices { get; set; }
        [BindProperty]
        public List<TGlcdossierDetailInfoViewModel>? glcdossierDetailInfoViewModels { get; set; }
        public MapShowingAddressModel(IMapper mapper)
        {
            _mapper = mapper;
            _glcdossierDetailInfoServices = new TGlcdossierDetailInfoServices(db);
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

        private void LoadData()
        {
            glcdossierDetailInfoViewModels = _mapper.Map<List<TGlcdossierDetailInfo>, List<TGlcdossierDetailInfoViewModel>>(_glcdossierDetailInfoServices.GetAll());
        }
    }
}
