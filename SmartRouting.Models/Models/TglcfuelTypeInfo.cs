using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcfuelTypeInfo:BaseEntity
{
    public int GlcfuelTypeId { get; set; }

    public string GlcfurlTypeName { get; set; } = null!;
}
