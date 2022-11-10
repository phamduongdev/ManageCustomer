namespace ProjectPRN211.Models
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            TblMatHangs = new HashSet<TblMatHang>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual ICollection<TblMatHang> TblMatHangs { get; set; }
    }
}
