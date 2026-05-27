using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class DanhGiasController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public DanhGiasController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH
        public IActionResult Index()
        {
            var ds = _context.DanhGia
                .Include(d => d.MaSanPhamNavigation)
                .Include(d => d.MaKhachHangNavigation)
                .ToList();

            return View(ds);
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.SanPhams = _context.SanPhams.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DanhGium model)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserID");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            model.MaKhachHang = userId.Value;

            model.NgayDanhGia = DateTime.Now;

            _context.DanhGia.Add(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(
                "Details",
                "SanPhams",
                new { id = model.MaSanPham }
            );
        }


        public IActionResult ByProduct(int id)
        {
            var sanPham = _context.SanPhams
                .Include(s => s.DanhGia)
                    .ThenInclude(d => d.MaKhachHangNavigation)
                .FirstOrDefault(s => s.MaSanPham == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }


        public async Task<IActionResult> Delete(int id)
        {
            // CHECK ADMIN
            var role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var danhGia = await _context.DanhGia
                .FindAsync(id);

            if (danhGia == null)
            {
                return NotFound();
            }

            _context.DanhGia.Remove(danhGia);

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Đã xóa bình luận";

            return RedirectToAction("Index");
        }

    }
}