using Microsoft.EntityFrameworkCore;
using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASM1_SOF1022.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public ShopController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // DANH SÁCH SẢN PHẨM
        // =========================

        public IActionResult Index(
            string searchString,
            int? maDanhMuc)
        {
            var sanPhams = _context.SanPhams
                .Include(s => s.MaDanhMucNavigation)
                .OrderByDescending(s => s.MaSanPham)
                .AsQueryable();

            // TÌM KIẾM
            if (!string.IsNullOrEmpty(searchString))
            {
                sanPhams = sanPhams.Where(s =>
                    s.TenSanPham.Contains(searchString));
            }

            // LỌC DANH MỤC
            if (maDanhMuc != null)
            {
                sanPhams = sanPhams.Where(s =>
                    s.MaDanhMuc == maDanhMuc);
            }

            // VIEWBAG DANH MỤC
            ViewBag.DanhMucs = _context.DanhMucs.ToList();

            return View(sanPhams.ToList());
        }

        // =========================
        // CHI TIẾT
        // =========================

        public IActionResult Details(int id)
        {
            var sanPham = _context.SanPhams
                .Include(x => x.MaDanhMucNavigation)
                .FirstOrDefault(x => x.MaSanPham == id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }


        public async Task<IActionResult> Categories()
        {
            var danhMucs = await _context.DanhMucs.ToListAsync();

            return View(danhMucs);
        }


        public async Task<IActionResult> CategoryProducts(int id)
        {
            var sanPhams = await _context.SanPhams
                .Include(x => x.MaDanhMucNavigation)
                .Where(x => x.MaDanhMuc == id)
                .OrderByDescending(x => x.MaSanPham)
                .ToListAsync();

            ViewBag.DanhMuc = await _context.DanhMucs.FindAsync(id);

            return View(sanPhams);
        }
    }
}