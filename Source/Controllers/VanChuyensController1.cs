using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class VanChuyensController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public VanChuyensController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var ds = _context.VanChuyens
                .Include(v => v.MaDonHangNavigation);

            return View(await ds.ToListAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vc = await _context.VanChuyens.FindAsync(id);

            if (vc == null)
            {
                return NotFound();
            }

            return View(vc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, VanChuyen model)
        {
            var vc = await _context.VanChuyens.FindAsync(id);

            vc.TrangThaiVanChuyen =
                model.TrangThaiVanChuyen;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}