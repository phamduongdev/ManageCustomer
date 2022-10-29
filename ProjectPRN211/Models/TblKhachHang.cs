using System;
using System.Collections.Generic;

namespace ProjectPRN211.Models
{
    public partial class TblKhachHang
    {
        public TblKhachHang()
        {
            TblHoaDons = new HashSet<TblHoaDon>();
        }

        public string MaKh { get; set; } = null!;
        public string? TenKh { get; set; }
        public bool? Gt { get; set; }
        public string? Diachi { get; set; }
        public DateTime? NgaySinh { get; set; }

        public virtual ICollection<TblHoaDon> TblHoaDons { get; set; }
    }
}
