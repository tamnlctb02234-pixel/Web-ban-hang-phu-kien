using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class KhachHangsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public KhachHangsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }
        // =========================
        // DANH SÁCH KHÁCH HÀNG
        // =========================
        public async Task<IActionResult> Index()
        {
            var dsKhachHang = _context.KhachHangs
                .Include(k => k.MaVaiTroNavigation);

            return View(await dsKhachHang.ToListAsync());
        }

        // =========================
        // CHI TIẾT
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .Include(k => k.MaVaiTroNavigation)
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // =========================
        // TẠO KHÁCH HÀNG
        // =========================
        public IActionResult Create()
        {
            ViewData["MaVaiTro"] = new SelectList(
                _context.VaiTros,
                "MaVaiTro",
                "TenVaiTro"
            );

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khachHang);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["MaVaiTro"] = new SelectList(
                _context.VaiTros,
                "MaVaiTro",
                "TenVaiTro",
                khachHang.MaVaiTro
            );

            return View(khachHang);
        }

        // =========================
        // SỬA KHÁCH HÀNG
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            ViewData["MaVaiTro"] = new SelectList(
                _context.VaiTros,
                "MaVaiTro",
                "TenVaiTro",
                khachHang.MaVaiTro
            );

            return View(khachHang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            KhachHang khachHang
        )
        {
            if (id != khachHang.MaKhachHang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKhachHang))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MaVaiTro"] = new SelectList(
                _context.VaiTros,
                "MaVaiTro",
                "TenVaiTro",
                khachHang.MaVaiTro
            );

            return View(khachHang);
        }

        // =========================
        // XÓA KHÁCH HÀNG
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .Include(k => k.MaVaiTroNavigation)
                .FirstOrDefaultAsync(m => m.MaKhachHang == id);

            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);

            if (khachHang == null)
            {
                return NotFound();
            }

            // Nếu có đơn hàng thì không cho xóa
            bool coDonHang = await _context.DonHangs
                .AnyAsync(x => x.MaKhachHang == id);

            if (coDonHang)
            {
                TempData["error"] =
                    "Khách hàng đã phát sinh đơn hàng nên không thể xóa.";

                return RedirectToAction(nameof(Index));
            }

            // Xóa các đánh giá của khách hàng
            var danhGia = await _context.DanhGia
                .Where(x => x.MaKhachHang == id)
                .ToListAsync();

            _context.DanhGia.RemoveRange(danhGia);

            // Xóa khách hàng
            _context.KhachHangs.Remove(khachHang);

            await _context.SaveChangesAsync();

            TempData["success"] = "Xóa khách hàng thành công.";

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // KIỂM TRA TỒN TẠI
        // =========================
        private bool KhachHangExists(int id)
        {
            return _context.KhachHangs
                .Any(e => e.MaKhachHang == id);
        }
    }
}
