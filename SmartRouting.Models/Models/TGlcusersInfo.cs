using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models
{
    public partial class TGlcusersInfo:BaseEntity
    {
        public int GlcusersInfoId { get; set; }
        public string GlcusersNationalCode { get; set; } = null!;
        public string GlcusersName { get; set; } = null!;
        public string GlcusersLastName { get; set; } = null!;
        public string GlcusersMobile { get; set; } = null!;
        public string GlcusersType { get; set; } = null!;
    }
}
