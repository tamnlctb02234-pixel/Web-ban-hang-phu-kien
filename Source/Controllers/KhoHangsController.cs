using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class KhoHangsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public KhoHangsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH
        public IActionResult Index()
        {
            var ds = _context.KhoHangs
                .Include(k => k.MaSanPhamNavigation)
                .ToList();

            return View(ds);
        }

        // FORM SỬA
        public IActionResult Edit(int id)
        {
            var kho = _context.KhoHangs
                .Include(k => k.MaSanPhamNavigation)
                .FirstOrDefault(k => k.MaKho == id);

            if (kho == null)
            {
                return NotFound();
            }

            return View(kho);
        }

        // XỬ LÝ SỬA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KhoHang kho)
        {
            if (kho.SoLuongTon < 0)
            {
                ModelState.AddModelError(
                    "SoLuongTon",
                    "Số lượng tồn không được âm.");
            }

            if (!ModelState.IsValid)
            {
                return View(kho);
            }

            var khoHang = await _context.KhoHangs
                .FindAsync(kho.MaKho);

            if (khoHang == null)
            {
                return NotFound();
            }

            // Cập nhật kho
            khoHang.SoLuongTon = kho.SoLuongTon;
            khoHang.NgayCapNhat = DateTime.Now;

            // Cập nhật luôn số lượng sản phẩm
            var sanPham = await _context.SanPhams
                .FindAsync(khoHang.MaSanPham);

            if (sanPham != null)
            {
                sanPham.SoLuong = kho.SoLuongTon ?? 0;
            }

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Cập nhật kho hàng thành công.";

            return RedirectToAction(nameof(Index));
        }

        // SẢN PHẨM SẮP HẾT HÀNG
        public IActionResult LowStock()
        {
            var ds = _context.KhoHangs
                .Include(x => x.MaSanPhamNavigation)
                .Where(x => x.SoLuongTon < 10)
                .OrderBy(x => x.SoLuongTon)
                .ToList();

            return View(ds);
        }
    }
}