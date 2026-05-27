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

        // EDIT
        public IActionResult Edit(int id)
        {
            var kho = _context.KhoHangs
                .Include(k => k.MaSanPhamNavigation)
                .FirstOrDefault(k => k.MaKho == id);

            return View(kho);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(KhoHang kho)
        {
            var khoHang = await _context.KhoHangs
                .FindAsync(kho.MaKho);

            if (khoHang == null)
            {
                return NotFound();
            }

            khoHang.SoLuongTon = kho.SoLuongTon;

            khoHang.NgayCapNhat = DateTime.Now;

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Cập nhật kho hàng thành công";

            return RedirectToAction("Index");
        }
    }
}