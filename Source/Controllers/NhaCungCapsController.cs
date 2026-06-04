using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM1_SOF1022.Controllers
{
    public class NhaCungCapsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public NhaCungCapsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH
        public IActionResult Index()
        {
            return View(_context.NhaCungCaps.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE
        [HttpPost]
        public async Task<IActionResult> Create(NhaCungCap ncc)
        {
            if (!ModelState.IsValid)
            {
                return View(ncc);
            }

            _context.NhaCungCaps.Add(ncc);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var ncc = _context.NhaCungCaps.Find(id);

            return View(ncc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NhaCungCap ncc)
        {
            _context.NhaCungCaps.Update(ncc);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var ncc = _context.NhaCungCaps.Find(id);

            if (ncc == null)
            {
                TempData["error"] =
                    "Nhà cung cấp không tồn tại.";

                return RedirectToAction(nameof(Index));
            }

            bool dangDuocSuDung = _context.SanPhams
                .Any(x => x.MaNhaCungCap == id);

            if (dangDuocSuDung)
            {
                TempData["error"] =
                    "Không thể xóa nhà cung cấp vì đang có sản phẩm thuộc nhà cung cấp này.";

                return RedirectToAction(nameof(Index));
            }

            _context.NhaCungCaps.Remove(ncc);

            _context.SaveChanges();

            TempData["success"] =
                "Xóa nhà cung cấp thành công.";

            return RedirectToAction(nameof(Index));
        }
    }
}