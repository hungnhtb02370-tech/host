using System;
using System.Collections.Generic;

namespace asm.Models;

public partial class NhaCungCap
{
    public int MaNcc { get; set; }

    public string TenNcc { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? DiaChi { get; set; }

    public bool TrangThai { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
