using System;
using System.Collections.Generic;

namespace ProjectPRN211.Models
{
    public partial class TblCart
    {
        public string MaHang { get; set; } = null!;
        public string? TenHang { get; set; }
        public string? Dvt { get; set; }
        public float? Gia { get; set; }
        public int? Soluong { get; set; }

        public virtual TblDonViTinh? DvtNavigation { get; set; }
    }
}
