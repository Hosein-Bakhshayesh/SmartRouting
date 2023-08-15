﻿using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRouting.Repository.Repository
{
    public class TGlcdriverInfoRepository : GenericRepository<TGlcdriverInfo>, ITGlcdriverInfoRepository
    {
        public TGlcdriverInfoRepository(Db_SmartRoutingContext context) : base(context)
        {
        }
    }
}
