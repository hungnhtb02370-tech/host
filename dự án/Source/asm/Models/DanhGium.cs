using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class DanhGium
{
    public int MaDg { get; set; }

    public int MaKh { get; set; }

    public int MaSp { get; set; }

    public int? MaHd { get; set; }

    public int SoSao { get; set; }

    public string? BinhLuan { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime NgayDanhGia { get; set; }

    public virtual HoaDon? MaHdNavigation { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
