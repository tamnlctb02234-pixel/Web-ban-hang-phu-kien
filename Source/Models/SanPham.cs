using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASM1_SOF1022.Models;

public partial class SanPham
{
    public int MaSanPham { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    public string TenSanPham { get; set; } = null!;

    [Range(0, double.MaxValue,
       ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
    public decimal Gia { get; set; }

    [Range(0, int.MaxValue,
     ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0")]
    public int SoLuong { get; set; }

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public int? MaDanhMuc { get; set; }

    public int? MaNhaCungCap { get; set; }

    public int? MaKhuyenMai { get; set; }

    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<DanhGium> DanhGia { get; set; } = new List<DanhGium>();

    public virtual ICollection<KhoHang> KhoHangs { get; set; } = new List<KhoHang>();

    public virtual DanhMuc? MaDanhMucNavigation { get; set; }

    public virtual KhuyenMai? MaKhuyenMaiNavigation { get; set; }

    public virtual NhaCungCap? MaNhaCungCapNavigation { get; set; }
}
