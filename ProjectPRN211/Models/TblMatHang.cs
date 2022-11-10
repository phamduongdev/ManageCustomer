namespace ProjectPRN211.Models
{
    public partial class TblMatHang
    {
        public TblMatHang()
        {
            TblChiTietHds = new HashSet<TblChiTietHd>();
        }

        public string MaHang { get; set; } = null!;
        public string TenHang { get; set; } = null!;
        public string Dvt { get; set; } = null!;
        public float Gia { get; set; }
        public int CategoryId { get; set; }

        public virtual TblCategory Category { get; set; } = null!;
        public virtual TblDonViTinh DvtNavigation { get; set; } = null!;
        public virtual ICollection<TblChiTietHd> TblChiTietHds { get; set; }
    }
}
