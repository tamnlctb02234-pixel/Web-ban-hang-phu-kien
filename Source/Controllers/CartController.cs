using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASM1_SOF1022.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public CartController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // HIỂN THỊ GIỎ HÀNG
        // =========================
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("Cart");

            List<CartItem> cartItems = new List<CartItem>();

            if (cart != null)
            {
                cartItems =
                    JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }

            return View(cartItems);
        }

        // =========================
        // THÊM VÀO GIỎ HÀNG
        // =========================
        public IActionResult AddToCart(int id)
        {
            // KIỂM TRA ĐĂNG NHẬP
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                TempData["error"] =
                    "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng";

                return RedirectToAction("Login", "Account");
            }

            // TÌM SẢN PHẨM
            var sanPham = _context.SanPhams
                .FirstOrDefault(s => s.MaSanPham == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            // KIỂM TRA HẾT HÀNG
            if (sanPham.SoLuong <= 0)
            {
                TempData["error"] =
                    "Sản phẩm hiện đã hết hàng";

                return RedirectToAction("Index", "Shop");
            }

            // LẤY GIỎ HÀNG
            var cart = HttpContext.Session.GetString("Cart");

            List<CartItem> cartItems;

            if (cart != null)
            {
                cartItems =
                    JsonConvert.DeserializeObject<List<CartItem>>(cart);
            }
            else
            {
                cartItems = new List<CartItem>();
            }

            // KIỂM TRA SẢN PHẨM ĐÃ TỒN TẠI
            var item = cartItems
                .FirstOrDefault(c => c.MaSanPham == id);

            if (item != null)
            {
                // KIỂM TRA TỒN KHO
                if (item.SoLuong >= sanPham.SoLuong)
                {
                    TempData["error"] =
                        "Số lượng sản phẩm trong kho không đủ";

                    return RedirectToAction("Index");
                }

                item.SoLuong++;
            }
            else
            {
                cartItems.Add(new CartItem
                {
                    MaSanPham = sanPham.MaSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    Gia = sanPham.Gia,
                    SoLuong = 1,
                    HinhAnh = sanPham.HinhAnh
                });
            }

            // LƯU SESSION
            HttpContext.Session.SetString(
                "Cart",
                JsonConvert.SerializeObject(cartItems));

            TempData["success"] =
                "Đã thêm sản phẩm vào giỏ hàng";

            return RedirectToAction("Index");
        }

        // =========================
        // XÓA KHỎI GIỎ HÀNG
        // =========================
        public IActionResult Remove(int id)
        {
            var cart = HttpContext.Session.GetString("Cart");

            if (cart != null)
            {
                var cartItems =
                    JsonConvert.DeserializeObject<List<CartItem>>(cart);

                var item = cartItems
                    .FirstOrDefault(c => c.MaSanPham == id);

                if (item != null)
                {
                    cartItems.Remove(item);
                }

                HttpContext.Session.SetString(
                    "Cart",
                    JsonConvert.SerializeObject(cartItems));

                TempData["success"] =
                    "Đã xóa sản phẩm khỏi giỏ hàng";
            }

            return RedirectToAction("Index");
        }

        // =========================
        // FORM THANH TOÁN
        // =========================
        public IActionResult Checkout()
        {
            // KIỂM TRA ĐĂNG NHẬP
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                TempData["error"] =
                    "Bạn cần đăng nhập để thanh toán";

                return RedirectToAction("Login", "Account");
            }

            // KIỂM TRA GIỎ HÀNG
            var cart = HttpContext.Session.GetString("Cart");

            if (cart == null)
            {
                TempData["error"] =
                    "Giỏ hàng đang trống";

                return RedirectToAction("Index");
            }

            return View();
        }

        // =========================
        // XỬ LÝ THANH TOÁN
        // =========================
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel model)
        {
            // KIỂM TRA ĐĂNG NHẬP
            int? userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                TempData["error"] =
                    "Bạn cần đăng nhập để thanh toán";

                return RedirectToAction("Login", "Account");
            }

            // LẤY GIỎ HÀNG
            var cart = HttpContext.Session.GetString("Cart");

            if (cart == null)
            {
                TempData["error"] =
                    "Giỏ hàng đang trống";

                return RedirectToAction("Index");
            }

            var cartItems =
                JsonConvert.DeserializeObject<List<CartItem>>(cart);

            // KIỂM TRA TỒN KHO
            foreach (var item in cartItems)
            {
                var sp = await _context.SanPhams
                    .FindAsync(item.MaSanPham);

                if (sp == null)
                {
                    TempData["error"] =
                        "Sản phẩm không tồn tại";

                    return RedirectToAction("Index");
                }

                if (sp.SoLuong < item.SoLuong)
                {
                    TempData["error"] =
                        $"Sản phẩm {sp.TenSanPham} không đủ hàng";

                    return RedirectToAction("Index");
                }
            }

            // TÍNH TỔNG TIỀN
            decimal tongTien =
                cartItems.Sum(x => x.ThanhTien);

            // TẠO ĐƠN HÀNG
            DonHang donHang = new DonHang()
            {
                MaKhachHang = userId,
                NgayDatHang = DateTime.Now,
                TongTien = tongTien,
                TrangThai = "Chờ xác nhận"
            };

            _context.DonHangs.Add(donHang);

            await _context.SaveChangesAsync();

            // CHI TIẾT ĐƠN HÀNG
            foreach (var item in cartItems)
            {
                ChiTietDonHang ct = new ChiTietDonHang()
                {
                    MaDonHang = donHang.MaDonHang,
                    MaSanPham = item.MaSanPham,
                    SoLuong = item.SoLuong,
                    Gia = item.Gia
                };

                _context.ChiTietDonHangs.Add(ct);

                // TRỪ KHO
                var sp = await _context.SanPhams
                    .FindAsync(item.MaSanPham);

                sp.SoLuong -= item.SoLuong;
            }

            // TẠO THANH TOÁN
            ThanhToan thanhToan = new ThanhToan()
            {
                MaDonHang = donHang.MaDonHang,
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                TrangThaiThanhToan = "Chưa thanh toán",
                NgayThanhToan = DateTime.Now
            };

            _context.ThanhToans.Add(thanhToan);

            // TẠO VẬN CHUYỂN
            VanChuyen vanChuyen = new VanChuyen()
            {
                MaDonHang = donHang.MaDonHang,
                DiaChiGiaoHang = model.DiaChiGiaoHang,
                TrangThaiVanChuyen = "Đang chuẩn bị",
                NgayVanChuyen = DateTime.Now
            };

            _context.VanChuyens.Add(vanChuyen);

            await _context.SaveChangesAsync();

            // XÓA GIỎ HÀNG
            HttpContext.Session.Remove("Cart");

            TempData["success"] =
                "Đặt hàng thành công";

            return RedirectToAction("Success");
        }

        // =========================
        // THÀNH CÔNG
        // =========================
        public IActionResult Success()
        {
            return View();
        }
    }
}