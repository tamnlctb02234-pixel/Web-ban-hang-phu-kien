using System;
using System.Collections.Generic;

namespace ASM1_SOF1022.Models;

public partial class VaiTro
{
    public int MaVaiTro { get; set; }

    public string TenVaiTro { get; set; } = null!;

    public virtual ICollection<KhachHang> KhachHangs { get; set; } = new List<KhachHang>();
}
