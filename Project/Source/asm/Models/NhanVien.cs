using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class NhanVien
{
    public int MaNv { get; set; }

    public string HoTen { get; set; } = null!;

    public string? HinhAnh { get; set; }

    public string? GioiTinh { get; set; }

    public decimal? Luong { get; set; }

    public string? Sdt { get; set; }

    public string Email { get; set; } = null!;

    public string? Cccd { get; set; }

    public string? DiaChi { get; set; }

    public string TenTk { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public bool IsAdministrator { get; set; }

    public bool TrangThai { get; set; }

    public string? ChucVu { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<BaoCaoDoanhThu> BaoCaoDoanhThus { get; set; } = new List<BaoCaoDoanhThu>();

    public virtual ICollection<ChiTietChucNang> ChiTietChucNangs { get; set; } = new List<ChiTietChucNang>();

    public virtual ICollection<QuanLySanPham> QuanLySanPhams { get; set; } = new List<QuanLySanPham>();

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
