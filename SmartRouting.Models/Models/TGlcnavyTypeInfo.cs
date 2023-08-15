using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models
{
    public partial class TGlcnavyTypeInfo:BaseEntity
    {
        public TGlcnavyTypeInfo()
        {
            TGlcnavyInfos = new HashSet<TGlcnavyInfo>();
        }

        public int GlcnavyTypeInfoId { get; set; }
        public string GlcnavyTypeName { get; set; } = null!;
        public double GlcnavyTypeLength { get; set; }
        public double GlcnavyTypeWidth { get; set; }
        public double GlcnavyTypeHeight { get; set; }
        public string GlcnavyTypeFuel { get; set; } = null!;
        public string? GlcnavyTypeModel { get; set; }

        public virtual ICollection<TGlcnavyInfo> TGlcnavyInfos { get; set; }
    }
}
