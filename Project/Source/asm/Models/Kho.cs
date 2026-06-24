using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class Kho
{
    public int MaKho { get; set; }

    public string TenKho { get; set; } = null!;

    public string? DiaChi { get; set; }

    public int? SucChua { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<QuanLySanPham> QuanLySanPhams { get; set; } = new List<QuanLySanPham>();
}
