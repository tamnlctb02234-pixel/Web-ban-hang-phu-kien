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
                .OrderByDescending(x => x.MaSanPham)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                sanPhams = sanPhams.Where(x =>
                    x.TenSanPham.Contains(search));
            }

            return View(sanPhams.ToList());
        }

        // FORM THÊM
        public async Task<IActionResult> Create()
        {
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            ViewBag.NhaCungCaps = _context.NhaCungCaps.ToList();
            ViewBag.KhuyenMais = _context.KhuyenMais.ToList();

            return View();
        }

        // XỬ LÝ THÊM
        [HttpPost]
        public async Task<IActionResult> Create(SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                // Thêm sản phẩm
                _context.SanPhams.Add(sanPham);
                await _context.SaveChangesAsync();

                // Tạo kho hàng tương ứng
                var kho = new KhoHang
                {
                    MaSanPham = sanPham.MaSanPham,
                    SoLuongTon = sanPham.SoLuong,
                    NgayCapNhat = DateTime.Now
                };

                _context.KhoHangs.Add(kho);

                await _context.SaveChangesAsync();

                TempData["success"] = "Thêm sản phẩm thành công";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPham);
        }

        // FORM SỬA
        public async Task<IActionResult> Edit(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);

            if(sanPham == null)
            {
                return NotFound();
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            ViewBag.NhaCungCaps = _context.NhaCungCaps.ToList();
            ViewBag.KhuyenMais = _context.KhuyenMais.ToList();

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

                var kho = await _context.KhoHangs
                    .FirstOrDefaultAsync(x => x.MaSanPham == sanPham.MaSanPham);

                if (kho != null)
                {
                    kho.SoLuongTon = sanPham.SoLuong;
                    kho.NgayCapNhat = DateTime.Now;
                }

                await _context.SaveChangesAsync();

                TempData["success"] = "Cập nhật sản phẩm thành công";

                return RedirectToAction(nameof(Index));
            }

            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPham);
        }


        // XÓA
        public async Task<IActionResult> Delete(int id)
        {
            var sanPham = await _context.SanPhams
                .FirstOrDefaultAsync(x => x.MaSanPham == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            // Chi tiết đơn hàng
            var chiTietDonHang = _context.ChiTietDonHangs
                .Where(x => x.MaSanPham == id);

            _context.ChiTietDonHangs.RemoveRange(chiTietDonHang);

            // Chi tiết phiếu nhập
            var chiTietNhap = _context.ChiTietPhieuNhaps
                .Where(x => x.MaSanPham == id);

            _context.ChiTietPhieuNhaps.RemoveRange(chiTietNhap);

            // Đánh giá
            var danhGia = _context.DanhGia
                .Where(x => x.MaSanPham == id);

            _context.DanhGia.RemoveRange(danhGia);

            // Kho hàng
            var khoHang = _context.KhoHangs
                .Where(x => x.MaSanPham == id);

            _context.KhoHangs.RemoveRange(khoHang);

            // Sản phẩm
            _context.SanPhams.Remove(sanPham);

            await _context.SaveChangesAsync();

            TempData["success"] = "Xóa sản phẩm thành công";

            return RedirectToAction(nameof(Index));
        }
    }
}