using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class DanhGium
{
    public int MaDanhGia { get; set; }

    public int? MaSanPham { get; set; }

    public int? MaKhachHang { get; set; }

    public int? SoSao { get; set; }

    public string? BinhLuan { get; set; }

    public DateTime? NgayDanhGia { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
