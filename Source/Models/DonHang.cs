using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class DonHang
{
    public int MaDonHang { get; set; }

    public int? MaKhachHang { get; set; }

    public DateTime? NgayDatHang { get; set; }

    public decimal? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual KhachHang? MaKhachHangNavigation { get; set; }

    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();

    public virtual ICollection<VanChuyen> VanChuyens { get; set; } = new List<VanChuyen>();
}
