using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcdriverShiftInfo:BaseEntity
{
    public int GlcdriverShiftInfoId { get; set; }

    public DateTime GlcdriverShiftDay { get; set; }

    public int GlcdriverShiftDriverId { get; set; }

    public int GlcdriverShiftBeginHour { get; set; }

    public int GlcdriverShiftEndHour { get; set; }

    public int GlcdriverShiftTerminalId { get; set; }

    public virtual TGlcdriverInfo GlcdriverShiftDriver { get; set; } = null!;

    public virtual TGlcterminalInfo GlcdriverShiftTerminal { get; set; } = null!;
}
