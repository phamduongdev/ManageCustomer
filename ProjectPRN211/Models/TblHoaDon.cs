using System;
using System.Collections.Generic;

namespace ProjectPRN211.Models
{
    public partial class TblHoaDon
    {
        public TblHoaDon()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public decimal MaHd { get; set; }
        public string? MaKh { get; set; }
        public DateTime? NgayHd { get; set; }

        public virtual TblKhachHang? MaKhNavigation { get; set; }
        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
