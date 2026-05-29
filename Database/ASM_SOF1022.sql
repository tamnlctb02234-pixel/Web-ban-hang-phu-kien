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
(N'Gấu bông'),
(N'Móc khóa'),
(N'Bình nước'),
(N'Phụ kiện'),
(N'Đèn ngủ'),
(N'Ba lô'),
(N'Văn phòng phẩm')


-- =========================
-- NHÀ CUNG CẤP
-- =========================

INSERT INTO NhaCungCap(TenNhaCungCap, SoDienThoai, DiaChi)
VALUES
(N'Công ty Teddy House', '0908123456', N'Quận 1, TP.HCM'),
(N'Cute Planet Việt Nam', '0911222333', N'Cầu Giấy, Hà Nội'),
(N'Bunny Mall', '0988777666', N'Hải Châu, Đà Nẵng'),
(N'Lovely Gift Store', '0977555444', N'Nha Trang, Khánh Hòa')


-- =========================
-- KHUYẾN MÃI
-- =========================

INSERT INTO KhuyenMai(TenKhuyenMai, PhanTramGiam, NgayBatDau, NgayKetThuc)
VALUES
(N'Sale Cuối Tuần', 10, '2026-05-01', '2026-06-30'),
(N'Flash Sale 5.5', 20, '2026-05-05', '2026-05-10'),
(N'Mừng Sinh Nhật Shop', 15, '2026-06-01', '2026-06-15'),
(N'Siêu Sale Mùa Hè', 25, '2026-07-01', '2026-07-20')


-- =========================
-- KHÁCH HÀNG
-- =========================

INSERT INTO KhachHang(HoTen, Email, MatKhau, SoDienThoai, DiaChi, MaVaiTro)
VALUES
(N'Nguyễn Minh Quân', 'admin@gmail.com', '123456', '0909000001', N'Bình Thạnh, TP.HCM', 1),
(N'Trần Ngọc Ánh', 'ngocanh@gmail.com', '123456', '0909000002', N'Thanh Xuân, Hà Nội', 2),
(N'Lê Hoàng Nam', 'hoangnam@gmail.com', '123456', '0909000003', N'Sơn Trà, Đà Nẵng', 2),
(N'Phạm Khánh Vy', 'khanhvy@gmail.com', '123456', '0909000004', N'Ninh Kiều, Cần Thơ', 2),
(N'Đỗ Gia Hân', 'giahan@gmail.com', '123456', '0909000005', N'Thủ Đức, TP.HCM', 2)


-- =========================
-- SẢN PHẨM
-- =========================

INSERT INTO SanPham(TenSanPham, Gia, SoLuong, MoTa, HinhAnh, MaDanhMuc, MaNhaCungCap, MaKhuyenMai)
VALUES
(N'Gấu Bông Teddy Classic 80cm', 450000, 25, N'Gấu bông teddy cao cấp phong cách Hàn Quốc', 'gau1.jpg', 1, 1, 1),
(N'Gấu Bông Thỏ Hồng Bunny', 390000, 18, N'Gấu thỏ màu pastel siêu mềm mại', 'gau2.jpg', 1, 2, 2),
(N'Gấu Bông Capybara Cute', 520000, 12, N'Capybara size lớn dành cho decor phòng ngủ', 'gau3.jpg', 1, 3, 3),
(N'Gấu Bông Dâu Tây Sweet Bear', 430000, 16, N'Gấu bông hình trái dâu đáng yêu', 'gau4.jpg', 1, 1, NULL),
(N'Gấu Bông Brown Coffee', 480000, 10, N'Gấu màu nâu cafe phong cách vintage', 'gau5.jpg', 1, 4, 4),

