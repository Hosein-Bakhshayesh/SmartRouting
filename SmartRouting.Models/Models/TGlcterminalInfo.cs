using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models
{
    public partial class TGlcterminalInfo:BaseEntity
    {
        public TGlcterminalInfo()
        {
            TGlcdriverShiftInfos = new HashSet<TGlcdriverShiftInfo>();
        }

        public int GlcterminalInfoId { get; set; }
        public int GlcterminalId { get; set; }
        public string GlcterminalName { get; set; } = null!;

        public virtual ICollection<TGlcdriverShiftInfo> TGlcdriverShiftInfos { get; set; }
    }
}
