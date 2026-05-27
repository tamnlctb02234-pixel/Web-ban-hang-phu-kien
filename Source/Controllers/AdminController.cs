using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM1_SOF1022.Controllers
{
    public class AdminController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public AdminController(ShopPhuKienDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.TongSanPham = _context.SanPhams.Count();
            ViewBag.TongDonHang = _context.DonHangs.Count();
            ViewBag.TongKhachHang = _context.KhachHangs.Count();
            ViewBag.TongDanhGia = _context.DanhGia.Count();

            ViewBag.TotalRevenue = _context.DonHangs.Sum(x => (decimal?)x.TongTien) ?? 0;

            return View();
        }
    }
}
