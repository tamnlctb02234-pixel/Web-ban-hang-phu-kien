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
            bool TonTai = _context.KhuyenMais.Any(x => x.TenKhuyenMai == km.TenKhuyenMai);

            if (TonTai)
            {
                ModelState.AddModelError("TenKhuyenMai", "Tên khuyến mãi đã tồn tại");
            }

            if (km.NgayBatDau > km.NgayKetThuc)
            {
                ModelState.AddModelError(
                    "",
                    "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc");
            }

            if (!ModelState.IsValid)
            {
                return View(km);
            }

            _context.KhuyenMais.Add(km);

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Thêm khuyến mãi thành công";

            return RedirectToAction(nameof(Index));
        }

        // EDIT
        public IActionResult Edit(int id)
        {
            var km = _context.KhuyenMais.Find(id);

            return View(km);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(KhuyenMai km)
        {
            // Kiểm tra ngày
            if (km.NgayBatDau > km.NgayKetThuc)
            {
                ModelState.AddModelError(
                    "",
                    "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc");
            }

            // Kiểm tra trùng tên (trừ chính bản ghi đang sửa)
            bool trungTen = _context.KhuyenMais.Any(x =>
                x.TenKhuyenMai == km.TenKhuyenMai
                && x.MaKhuyenMai != km.MaKhuyenMai);

            if (trungTen)
            {
                ModelState.AddModelError(
                    "TenKhuyenMai",
                    "Tên khuyến mãi đã tồn tại");
            }

            if (!ModelState.IsValid)
            {
                return View(km);
            }

            // Tìm khuyến mãi trong database
            var khuyenMai = await _context.KhuyenMais
                .FindAsync(km.MaKhuyenMai);

            if (khuyenMai == null)
            {
                return NotFound();
            }

            // Cập nhật dữ liệu
            khuyenMai.TenKhuyenMai = km.TenKhuyenMai;
            khuyenMai.PhanTramGiam = km.PhanTramGiam;
            khuyenMai.NgayBatDau = km.NgayBatDau;
            khuyenMai.NgayKetThuc = km.NgayKetThuc;

            await _context.SaveChangesAsync();

            TempData["success"] =
                "Cập nhật khuyến mãi thành công";

            return RedirectToAction(nameof(Index));
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