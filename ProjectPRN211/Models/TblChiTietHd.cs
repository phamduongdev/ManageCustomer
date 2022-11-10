namespace ProjectPRN211.Models
{
    public partial class TblChiTietHd
    {
        public decimal MaChiTietHd { get; set; }
        public decimal MaHd { get; set; }
        public string MaHang { get; set; } = null!;
        public int Soluong { get; set; }

        public virtual TblMatHang MaHangNavigation { get; set; } = null!;
        public virtual TblHoaDon MaHdNavigation { get; set; } = null!;
    }
}
