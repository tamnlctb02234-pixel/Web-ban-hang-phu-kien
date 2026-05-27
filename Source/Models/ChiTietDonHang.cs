using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class ChiTietDonHang
{
    public int MaChiTietDonHang { get; set; }

    public int? MaDonHang { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuong { get; set; }

    public decimal? Gia { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
