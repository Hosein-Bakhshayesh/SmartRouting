﻿using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Services.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRouting.Services.Services
{
    public class TGlcnavyInfoServices : GenericServices<TGlcnavyInfo>, ITGlcnavyInfoServices
    {
        public TGlcnavyInfoServices(Db_SmartRoutingContext context) : base(context)
        {
        }
    }
}
