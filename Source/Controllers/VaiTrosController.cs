using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM1_SOF1022.Controllers
{
    public class VaiTrosController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public VaiTrosController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // DANH SÁCH VAI TRÒ
        // =========================
        public async Task<IActionResult> Index()
        {
            return View(await _context.VaiTros.ToListAsync());
        }

        // =========================
        // CREATE
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VaiTro model)
        {
            if (ModelState.IsValid)
            {
                _context.VaiTros.Add(model);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // =========================
        // EDIT
        // =========================
        public IActionResult Edit(int id)
        {
            var vaiTro = _context.VaiTros.Find(id);

            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VaiTro model)
        {
            _context.VaiTros.Update(model);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE
        // =========================
        public IActionResult Delete(int id)
        {
            var vaiTro = _context.VaiTros.Find(id);

            if (vaiTro == null)
            {
                return NotFound();
            }

            return View(vaiTro);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vaiTro = await _context.VaiTros.FindAsync(id);

            if (vaiTro != null)
            {
                _context.VaiTros.Remove(vaiTro);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}