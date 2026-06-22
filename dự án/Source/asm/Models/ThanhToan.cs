using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ThanhToan
{
    public int MaGd { get; set; }

    public int MaHd { get; set; }

    public string PhuongThuc { get; set; } = null!;

    public decimal SoTien { get; set; }

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayThanhToan { get; set; }

    public virtual HoaDon MaHdNavigation { get; set; } = null!;
}
