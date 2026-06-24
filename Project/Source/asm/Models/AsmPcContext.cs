using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace asm.Models;

public partial class AsmPcContext : DbContext
{
    public AsmPcContext()
    {
    }

    public AsmPcContext(DbContextOptions<AsmPcContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnhSanPham> AnhSanPhams { get; set; }

    public virtual DbSet<BaoCaoDoanhThu> BaoCaoDoanhThus { get; set; }

    public virtual DbSet<Benchmark> Benchmarks { get; set; }

    public virtual DbSet<Build> Builds { get; set; }

    public virtual DbSet<ChiTietBuild> ChiTietBuilds { get; set; }

    public virtual DbSet<ChiTietChucNang> ChiTietChucNangs { get; set; }

    public virtual DbSet<ChiTietDanhMucSp> ChiTietDanhMucSps { get; set; }

    public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<ChiTietThongBao> ChiTietThongBaos { get; set; }

    public virtual DbSet<ChucNang> ChucNangs { get; set; }

    public virtual DbSet<DanhGium> DanhGia { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<DiaChi> DiaChis { get; set; }

    public virtual DbSet<GiaTungShop> GiaTungShops { get; set; }

    public virtual DbSet<GioHang> GioHangs { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<QuanLySanPham> QuanLySanPhams { get; set; }

    public virtual DbSet<SanPham> SanPhams { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<ThongBao> ThongBaos { get; set; }

    public virtual DbSet<TuongThich> TuongThiches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=asm_pc;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnhSanPham>(entity =>
        {
            entity.HasKey(e => e.MaAnh).HasName("PK__AnhSanPh__356240DF82D123E2");

            entity.ToTable("AnhSanPham");

            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.AnhSanPhams)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__AnhSanPham__MaSP__07C12930");
        });

        modelBuilder.Entity<BaoCaoDoanhThu>(entity =>
        {
            entity.HasKey(e => e.MaBaoCao).HasName("PK__BaoCaoDo__25A9188CD8CD6738");

            entity.ToTable("BaoCaoDoanhThu");

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayBatDau).HasColumnType("datetime");
            entity.Property(e => e.NgayKetThuc).HasColumnType("datetime");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongDoanhThu).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.BaoCaoDoanhThus)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaoCaoDoan__MaNV__3D2915A8");
        });

