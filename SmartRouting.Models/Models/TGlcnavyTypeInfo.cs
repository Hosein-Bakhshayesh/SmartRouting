using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcnavyTypeInfo:BaseEntity
{
    public int GlcnavyTypeInfoId { get; set; }

    public string GlcnavyTypeName { get; set; } = null!;

    public double GlcnavyTypeLength { get; set; }

    public double GlcnavyTypeWidth { get; set; }

    public double GlcnavyTypeHeight { get; set; }

    public int GlcnavyTypeFuel { get; set; }

    public int GlcnavyTypeModel { get; set; }

    public virtual ICollection<TGlcnavyInfo> TGlcnavyInfos { get; set; } = new List<TGlcnavyInfo>();
}
