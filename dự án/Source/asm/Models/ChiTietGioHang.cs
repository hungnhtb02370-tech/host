using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChiTietGioHang
{
    public int MaCtgh { get; set; }

    public int MaGh { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual GioHang MaGhNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
