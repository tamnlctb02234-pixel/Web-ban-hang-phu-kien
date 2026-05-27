using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class ThanhToan
{
    public int MaThanhToan { get; set; }

    public int? MaDonHang { get; set; }

    public string? PhuongThucThanhToan { get; set; }

    public string? TrangThaiThanhToan { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
