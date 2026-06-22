using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class Benchmark
{
    public int MaBm { get; set; }

    public int MaSp { get; set; }

    public string LoaiBm { get; set; } = null!;

    public string? CongCuTest { get; set; }

    public decimal? DiemSo { get; set; }

    public DateTime NgayTest { get; set; }

    public string? GhiChu { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
