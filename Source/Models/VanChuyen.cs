using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class VanChuyen
{
    public int MaVanChuyen { get; set; }

    public int? MaDonHang { get; set; }

    public string? DiaChiGiaoHang { get; set; }

    public string? TrangThaiVanChuyen { get; set; }

    public DateTime? NgayVanChuyen { get; set; }

    public virtual DonHang? MaDonHangNavigation { get; set; }
}
