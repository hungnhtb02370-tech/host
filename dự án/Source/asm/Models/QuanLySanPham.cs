using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class QuanLySanPham
{
    public int MaQl { get; set; }

    public int MaNv { get; set; }

    public int MaSp { get; set; }

    public int? MaKho { get; set; }

    public DateTime NgayQuanLy { get; set; }

    public string? VaiTro { get; set; }

    public string? GhiChu { get; set; }

    public virtual Kho? MaKhoNavigation { get; set; }

    public virtual NhanVien MaNvNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
