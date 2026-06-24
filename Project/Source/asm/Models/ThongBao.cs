using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ThongBao
{
    public int MaTb { get; set; }

    public int? MaKh { get; set; }

    public int? MaNv { get; set; }

    public string NoiDung { get; set; } = null!;

    public DateTime NgayTao { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietThongBao> ChiTietThongBaos { get; set; } = new List<ChiTietThongBao>();

    public virtual KhachHang? MaKhNavigation { get; set; }

    public virtual NhanVien? MaNvNavigation { get; set; }
}
