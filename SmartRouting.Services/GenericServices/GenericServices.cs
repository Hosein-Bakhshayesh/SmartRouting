using SmartRouting.Models.Context;
using SmartRouting.Models.Models;
using SmartRouting.Repository.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartRouting.Services.GenericServices
{
    public class GenericServices<T> : GenericRepository<T> where T:BaseEntity
    {
        public GenericServices(Db_SmartRoutingContext context) : base(context)
        {
        }
    }
}