(N'Móc Khóa Capybara Mini', 85000, 50, N'Móc khóa capybara mini dễ thương', 'mk1.jpg', 2, 2, NULL),
(N'Móc Khóa Gấu Brown', 95000, 40, N'Móc khóa gấu brown mềm mại', 'mk2.jpg', 2, 1, 1),
(N'Móc Khóa Thỏ Bunny', 79000, 45, N'Móc khóa thỏ hồng phong cách Hàn Quốc', 'mk3.jpg', 2, 3, NULL),
(N'Móc Khóa Mèo Mochi', 99000, 30, N'Móc khóa mèo mochi siêu cute', 'mk4.jpg', 2, 4, 2),

(N'Bình Nước Giữ Nhiệt Bear 500ml', 220000, 35, N'Bình nước giữ nhiệt hình gấu', 'binh1.jpg', 3, 2, 1),
(N'Bình Nước Bunny Pastel', 250000, 28, N'Bình nước phong cách pastel dễ thương', 'binh2.jpg', 3, 3, 3),
(N'Bình Nước Totoro Mini', 199000, 22, N'Bình nước mini tiện lợi mang đi học', 'binh3.jpg', 3, 4, NULL),

(N'Kẹp Tóc Bunny Pink', 65000, 60, N'Kẹp tóc phong cách ulzzang', 'pk1.jpg', 4, 1, NULL),
(N'Băng Đô Tai Thỏ', 120000, 25, N'Băng đô tai thỏ dành cho makeup', 'pk2.jpg', 4, 2, 2),
(N'Ví Mini Gấu Nâu', 180000, 20, N'Ví mini cute đựng tiền và thẻ', 'pk3.jpg', 4, 4, 1),

(N'Đèn Ngủ Silicon Thỏ Trắng', 320000, 15, N'Đèn ngủ silicone cảm ứng siêu dễ thương', 'den1.jpg', 5, 3, 4),
(N'Đèn Ngủ Gấu Brown', 350000, 10, N'Đèn ngủ ánh sáng vàng dịu mắt', 'den2.jpg', 5, 1, NULL),

(N'Ba Lô Gấu Teddy', 420000, 18, N'Ba lô phong cách học sinh Hàn Quốc', 'balo1.jpg', 6, 2, 3),
(N'Ba Lô Bunny Pastel', 460000, 14, N'Ba lô màu pastel đáng yêu', 'balo2.jpg', 6, 4, 4),

(N'Sổ Tay Capybara', 55000, 70, N'Sổ tay mini dùng học tập', 'vp1.jpg', 7, 3, NULL),
(N'Bút Gel Bunny', 25000, 120, N'Bút gel mực đen phong cách cute', 'vp2.jpg', 7, 2, NULL),
(N'Sticker Gấu Bông', 35000, 90, N'Sticker trang trí laptop và sổ tay', 'vp3.jpg', 7, 1, 1)


-- =========================
-- KHO HÀNG
-- =========================

INSERT INTO KhoHang(MaSanPham, SoLuongTon)
VALUES
(1,25),(2,18),(3,12),(4,16),(5,10),
(6,50),(7,40),(8,45),(9,30),
(10,35),(11,28),(12,22),
(13,60),(14,25),(15,20),
(16,15),(17,10),
(18,18),(19,14),
(20,70),(21,120),(22,90)


-- =========================
-- ĐÁNH GIÁ
-- =========================

INSERT INTO DanhGia(MaSanPham, MaKhachHang, SoSao, BinhLuan)
VALUES
(1,2,5,N'Gấu cực mềm và đẹp y hình'),
(2,3,5,N'Màu hồng rất xinh'),
(3,4,4,N'Capybara siêu đáng yêu'),
(6,5,5,N'Móc khóa nhỏ xinh cực cute'),
(10,2,5,N'Giữ nhiệt rất tốt'),
(16,3,5,N'Đèn ngủ đẹp và sáng dịu'),
(18,4,4,N'Ba lô rộng và đẹp'),
(21,5,5,N'Bút viết rất mượt')


-- =========================
-- KIỂM TRA
-- =========================

SELECT * FROM SanPham
SELECT * FROM DanhMuc
SELECT * FROM KhachHang
SELECT * FROM DanhGia