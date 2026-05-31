using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class PhieuNhap
{
    public int MaPhieuNhap { get; set; }

    public int MaNhaCungCap { get; set; }

    public DateTime? NgayNhap { get; set; }

    public decimal? TongTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual NhaCungCap MaNhaCungCapNavigation { get; set; } = null!;
}
