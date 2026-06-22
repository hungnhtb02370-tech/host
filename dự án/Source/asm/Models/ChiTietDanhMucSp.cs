using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChiTietDanhMucSp
{
    public int MaCtdm { get; set; }

    public int MaDanhMuc { get; set; }

    public int MaSp { get; set; }

    public virtual DanhMuc MaDanhMucNavigation { get; set; } = null!;

    public virtual SanPham MaSpNavigation { get; set; } = null!;
}
