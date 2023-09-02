using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcsms:BaseEntity
{
    public int GlcsmsId { get; set; }

    public string GlcsmsMobileNumber { get; set; } = null!;

    public string GlcsmsDiscription { get; set; } = null!;
}
