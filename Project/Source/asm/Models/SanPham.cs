using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class SanPham
{
    public int MaSp { get; set; }

    public int MaDanhMuc { get; set; }

    public int? MaNcc { get; set; }

    public string TenSp { get; set; } = null!;

    public string? ThuongHieu { get; set; }

    public decimal GiaBan { get; set; }

    public decimal? GiaNhap { get; set; }

    public DateTime? NgayNhap { get; set; }

    public int SoLuongTon { get; set; }

    public bool TrangThai { get; set; }

    public string? ThongSoKyThuat { get; set; }

    public string? MoTa { get; set; }

    public int? MaNvquanLy { get; set; }

    public virtual ICollection<AnhSanPham> AnhSanPhams { get; set; } = new List<AnhSanPham>();

    public virtual ICollection<Benchmark> Benchmarks { get; set; } = new List<Benchmark>();

    public virtual ICollection<ChiTietBuild> ChiTietBuilds { get; set; } = new List<ChiTietBuild>();

    public virtual ICollection<ChiTietDanhMucSp> ChiTietDanhMucSps { get; set; } = new List<ChiTietDanhMucSp>();

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; } = new List<ChiTietGioHang>();

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<GiaTungShop> GiaTungShops { get; set; } = new List<GiaTungShop>();

    public virtual DanhMuc MaDanhMucNavigation { get; set; } = null!;

    public virtual NhaCungCap? MaNccNavigation { get; set; }

    public virtual NhanVien? MaNvquanLyNavigation { get; set; }

    public virtual ICollection<QuanLySanPham> QuanLySanPhams { get; set; } = new List<QuanLySanPham>();
}
