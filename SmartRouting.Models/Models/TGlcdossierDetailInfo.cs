using System;
using System.Collections.Generic;

namespace SmartRouting.Models.Models;

public partial class TGlcdossierDetailInfo:BaseEntity
{
    public int GlcdossierDetailInfoId { get; set; }

    public string DossierDetailNumber { get; set; } = null!;

    public string DossierDetailCustomer { get; set; } = null!;

    public string DossierDetailCmobile { get; set; } = null!;

    public string DossierDetailCaddress { get; set; } = null!;

    public double DossierDetailLength { get; set; }

    public double DossierDetailWidth { get; set; }

    public string DossierDetailProduct { get; set; } = null!;

    public int DossierDetailQuantity { get; set; }

    public int DossierDetailHour { get; set; }

    public string DossierDetailType { get; set; } = null!;

    public int DossierDetailDriverId { get; set; }

    public virtual TGlcdriverInfo DossierDetailDriver { get; set; } = null!;
}
