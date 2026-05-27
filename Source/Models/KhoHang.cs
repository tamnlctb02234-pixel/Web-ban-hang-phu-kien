using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class KhoHang
{
    public int MaKho { get; set; }

    public int? MaSanPham { get; set; }

    public int? SoLuongTon { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual SanPham? MaSanPhamNavigation { get; set; }
}
