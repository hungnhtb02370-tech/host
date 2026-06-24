using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class Build
{
    public int MaBuild { get; set; }

    public int MaKh { get; set; }

    public string TenBuild { get; set; } = null!;

    public string? MoTa { get; set; }

    public decimal TongGia { get; set; }

    public bool CongKhai { get; set; }

    public int? TongWatt { get; set; }

    public int LuotXem { get; set; }

    public int LuotThich { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<ChiTietBuild> ChiTietBuilds { get; set; } = new List<ChiTietBuild>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
