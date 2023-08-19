using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcdriverInfo:BaseEntity
{
    public int GlcdriverInfoId { get; set; }

    public string GlcdriverNationalCode { get; set; } = null!;

    public string GlcdriverName { get; set; } = null!;

    public string GlcdriverLastName { get; set; } = null!;

    public string GlcdriverMobile { get; set; } = null!;

    public string? GlcdriverPhotoPath { get; set; }

    public int GlcdriverNavyInfoId { get; set; }

    public virtual TGlcnavyInfo GlcdriverNavyInfo { get; set; } = null!;

    public virtual ICollection<TGlcdossierDetailInfo> TGlcdossierDetailInfos { get; set; } = new List<TGlcdossierDetailInfo>();

    public virtual ICollection<TGlcdriverShiftInfo> TGlcdriverShiftInfos { get; set; } = new List<TGlcdriverShiftInfo>();
}
