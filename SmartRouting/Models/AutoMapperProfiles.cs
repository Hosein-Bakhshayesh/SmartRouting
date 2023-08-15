﻿using AutoMapper;
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
        }
    }
}