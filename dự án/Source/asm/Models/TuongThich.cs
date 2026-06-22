using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class TuongThich
{
    public int MaTt { get; set; }

    public int MaDanhMucA { get; set; }

    public int MaDanhMucB { get; set; }

    public string ThuocTinhA { get; set; } = null!;

    public string ThuocTinhB { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual DanhMuc MaDanhMucANavigation { get; set; } = null!;

    public virtual DanhMuc MaDanhMucBNavigation { get; set; } = null!;
}
