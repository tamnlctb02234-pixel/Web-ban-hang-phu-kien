using ASM1_SOF1022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASM1_SOF1022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public HomeController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // TRANG CHỦ
        // =========================

        public async Task<IActionResult> Index()
        {
            var sanPhams = await _context.SanPhams
                .Include(x => x.MaDanhMucNavigation)
                .Take(8)
                .ToListAsync();

            return View(sanPhams);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id
                        ?? HttpContext.TraceIdentifier
                });
        }
    }
}