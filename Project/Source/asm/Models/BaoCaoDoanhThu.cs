using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class BaoCaoDoanhThu
{
    public int MaBaoCao { get; set; }

    public int MaNv { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public decimal TongDoanhThu { get; set; }

    public int TongDonHang { get; set; }

    public DateTime NgayTao { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
