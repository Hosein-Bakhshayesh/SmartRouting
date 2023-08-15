using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models
{
    public partial class TGlcdriverInfo:BaseEntity
    {
        public TGlcdriverInfo()
        {
            TGlcdossierDetailInfos = new HashSet<TGlcdossierDetailInfo>();
            TGlcdriverShiftInfos = new HashSet<TGlcdriverShiftInfo>();
        }

        public int GlcdriverInfoId { get; set; }
        public string GlcdriverNationalCode { get; set; } = null!;
        public string GlcdriverName { get; set; } = null!;
        public string GlcdriverLastName { get; set; } = null!;
        public string GlcdriverMobile { get; set; } = null!;
        public string? GlcdriverPhotoPath { get; set; }
        public int GlcdriverNlgcid { get; set; }

        public virtual TGlcnavyInfo GlcdriverNlgc { get; set; } = null!;
        public virtual ICollection<TGlcdossierDetailInfo> TGlcdossierDetailInfos { get; set; }
        public virtual ICollection<TGlcdriverShiftInfo> TGlcdriverShiftInfos { get; set; }
    }
}
