using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChiTietBuild
{
    public int MaCtb { get; set; }

    public int MaBuild { get; set; }

    public int MaSp { get; set; }

    public int SoLuong { get; set; }

    public string? GhiChu { get; set; }

    public virtual Build MaBuildNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
