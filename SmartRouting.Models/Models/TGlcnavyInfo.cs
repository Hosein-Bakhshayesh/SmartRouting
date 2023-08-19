using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcnavyInfo:BaseEntity
{
    public int GlcnavyInfoId { get; set; }

    public int GlcnavyType { get; set; }

    public int GlcnavyPelakIran { get; set; }

    public int GlcnavyPelak2 { get; set; }

    public string GlcnavyPelakChar { get; set; } = null!;

    public int GlcnavyPelak3 { get; set; }

    public int GlcnavyOwnerType { get; set; }

    public virtual TGlcnavyOwnerInfo GlcnavyOwnerTypeNavigation { get; set; } = null!;

    public virtual TGlcnavyTypeInfo GlcnavyTypeNavigation { get; set; } = null!;

    public virtual ICollection<TGlcdriverInfo> TGlcdriverInfos { get; set; } = new List<TGlcdriverInfo>();
}
