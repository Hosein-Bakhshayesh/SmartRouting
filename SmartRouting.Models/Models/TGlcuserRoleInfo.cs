using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcuserRoleInfo:BaseEntity
{
    public int GlcuserRoleInfoId { get; set; }

    public string GlcuserRoleName { get; set; } = null!;

    public virtual ICollection<TGlcusersInfo> TGlcusersInfos { get; set; } = new List<TGlcusersInfo>();
}
