using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM1_SOF1022.Models;

public partial class NhaCungCap
{
    public int MaNhaCungCap { get; set; }

    public string TenNhaCungCap { get; set; } = null!;

    [RegularExpression(@"^[0-9]+$",ErrorMessage ="Số điện thoại chỉ được chứa kí tự số")]
    [StringLength(11,MinimumLength =10,ErrorMessage ="Số điện thoại phải từ 10 đến 11 ký tự")]
    public string? SoDienThoai { get; set; }

    [Required(ErrorMessage ="Vui lòng nhập địa chỉ của bạn")]
    [StringLength(255,ErrorMessage ="Địa chỉ không được vượt quá 255 ký tự")]
    public string? DiaChi { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
