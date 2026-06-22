using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class ChucNang
{
    public int MaCn { get; set; }

    public string TenChucNang { get; set; } = null!;

    public string? MoTa { get; set; }

    public bool TrangThai { get; set; }

    public virtual ICollection<ChiTietChucNang> ChiTietChucNangs { get; set; } = new List<ChiTietChucNang>();
}
