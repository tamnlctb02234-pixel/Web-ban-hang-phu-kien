CREATE DATABASE ShopPhuKienDB
GO

USE ShopPhuKienDB
GO


-- 1. Bảng phân quyền
CREATE TABLE VaiTro
(
    MaVaiTro INT PRIMARY KEY IDENTITY(1,1),
    TenVaiTro NVARCHAR(50) NOT NULL
)


-- 2. Bảng khách hàng
CREATE TABLE KhachHang
(
    MaKhachHang INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(255),

    MaVaiTro INT,

    FOREIGN KEY (MaVaiTro)
        REFERENCES VaiTro(MaVaiTro)
)


-- 3. Bảng danh mục
CREATE TABLE DanhMuc
(
    MaDanhMuc INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc NVARCHAR(100) NOT NULL
)


-- 4. Bảng nhà cung cấp
CREATE TABLE NhaCungCap
(
    MaNhaCungCap INT PRIMARY KEY IDENTITY(1,1),
    TenNhaCungCap NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(20),
    DiaChi NVARCHAR(255)
)


-- 5. Bảng khuyến mãi
CREATE TABLE KhuyenMai
(
    MaKhuyenMai INT PRIMARY KEY IDENTITY(1,1),
    TenKhuyenMai NVARCHAR(100),
    PhanTramGiam INT,
    NgayBatDau DATE,
    NgayKetThuc DATE
)


-- 6. Bảng sản phẩm
CREATE TABLE SanPham
(
    MaSanPham INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(200) NOT NULL,
    Gia DECIMAL(18,2) NOT NULL,
    SoLuong INT NOT NULL,
    MoTa NVARCHAR(MAX),
    HinhAnh NVARCHAR(255),
    MaDanhMuc INT,
    MaNhaCungCap INT,
    MaKhuyenMai INT,

    FOREIGN KEY (MaDanhMuc)
        REFERENCES DanhMuc(MaDanhMuc),

    FOREIGN KEY (MaNhaCungCap)
        REFERENCES NhaCungCap(MaNhaCungCap),

    FOREIGN KEY (MaKhuyenMai)
        REFERENCES KhuyenMai(MaKhuyenMai)
)


-- 7. Bảng đơn hàng
CREATE TABLE DonHang
(
    MaDonHang INT PRIMARY KEY IDENTITY(1,1),

    MaKhachHang INT,

    NgayDatHang DATETIME DEFAULT GETDATE(),

    TongTien DECIMAL(18,2),

    TrangThai NVARCHAR(50),

    FOREIGN KEY (MaKhachHang)
        REFERENCES KhachHang(MaKhachHang)
)


-- 8. Bảng chi tiết đơn hàng
CREATE TABLE ChiTietDonHang
(
    MaChiTietDonHang INT PRIMARY KEY IDENTITY(1,1),

    MaDonHang INT,

    MaSanPham INT,

    SoLuong INT,

    Gia DECIMAL(18,2),

    FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang),

    FOREIGN KEY (MaSanPham)
        REFERENCES SanPham(MaSanPham)
)


-- 9. Bảng thanh toán
CREATE TABLE ThanhToan
(
    MaThanhToan INT PRIMARY KEY IDENTITY(1,1),

    MaDonHang INT,

    PhuongThucThanhToan NVARCHAR(50),

    TrangThaiThanhToan NVARCHAR(50),

    NgayThanhToan DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)
)


-- 10. Bảng vận chuyển
CREATE TABLE VanChuyen
(
    MaVanChuyen INT PRIMARY KEY IDENTITY(1,1),

    MaDonHang INT,

    DiaChiGiaoHang NVARCHAR(255),

    TrangThaiVanChuyen NVARCHAR(50),

    NgayVanChuyen DATETIME,

    FOREIGN KEY (MaDonHang)
        REFERENCES DonHang(MaDonHang)
)


-- 11. Bảng đánh giá
CREATE TABLE DanhGia
(
    MaDanhGia INT PRIMARY KEY IDENTITY(1,1),

    MaSanPham INT,

    MaKhachHang INT,

    SoSao INT,

    BinhLuan NVARCHAR(500),

    NgayDanhGia DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (MaSanPham)
        REFERENCES SanPham(MaSanPham),

    FOREIGN KEY (MaKhachHang)
        REFERENCES KhachHang(MaKhachHang)
)


-- 12. Bảng kho hàng
CREATE TABLE KhoHang
(
    MaKho INT PRIMARY KEY IDENTITY(1,1),
    MaSanPham INT,
    SoLuongTon INT,
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaSanPham)
        REFERENCES SanPham(MaSanPham)
)


INSERT INTO VaiTro(TenVaiTro)
VALUES
(N'Admin'),
(N'User')

Select * from SanPham

Select * from KhachHang

Update KhachHang
Set MaVaiTro = 1
Where MaKhachHang = 1



--Thêm dữ liệu



USE ShopPhuKienDB
GO

-- =========================
-- DANH MỤC
-- =========================

INSERT INTO DanhMuc(TenDanhMuc)
VALUES

DELETE FROM DanhMuc


-- =========================
-- NHÀ CUNG CẤP
-- =========================

INSERT INTO NhaCungCap(TenNhaCungCap, SoDienThoai, DiaChi)
VALUES

DELETE FROM NhaCungCap


-- =========================
-- KHUYẾN MÃI
-- =========================

INSERT INTO KhuyenMai(TenKhuyenMai, PhanTramGiam, NgayBatDau, NgayKetThuc)
VALUES

DELETE FROM KhuyenMai


-- =========================
-- KHÁCH HÀNG
-- =========================

INSERT INTO KhachHang(HoTen, Email, MatKhau, SoDienThoai, DiaChi, MaVaiTro)
VALUES

DELETE FROM KhachHang


-- =========================
-- SẢN PHẨM
-- =========================

INSERT INTO SanPham(TenSanPham, Gia, SoLuong, MoTa, HinhAnh, MaDanhMuc, MaNhaCungCap, MaKhuyenMai)
VALUES

DELETE FROM SanPham


-- =========================
-- KHO HÀNG
-- =========================

INSERT INTO KhoHang(MaSanPham, SoLuongTon)
VALUES

DELETE FROM KhoHang


-- =========================
-- ĐÁNH GIÁ
-- =========================

INSERT INTO DanhGia(MaSanPham, MaKhachHang, SoSao, BinhLuan)
VALUES

DELETE FROM DanhGia

DELETE FROM ChiTietPhieuNhap

DELETE FROM PhieuNhap

DELETE FROM ChiTietDonHang

DELETE FROM DonHang

DELETE FROM ThanhToan

DELETE FROM VanChuyen
-- =========================
-- KIỂM TRA
-- =========================

SELECT * FROM SanPham
SELECT * FROM DanhMuc
SELECT * FROM KhachHang
SELECT * FROM DanhGia