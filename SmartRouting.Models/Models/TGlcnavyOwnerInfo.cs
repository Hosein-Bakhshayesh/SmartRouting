using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcnavyOwnerInfo:BaseEntity
{
    public int GlcnavyOwnerId { get; set; }

    public string GlcnavyOwnerName { get; set; } = null!;
}
