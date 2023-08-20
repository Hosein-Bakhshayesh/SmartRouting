using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcdriverShiftInfo:BaseEntity
{
    public int GlcdriverShiftInfoId { get; set; }

    public string GlcdriverShiftDay { get; set; } = null!;

    public int GlcdriverShiftDriverId { get; set; }

    public string GlcdriverShiftBeginHour { get; set; } = null!;

    public string GlcdriverShiftEndHour { get; set; } = null!;

    public int GlcdriverShiftTerminalId { get; set; }

    public virtual TGlcdriverInfo GlcdriverShiftDriver { get; set; } = null!;

    public virtual TGlcterminalInfo GlcdriverShiftTerminal { get; set; } = null!;
}
