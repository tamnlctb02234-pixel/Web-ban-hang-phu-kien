using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ASM1_SOF1022.Controllers
{
    [Authorize]
    public class ThanhToansController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public ThanhToansController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH THANH TOÁN
        public async Task<IActionResult> Index()
        {
            var ds = _context.ThanhToans
                .Include(t => t.MaDonHangNavigation);

            return View(await ds.ToListAsync());
        }

        // CẬP NHẬT TRẠNG THÁI
        public async Task<IActionResult> Edit(int id)
        {
            var tt = await _context.ThanhToans.FindAsync(id);

            if (tt == null)
            {
                return NotFound();
            }

            return View(tt);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ThanhToan model)
        {
            var tt = await _context.ThanhToans.FindAsync(id);

            if (tt == null)
            {
                return NotFound();
            }

            tt.TrangThaiThanhToan = model.TrangThaiThanhToan;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}