using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class AnhSanPham
{
    public int MaAnh { get; set; }

    public int MaSp { get; set; }

    public string DuongDan { get; set; } = null!;

    public bool LaAnhChinh { get; set; }

    public int ThuTu { get; set; }

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
