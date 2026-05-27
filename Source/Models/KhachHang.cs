using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public int? MaVaiTro { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual VaiTro? MaVaiTroNavigation { get; set; }
}
