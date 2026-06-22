using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class DiaChi
{
    public int MaDc { get; set; }

    public int MaKh { get; set; }

    public string? HoTenNguoiNhan { get; set; }

    public string? Sdt { get; set; }

    public string DiaChiDay { get; set; } = null!;

    public string? TinhThanh { get; set; }

    public string? QuanHuyen { get; set; }

    public string? PhuongXa { get; set; }

    public bool MacDinh { get; set; }

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual KhachHang MaKhNavigation { get; set; } = null!;
}
