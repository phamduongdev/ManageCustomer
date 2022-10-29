using Microsoft.EntityFrameworkCore;

namespace ProjectPRN211.Models
{
    public partial class MyOrderContext : DbContext
    {
        public MyOrderContext()
        {
        }

        public MyOrderContext(DbContextOptions<MyOrderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblChiTietHd> TblChiTietHds { get; set; } = null!;
        public virtual DbSet<TblDonViTinh> TblDonViTinhs { get; set; } = null!;
        public virtual DbSet<TblHoaDon> TblHoaDons { get; set; } = null!;
        public virtual DbSet<TblKhachHang> TblKhachHangs { get; set; } = null!;
        public virtual DbSet<TblMatHang> TblMatHangs { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblChiTietHd>(entity =>
            {
                entity.HasKey(e => e.MaChiTietHd)
                    .HasName("PK__tblChiTi__651E49EB7363C4B6");

                entity.ToTable("tblChiTietHD");

                entity.Property(e => e.MaChiTietHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaChiTietHD");

                entity.Property(e => e.MaHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MaHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaHD");

                entity.HasOne(d => d.MaHangNavigation)
                    .WithMany(p => p.TblChiTietHds)
                    .HasForeignKey(d => d.MaHang)
                    .HasConstraintName("FK__tblChiTie__MaHan__300424B4");

                entity.HasOne(d => d.MaHdNavigation)
                    .WithMany(p => p.TblChiTietHds)
                    .HasForeignKey(d => d.MaHd)
                    .HasConstraintName("FK__tblChiTie__Soluo__2F10007B");
            });

            modelBuilder.Entity<TblDonViTinh>(entity =>
            {
                entity.HasKey(e => e.Dvt)
                    .HasName("PK__tblDonVi__C030C78793FDF041");

                entity.ToTable("tblDonViTinh");

                entity.Property(e => e.Dvt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DVT");
            });

            modelBuilder.Entity<TblHoaDon>(entity =>
            {
                entity.HasKey(e => e.MaHd)
                    .HasName("PK__tblHoaDo__2725A6E0339581E8");

                entity.ToTable("tblHoaDon");

                entity.Property(e => e.MaHd)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("MaHD");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.NgayHd)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("NgayHD");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.TblHoaDons)
                    .HasForeignKey(d => d.MaKh)
                    .HasConstraintName("FK__tblHoaDon__NgayH__276EDEB3");
            });

            modelBuilder.Entity<TblKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__tblKhach__2725CF1ECF77828E");

                entity.ToTable("tblKhachHang");

                entity.Property(e => e.MaKh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("MaKH");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Gt).HasColumnName("GT");

                entity.Property(e => e.NgaySinh).HasColumnType("smalldatetime");

                entity.Property(e => e.TenKh)
                    .HasMaxLength(50)
                    .HasColumnName("TenKH");
            });

            modelBuilder.Entity<TblMatHang>(entity =>
            {
                entity.HasKey(e => e.MaHang)
                    .HasName("PK__tblMatHa__19C0DB1D939676B5");

                entity.ToTable("tblMatHang");

                entity.Property(e => e.MaHang)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Dvt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DVT");

                entity.Property(e => e.TenHang).HasMaxLength(50);

                entity.HasOne(d => d.DvtNavigation)
                    .WithMany(p => p.TblMatHangs)
                    .HasForeignKey(d => d.Dvt)
                    .HasConstraintName("FK__tblMatHang__Gia__2C3393D0");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
