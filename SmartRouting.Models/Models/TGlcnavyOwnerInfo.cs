using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcnavyOwnerInfo:BaseEntity
{
    public int GlcnavyOwnerId { get; set; }

    public string GlcnavyOwnerName { get; set; } = null!;

    public virtual ICollection<TGlcnavyInfo> TGlcnavyInfos { get; set; } = new List<TGlcnavyInfo>();
}
