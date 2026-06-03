using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM1_SOF1022.Controllers
{
    public class KhuyenMaisController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public KhuyenMaisController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH
        public IActionResult Index()
        {
            return View(_context.KhuyenMais.ToList());
        }

        // GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        // POST CREATE
        [HttpPost]
        public async Task<IActionResult> Create(KhuyenMai km)
        {
            _context.KhuyenMais.Add(km);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var km = _context.KhuyenMais.Find(id);

            return View(km);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(KhuyenMai km)
        {
            _context.KhuyenMais.Update(km);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            bool dangDuocSuDung = _context.SanPhams
                .Any(x => x.MaKhuyenMai == id);

            if (dangDuocSuDung)
            {
                TempData["error"] =
                    "Khuyến mãi đang được áp dụng cho sản phẩm nên không thể xóa.";

                return RedirectToAction(nameof(Index));
            }

            var km = _context.KhuyenMais.Find(id);

            _context.KhuyenMais.Remove(km);

            _context.SaveChanges();

            TempData["success"] =
                "Xóa khuyến mãi thành công.";

            return RedirectToAction(nameof(Index));
        }
    }
}