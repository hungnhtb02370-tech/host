using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class KhachHang
{
    public int MaKh { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? Sdt { get; set; }

    public string? Cccd { get; set; }

    public string? GioiTinh { get; set; }

    public DateTime NgayTao { get; set; }

    public bool TrangThai { get; set; }

    public virtual ICollection<Build> Builds { get; set; } = new List<Build>();

    public virtual ICollection<ChiTietThongBao> ChiTietThongBaos { get; set; } = new List<ChiTietThongBao>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DiaChi> DiaChis { get; set; } = new List<DiaChi>();

    public virtual GioHang? GioHang { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual ICollection<ThongBao> ThongBaos { get; set; } = new List<ThongBao>();
}
