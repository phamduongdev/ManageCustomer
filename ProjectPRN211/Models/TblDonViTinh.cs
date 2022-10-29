using System;
using System.Collections.Generic;

namespace ProjectPRN211.Models
{
    public partial class TblDonViTinh
    {
        public TblDonViTinh()
        {
            TblMatHangs = new HashSet<TblMatHang>();
        }

        public string Dvt { get; set; } = null!;

        public virtual ICollection<TblMatHang> TblMatHangs { get; set; }
    }
}