        modelBuilder.Entity<Benchmark>(entity =>
        {
            entity.HasKey(e => e.MaBm).HasName("PK__Benchmar__272475AC9DBBE8BB");

            entity.ToTable("Benchmark");

            entity.Property(e => e.MaBm).HasColumnName("MaBM");
            entity.Property(e => e.CongCuTest).HasMaxLength(100);
            entity.Property(e => e.DiemSo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.LoaiBm)
                .HasMaxLength(50)
                .HasColumnName("LoaiBM");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayTest)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.Benchmarks)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__Benchmark__MaSP__29221CFB");
        });

        modelBuilder.Entity<Build>(entity =>
        {
            entity.HasKey(e => e.MaBuild).HasName("PK__Build__B4A44200BA6BCFDE");

            entity.ToTable("Build");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TenBuild).HasMaxLength(200);
            entity.Property(e => e.TongGia).HasColumnType("decimal(15, 2)");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.Builds)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__Build__MaKH__1BC821DD");
        });

        modelBuilder.Entity<ChiTietBuild>(entity =>
        {
            entity.HasKey(e => e.MaCtb).HasName("PK__ChiTietB__3DCB540B2BDEA9D3");

            entity.ToTable("ChiTietBuild");

            entity.HasIndex(e => new { e.MaBuild, e.MaSp }, "UQ__ChiTietB__66D61280DA33EC1D").IsUnique();

            entity.Property(e => e.MaCtb).HasColumnName("MaCTB");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.MaBuildNavigation).WithMany(p => p.ChiTietBuilds)
                .HasForeignKey(d => d.MaBuild)
                .HasConstraintName("FK__ChiTietBu__MaBui__245D67DE");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietBuilds)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietBui__MaSP__25518C17");
        });

        modelBuilder.Entity<ChiTietChucNang>(entity =>
        {
            entity.HasKey(e => e.MaCtcn).HasName("PK__ChiTietC__1E4E48DFCE9F3468");

            entity.ToTable("ChiTietChucNang");

            entity.HasIndex(e => new { e.MaNv, e.MaCn }, "UQ__ChiTietC__D5578FEB6124A8D3").IsUnique();

            entity.Property(e => e.MaCtcn).HasColumnName("MaCTCN");
            entity.Property(e => e.MaCn).HasColumnName("MaCN");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayCapQuyen)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);

            entity.HasOne(d => d.MaCnNavigation).WithMany(p => p.ChiTietChucNangs)
                .HasForeignKey(d => d.MaCn)
                .HasConstraintName("FK__ChiTietChu__MaCN__693CA210");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ChiTietChucNangs)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__ChiTietChu__MaNV__68487DD7");
        });

        modelBuilder.Entity<ChiTietDanhMucSp>(entity =>
        {
            entity.HasKey(e => e.MaCtdm).HasName("PK__ChiTietD__1E4E40FF7CE0C26A");

            entity.ToTable("ChiTietDanhMucSP");

            entity.HasIndex(e => new { e.MaDanhMuc, e.MaSp }, "UQ__ChiTietD__61075807B0AB6E5B").IsUnique();

            entity.Property(e => e.MaCtdm).HasColumnName("MaCTDM");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.ChiTietDanhMucSps)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietDa__MaDan__0D7A0286");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietDanhMucSps)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__ChiTietDan__MaSP__0E6E26BF");
        });

        modelBuilder.Entity<ChiTietGioHang>(entity =>
        {
            entity.HasKey(e => e.MaCtgh).HasName("PK__ChiTietG__1E4FAF542E60E9FE");

            entity.ToTable("ChiTietGioHang");

            entity.HasIndex(e => new { e.MaGh, e.MaSp }, "UQ__ChiTietG__F557FE05DF855DED").IsUnique();

            entity.Property(e => e.MaCtgh).HasColumnName("MaCTGH");
            entity.Property(e => e.DonGia).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.MaGh).HasColumnName("MaGH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(26, 2)");

            entity.HasOne(d => d.MaGhNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaGh)
                .HasConstraintName("FK__ChiTietGio__MaGH__37703C52");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietGioHangs)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietGio__MaSP__3864608B");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaCthd).HasName("PK__ChiTietH__1E4FA7710E7E44A4");

            entity.ToTable("ChiTietHoaDon");

            entity.HasIndex(e => new { e.MaHd, e.MaSp }, "UQ__ChiTietH__F557F660137C9172").IsUnique();

            entity.Property(e => e.MaCthd).HasColumnName("MaCTHD");
            entity.Property(e => e.DonGia).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([SoLuong]*[DonGia])", true)
                .HasColumnType("decimal(26, 2)");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK__ChiTietHoa__MaHD__4B7734FF");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHoa__MaSP__4C6B5938");
        });

        modelBuilder.Entity<ChiTietThongBao>(entity =>
        {
            entity.HasKey(e => e.MaCttb).HasName("PK__ChiTietT__1E4FC51EF774303B");

            entity.ToTable("ChiTietThongBao");

            entity.Property(e => e.MaCttb).HasColumnName("MaCTTB");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaTb).HasColumnName("MaTB");
            entity.Property(e => e.NgayNhan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.ChiTietThongBaos)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietTho__MaKH__625A9A57");

            entity.HasOne(d => d.MaTbNavigation).WithMany(p => p.ChiTietThongBaos)
                .HasForeignKey(d => d.MaTb)
                .HasConstraintName("FK__ChiTietTho__MaTB__6166761E");
        });

        modelBuilder.Entity<ChucNang>(entity =>
        {
            entity.HasKey(e => e.MaCn).HasName("PK__ChucNang__27258E0E9456497D");

            entity.ToTable("ChucNang");

            entity.Property(e => e.MaCn).HasColumnName("MaCN");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenChucNang).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<DanhGium>(entity =>
        {
            entity.HasKey(e => e.MaDg).HasName("PK__DanhGia__2725866018BBB931");

            entity.HasIndex(e => new { e.MaKh, e.MaSp }, "UQ__DanhGia__F5579F9E508AD176").IsUnique();

            entity.Property(e => e.MaDg).HasColumnName("MaDG");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayDanhGia)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK__DanhGia__MaHD__56E8E7AB");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaKH__55009F39");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.DanhGia)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DanhGia__MaSP__55F4C372");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhMuc).HasName("PK__DanhMuc__B3750887B2A58642");

            entity.ToTable("DanhMuc");

            entity.HasIndex(e => e.Slug, "UQ__DanhMuc__BC7B5FB6D8AF0494").IsUnique();

            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.Slug).HasMaxLength(100);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DiaChi>(entity =>
        {
            entity.HasKey(e => e.MaDc).HasName("PK__DiaChi__27258664E4123622");

            entity.ToTable("DiaChi");

            entity.Property(e => e.MaDc).HasColumnName("MaDC");
            entity.Property(e => e.DiaChiDay).HasMaxLength(255);
            entity.Property(e => e.HoTenNguoiNhan).HasMaxLength(100);
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.PhuongXa).HasMaxLength(100);
            entity.Property(e => e.QuanHuyen).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.TinhThanh).HasMaxLength(100);

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DiaChis)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__DiaChi__MaKH__73BA3083");
        });

        modelBuilder.Entity<GiaTungShop>(entity =>
        {
            entity.HasKey(e => e.MaGts).HasName("PK__GiaTungS__3CD364FEFF449073");

            entity.ToTable("GiaTungShop");

            entity.Property(e => e.MaGts).HasColumnName("MaGTS");
            entity.Property(e => e.ConHang).HasDefaultValue(true);
            entity.Property(e => e.Gia).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Link).HasMaxLength(500);
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayCapNhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TenShop).HasMaxLength(100);

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.GiaTungShops)
                .HasForeignKey(d => d.MaSp)
                .HasConstraintName("FK__GiaTungSho__MaSP__2CF2ADDF");
        });

        modelBuilder.Entity<GioHang>(entity =>
        {
            entity.HasKey(e => e.MaGh).HasName("PK__GioHang__2725AE85C91B5164");

            entity.ToTable("GioHang");

            entity.HasIndex(e => e.MaKh, "UQ__GioHang__2725CF1F43B5B440").IsUnique();

            entity.Property(e => e.MaGh).HasColumnName("MaGH");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MaKhNavigation).WithOne(p => p.GioHang)
                .HasForeignKey<GioHang>(d => d.MaKh)
                .HasConstraintName("FK__GioHang__MaKH__32AB8735");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHd).HasName("PK__HoaDon__2725A6E05B02752E");

            entity.ToTable("HoaDon");

            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.MaDc).HasColumnName("MaDC");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayLap)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TongTien).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ xác nhận");

            entity.HasOne(d => d.MaBaoCaoNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaBaoCao)
                .HasConstraintName("FK__HoaDon__MaBaoCao__43D61337");

            entity.HasOne(d => d.MaDcNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDc)
                .HasConstraintName("FK__HoaDon__MaDC__44CA3770");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDon__MaKH__42E1EEFE");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1E38BE2BFE");

            entity.ToTable("KhachHang");

            entity.HasIndex(e => e.Username, "UQ__KhachHan__536C85E491C1BECE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__KhachHan__A9D105342F6369F2").IsUnique();

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .HasColumnName("CCCD");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.HasKey(e => e.MaKho).HasName("PK__Kho__3BDA935069FFA161");

            entity.ToTable("Kho");

            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.TenKho).HasMaxLength(100);
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__3A185DEB82E6A929");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            entity.Property(e => e.TenNcc)
                .HasMaxLength(100)
                .HasColumnName("TenNCC");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NhanVien__2725D70A5D888752");

            entity.ToTable("NhanVien");

            entity.HasIndex(e => e.TenTk, "UQ__NhanVien__4CF9E777BB23C81B").IsUnique();

            entity.HasIndex(e => e.Cccd, "UQ__NhanVien__A955A0AA72823FCE").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__NhanVien__A9D105345235AC39").IsUnique();

            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.Cccd)
                .HasMaxLength(20)
                .HasColumnName("CCCD");
            entity.Property(e => e.ChucVu).HasMaxLength(50);
            entity.Property(e => e.DiaChi).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.Luong).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Sdt)
                .HasMaxLength(15)
                .HasColumnName("SDT");
            entity.Property(e => e.TenTk)
                .HasMaxLength(50)
                .HasColumnName("TenTK");
            entity.Property(e => e.TrangThai).HasDefaultValue(true);
        });

        modelBuilder.Entity<QuanLySanPham>(entity =>
        {
            entity.HasKey(e => e.MaQl).HasName("PK__QuanLySa__2725F852CB48FE1F");

            entity.ToTable("QuanLySanPham");

            entity.HasIndex(e => new { e.MaNv, e.MaSp }, "UQ__QuanLySa__F557878A56D763F4").IsUnique();

            entity.Property(e => e.MaQl).HasColumnName("MaQL");
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.NgayQuanLy)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.VaiTro).HasMaxLength(50);

            entity.HasOne(d => d.MaKhoNavigation).WithMany(p => p.QuanLySanPhams)
                .HasForeignKey(d => d.MaKho)
                .HasConstraintName("FK__QuanLySan__MaKho__14270015");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.QuanLySanPhams)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuanLySanP__MaNV__123EB7A3");

            entity.HasOne(d => d.MaSpNavigation).WithMany(p => p.QuanLySanPhams)
                .HasForeignKey(d => d.MaSp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__QuanLySanP__MaSP__1332DBDC");
        });

        modelBuilder.Entity<SanPham>(entity =>
        {
            entity.HasKey(e => e.MaSp).HasName("PK__SanPham__2725081CB2331181");

            entity.ToTable("SanPham");

            entity.Property(e => e.MaSp).HasColumnName("MaSP");
            entity.Property(e => e.GiaBan).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.GiaNhap).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.MaNcc).HasColumnName("MaNCC");
            entity.Property(e => e.MaNvquanLy).HasColumnName("MaNVQuanLy");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.TenSp)
                .HasMaxLength(200)
                .HasColumnName("TenSP");
            entity.Property(e => e.ThuongHieu).HasMaxLength(100);
            entity.Property(e => e.TrangThai).HasDefaultValue(true);

            entity.HasOne(d => d.MaDanhMucNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaDanhMuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SanPham__MaDanhM__00200768");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK__SanPham__MaNCC__01142BA1");

            entity.HasOne(d => d.MaNvquanLyNavigation).WithMany(p => p.SanPhams)
                .HasForeignKey(d => d.MaNvquanLy)
                .HasConstraintName("FK__SanPham__MaNVQua__04E4BC85");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaGd).HasName("PK__ThanhToa__2725AE816E54C71C");

            entity.ToTable("ThanhToan");

            entity.Property(e => e.MaGd).HasColumnName("MaGD");
            entity.Property(e => e.MaHd).HasColumnName("MaHD");
            entity.Property(e => e.NgayThanhToan).HasColumnType("datetime");
            entity.Property(e => e.PhuongThuc).HasMaxLength(50);
            entity.Property(e => e.SoTien).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Chờ thanh toán");

            entity.HasOne(d => d.MaHdNavigation).WithMany(p => p.ThanhToans)
                .HasForeignKey(d => d.MaHd)
                .HasConstraintName("FK__ThanhToan__MaHD__503BEA1C");
        });

        modelBuilder.Entity<ThongBao>(entity =>
        {
            entity.HasKey(e => e.MaTb).HasName("PK__ThongBao__2725006F950812E7");

            entity.ToTable("ThongBao");

            entity.Property(e => e.MaTb).HasColumnName("MaTB");
            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.MaNv).HasColumnName("MaNV");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NoiDung).HasMaxLength(500);
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Chưa đọc");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.MaKh)
                .HasConstraintName("FK__ThongBao__MaKH__5BAD9CC8");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.ThongBaos)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__ThongBao__MaNV__5CA1C101");
        });

        modelBuilder.Entity<TuongThich>(entity =>
        {
            entity.HasKey(e => e.MaTt).HasName("PK__TuongThi__27250079E4C91C81");

            entity.ToTable("TuongThich");

            entity.Property(e => e.MaTt).HasColumnName("MaTT");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.ThuocTinhA).HasMaxLength(100);
            entity.Property(e => e.ThuocTinhB).HasMaxLength(100);

            entity.HasOne(d => d.MaDanhMucANavigation).WithMany(p => p.TuongThichMaDanhMucANavigations)
                .HasForeignKey(d => d.MaDanhMucA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TuongThic__MaDan__17F790F9");

            entity.HasOne(d => d.MaDanhMucBNavigation).WithMany(p => p.TuongThichMaDanhMucBNavigations)
                .HasForeignKey(d => d.MaDanhMucB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TuongThic__MaDan__18EBB532");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
