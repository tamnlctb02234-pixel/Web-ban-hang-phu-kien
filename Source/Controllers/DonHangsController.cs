using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public DonHangsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // ADMIN: DANH SÁCH ĐƠN HÀNG
        // =========================
        public async Task<IActionResult> Index()
        {
            var donHangs = _context.DonHangs
                .Include(d => d.MaKhachHangNavigation)
                .OrderByDescending(d => d.NgayDatHang);

            return View(await donHangs.ToListAsync());
        }

        // =========================
        // KHÁCH: ĐƠN HÀNG CỦA TÔI
        // =========================
        public IActionResult MyOrders()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var donHangs = _context.DonHangs
                .Where(x => x.MaKhachHang == userId)
                .OrderByDescending(x => x.NgayDatHang)
                .ToList();

            return View(donHangs);
        }

        // =========================
        // CHI TIẾT ĐƠN HÀNG
        // =========================
        public IActionResult Details(int id)
        {
            var chiTiet = _context.ChiTietDonHangs
                .Include(x => x.MaSanPhamNavigation)
                .Where(x => x.MaDonHang == id)
                .ToList();

            return View(chiTiet);
        }

        // =========================
        // CẬP NHẬT TRẠNG THÁI
        // =========================
        public IActionResult UpdateStatus(int id, string status)
        {
            var donHang = _context.DonHangs
                .FirstOrDefault(x => x.MaDonHang == id);

            if (donHang == null)
            {
                return NotFound();
            }

            donHang.TrangThai = status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // XÓA ĐƠN HÀNG
        // =========================
        public IActionResult Delete(int id)
        {
            var donHang = _context.DonHangs
                .FirstOrDefault(x => x.MaDonHang == id);

            if (donHang == null)
            {
                return NotFound();
            }

            _context.DonHangs.Remove(donHang);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}