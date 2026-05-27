using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    public string? TenKhuyenMai { get; set; }

    public int? PhanTramGiam { get; set; }

    public DateOnly? NgayBatDau { get; set; }

    public DateOnly? NgayKetThuc { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
