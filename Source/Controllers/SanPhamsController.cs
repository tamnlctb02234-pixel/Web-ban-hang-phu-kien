using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM1_SOF1022.Models;

namespace ASM1_SOF1022.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public SanPhamsController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // DANH SÁCH SẢN PHẨM
        public async Task<IActionResult> Index(string search)
        {
            var sanPhams = _context.SanPhams
                .Include(x => x.MaDanhMucNavigation)
                .Include(x => x.MaKhuyenMaiNavigation)
                .Include(x => x.MaNhaCungCapNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                sanPhams = sanPhams.Where(x =>
                    x.TenSanPham.Contains(search));
            }

            return View(sanPhams.ToList());
        }

        // FORM THÊM
        public IActionResult Create()
        {
            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View();
        }

        // XỬ LÝ THÊM
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.SanPhams.Add(sanPham);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPham);
        }

        // FORM SỬA
        public async Task<IActionResult> Edit(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);

            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPham);
        }

        // XỬ LÝ SỬA
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SanPham sanPham)
        {
            if (id != sanPham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(sanPham);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPham);
        }


        // XÓA
        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _context.SanPhams
                .FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            // Xóa chi tiết đơn hàng liên quan
            var chiTiet = _context.ChiTietDonHangs
                .Where(x => x.MaSanPham == id)
                .ToList();

            _context.ChiTietDonHangs.RemoveRange(chiTiet);

            // Xóa kho hàng liên quan
            var khoHang = _context.KhoHangs
                .Where(x => x.MaSanPham == id)
                .ToList();

            _context.KhoHangs.RemoveRange(khoHang);

            // Xóa đánh giá liên quan
            var danhGia = _context.DanhGia
                .Where(x => x.MaSanPham == id)
                .ToList();

            _context.DanhGia.RemoveRange(danhGia);

            // Xóa sản phẩm
            _context.SanPhams.Remove(sanPham);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}