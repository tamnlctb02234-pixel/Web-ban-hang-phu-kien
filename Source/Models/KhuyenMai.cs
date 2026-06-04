using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM1_SOF1022.Models;

public partial class KhuyenMai
{
    public int MaKhuyenMai { get; set; }

    [Required(ErrorMessage = "Tên khuyến mãi không được để trống")]
    public string? TenKhuyenMai { get; set; }

    [Required(ErrorMessage = "Phần trăm giảm không được để trống")]
    [Range(1, 100, ErrorMessage = "Phần trăm giảm phải từ 1 đến 100")]
    public int? PhanTramGiam { get; set; }

    [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
    public DateOnly? NgayBatDau { get; set; }

    [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
    public DateOnly? NgayKetThuc { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
