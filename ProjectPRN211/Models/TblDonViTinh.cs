namespace ProjectPRN211.Models
{
    public partial class TblDonViTinh
    {
        public TblDonViTinh()
        {
            TblCarts = new HashSet<TblCart>();
            TblMatHangs = new HashSet<TblMatHang>();
        }

        public string Dvt { get; set; } = null!;

        public virtual ICollection<TblCart> TblCarts { get; set; }
        public virtual ICollection<TblMatHang> TblMatHangs { get; set; }
    }
}
