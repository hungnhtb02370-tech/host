using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChiTietThongBao
{
    public int MaCttb { get; set; }

    public int MaTb { get; set; }

    public int MaKh { get; set; }

    public bool DaXem { get; set; }

    public DateTime NgayNhan { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual ThongBao MaTbNavigation { get; set; } = null!;
}
