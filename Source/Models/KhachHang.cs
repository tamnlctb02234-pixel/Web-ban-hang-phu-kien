using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM1_SOF1022.Models;

public partial class KhachHang
{
    public int MaKhachHang { get; set; }

    public string HoTen { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    [StringLength(15,ErrorMessage ="Số điện thoại phải từ 1 đến 15 ký tự")]
    public string? SoDienThoai { get; set; }

    [Required(ErrorMessage ="Vui lòng nhập địa chỉ của bạn")]
    [StringLength(255,ErrorMessage ="Địa chỉ không được quá 255 ký tự")]
    public string? DiaChi { get; set; }

    public int? MaVaiTro { get; set; }

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();

    public virtual VaiTro? MaVaiTroNavigation { get; set; }
}
