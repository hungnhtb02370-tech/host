using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class DanhMuc
{
    public int MaDanhMuc { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string? MoTa { get; set; }

    public string Slug { get; set; } = null!;

    public string? Icon { get; set; }

    public int ThuTu { get; set; }

    public virtual ICollection<ChiTietDanhMucSp> ChiTietDanhMucSps { get; set; } = new List<ChiTietDanhMucSp>();

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();

    public virtual ICollection<TuongThich> TuongThichMaDanhMucANavigations { get; set; } = new List<TuongThich>();

    public virtual ICollection<TuongThich> TuongThichMaDanhMucBNavigations { get; set; } = new List<TuongThich>();
}
