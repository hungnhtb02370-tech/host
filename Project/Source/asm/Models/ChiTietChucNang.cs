using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChiTietChucNang
{
    public int MaCtcn { get; set; }

    public int MaNv { get; set; }

    public int MaCn { get; set; }

    public DateTime NgayCapQuyen { get; set; }

    public bool TrangThai { get; set; }

    public virtual ChucNang MaCnNavigation { get; set; } = null!;

    public virtual NhanVien MaNvNavigation { get; set; } = null!;
}
