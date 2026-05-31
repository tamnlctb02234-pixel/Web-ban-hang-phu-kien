using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class PhieuNhapsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public PhieuNhapsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // Danh sách phiếu nhập
        public IActionResult Index()
        {
            var ds = _context.PhieuNhaps
                .Include(x => x.MaNhaCungCapNavigation)
                .OrderByDescending(x => x.NgayNhap)
                .ToList();

            return View(ds);
        }

        // Chi tiết phiếu nhập
        public IActionResult Details(int id)
        {
            var phieuNhap = _context.PhieuNhaps
                .Include(x => x.MaNhaCungCapNavigation)
                .FirstOrDefault(x => x.MaPhieuNhap == id);

            if (phieuNhap == null)
                return NotFound();

            var chiTiet = _context.ChiTietPhieuNhaps
                .Include(x => x.MaSanPhamNavigation)
                .Where(x => x.MaPhieuNhap == id)
                .ToList();

            ViewBag.PhieuNhap = phieuNhap;
            ViewBag.ChiTiet = chiTiet;

            return View();
        }
    }
}