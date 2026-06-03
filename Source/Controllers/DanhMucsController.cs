using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM1_SOF1022.Models;

namespace ASM1_SOF1022.Controllers
{
    public class DanhMucsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public DanhMucsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // HIỂN THỊ DANH SÁCH
        // =========================
        public async Task<IActionResult> Index()
        {
            return View(await _context.DanhMucs.ToListAsync());
        }

        // =========================
        // FORM THÊM
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // XỬ LÝ THÊM
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhMuc);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(danhMuc);
        }

        // =========================
        // FORM SỬA
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs.FindAsync(id);

            if (danhMuc == null)
            {
                return NotFound();
            }

            return View(danhMuc);
        }

        // =========================
        // XỬ LÝ SỬA
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DanhMuc danhMuc)
        {
            if (id != danhMuc.MaDanhMuc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhMuc);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhMucExists(danhMuc.MaDanhMuc))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(danhMuc);
        }

        // =========================
        // XÓA
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var danhMuc = await _context.DanhMucs
                .FirstOrDefaultAsync(x => x.MaDanhMuc == id);

            if (danhMuc == null)
            {
                return NotFound();
            }

            bool coSanPham = await _context.SanPhams
                .AnyAsync(x => x.MaDanhMuc == id);

            if (coSanPham)
            {
                TempData["error"] =
                    "Không thể xóa danh mục vì đang có sản phẩm thuộc danh mục này.";

                return RedirectToAction(nameof(Index));
            }

            _context.DanhMucs.Remove(danhMuc);

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Xóa danh mục thành công.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var chiTiet = _context.ChiTietDonHangs
                .Where(x => x.MaDonHang == id)
                .ToList();

            return View(chiTiet);
        }

        // =========================
        // KIỂM TRA TỒN TẠI
        // =========================
        private bool DanhMucExists(int id)
        {
            return _context.DanhMucs.Any(e => e.MaDanhMuc == id);
        }
    }
}