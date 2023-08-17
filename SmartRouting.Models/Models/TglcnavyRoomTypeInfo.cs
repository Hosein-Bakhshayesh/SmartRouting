using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcnavyRoomTypeInfo : BaseEntity
{
    public int GlcnavyRoomTypeId { get; set; }

    public string GlcnavyRoomTypeName { get; set; } = null!;
}
