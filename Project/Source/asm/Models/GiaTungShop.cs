using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class GiaTungShop
{
    public int MaGts { get; set; }

    public int MaSp { get; set; }

    public string TenShop { get; set; } = null!;

    public decimal Gia { get; set; }

    public string? Link { get; set; }

    public DateTime NgayCapNhat { get; set; }

    public bool ConHang { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
