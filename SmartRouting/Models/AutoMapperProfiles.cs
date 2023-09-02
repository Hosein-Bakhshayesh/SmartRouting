using AutoMapper;
using SmartRouting.Models.Models;
using SmartRouting.Models.ViewModels;

namespace SmartRouting.Models
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TGlcnavyTypeInfo, TGlcnavyTypeInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyTypeInfoViewModel, TGlcnavyTypeInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyRoomTypeInfo, TglcnavyRoomTypeInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TglcnavyRoomTypeInfoViewModel, TGlcnavyRoomTypeInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcfuelTypeInfo, TglcfuelTypeInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TglcfuelTypeInfoViewModel, TGlcfuelTypeInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyInfo, TGlcnavyInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyInfoViewModel, TGlcnavyInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyOwnerInfo, TGlcnavyOwnerInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcnavyOwnerInfoViewModel, TGlcnavyOwnerInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdriverInfo, TGlcdriverInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdriverInfoViewModel, TGlcdriverInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcterminalInfo, TGlcterminalInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcterminalInfoViewModel, TGlcterminalInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdriverShiftInfo, TGlcdriverShiftInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdriverShiftInfoViewModel, TGlcdriverShiftInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcuserRoleInfo, TGlcuserRoleInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcuserRoleInfoViewModel, TGlcuserRoleInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcusersInfo, TGlcusersInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcusersInfoViewModel, TGlcusersInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdossierDetailInfo, TGlcdossierDetailInfoViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcdossierDetailInfoViewModel, TGlcdossierDetailInfo>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcsms, TGlcSmsViewModel>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<TGlcSmsViewModel, TGlcsms>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
