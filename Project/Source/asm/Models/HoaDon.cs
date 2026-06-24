using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class HoaDon
{
    public int MaHd { get; set; }

    public int MaKh { get; set; }

    public int? MaBaoCao { get; set; }

    public int? MaDc { get; set; }

    public DateTime NgayLap { get; set; }

    public decimal TongTien { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual BaoCaoDoanhThu? MaBaoCaoNavigation { get; set; }

    public virtual DiaChi? MaDcNavigation { get; set; }

    public virtual KhachHang MaKhNavigation { get; set; } = null!;

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
