using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class ChiTietPhieuNhap
{
    public int MaChiTietNhap { get; set; }

    public int MaPhieuNhap { get; set; }

    public int MaSanPham { get; set; }

    public int SoLuong { get; set; }

    public decimal GiaNhap { get; set; }

    public virtual PhieuNhap MaPhieuNhapNavigation { get; set; } = null!;

    public virtual SanPham MaSanPhamNavigation { get; set; } = null!;
}
