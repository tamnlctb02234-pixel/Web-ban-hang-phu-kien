using Microsoft.AspNetCore.Mvc;
using ASM1_SOF1022.Models;

namespace ASM1_SOF1022.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopPhuKienDbContext _context;

        public AccountController(ShopPhuKienDbContext context)
        {
            _context = context;
        }

        // =========================
        // ĐĂNG KÝ
        // =========================
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                kh.MaVaiTro = 2;

                _context.KhachHangs.Add(kh);

                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View(kh);
        }

        // =========================
        // ĐĂNG NHẬP
        // =========================
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string matKhau)
        {
            var user = _context.KhachHangs
                .FirstOrDefault(x =>
                    x.Email == email &&
                    x.MatKhau == matKhau);

            if (user != null)
            {
                HttpContext.Session.SetString(
                    "UserName",
                    user.HoTen
                );

                HttpContext.Session.SetInt32(
                    "UserID",
                    user.MaKhachHang
                );

                // THÊM DÒNG NÀY
                HttpContext.Session.SetInt32(
                    "RoleID",
                    user.MaVaiTro ?? 0
                );

                return RedirectToAction(
                    "Index",
                    "Home"
                );
            }

            ViewBag.Error = "Sai email hoặc mật khẩu";

            return View();
        }

        // =========================
        // ĐĂNG XUẤT
        // =========================
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                "Index",
                "Home"
            );
        }
    }
}