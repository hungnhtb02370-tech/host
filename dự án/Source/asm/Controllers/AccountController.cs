// Controllers/AccountController.cs
// Thêm đăng nhập bằng Session, hỗ trợ 3 role: KhachHang | NhanVien | Admin
using asm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace asm.Controllers
{
    public class AccountController : Controller
    {
        private readonly AsmPcContext _db;

        public AccountController(AsmPcContext db)
        {
            _db = db;
        }

        // ─── ĐĂNG KÝ ─────────────────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (_db.KhachHangs.Any(k => k.Email == req.Email))
                return Json(new { success = false, message = "Email đã được sử dụng!" });

            if (_db.KhachHangs.Any(k => k.Username == req.Username))
                return Json(new { success = false, message = "Username đã tồn tại!" });

            var khachHang = new KhachHang
            {
                Username = req.Username,
                Email = req.Email,
                HoTen = req.HoTen,
                MatKhau = HashPassword(req.MatKhau),
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            _db.KhachHangs.Add(khachHang);
            await _db.SaveChangesAsync();

            // Tạo giỏ hàng ngay khi đăng ký
            _db.GioHangs.Add(new GioHang { MaKh = khachHang.MaKh });
            await _db.SaveChangesAsync();

            // Ghi session
            SetKhachHangSession(khachHang);

            return Json(new
            {
                success = true,
                message = $"Đăng ký thành công! Xin chào {khachHang.HoTen}",
                user = BuildUserDto(khachHang)
            });
        }

        // ─── ĐĂNG NHẬP KHÁCH HÀNG ────────────────────────────────────────────
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var hashed = HashPassword(req.MatKhau);
            var user = _db.KhachHangs
                .FirstOrDefault(k => k.Email == req.Email && k.MatKhau == hashed);

            if (user == null)
                return Json(new { success = false, message = "Email hoặc mật khẩu không đúng!" });

            if (user.TrangThai == false)
                return Json(new { success = false, message = "Tài khoản đã bị khóa!" });

            SetKhachHangSession(user);

            return Json(new
            {
                success = true,
                message = $"Xin chào {user.HoTen}!",
                user = BuildUserDto(user)
            });
        }

        // ─── ĐĂNG NHẬP NHÂN VIÊN / ADMIN ─────────────────────────────────────
        [HttpPost]
        public IActionResult LoginNhanVien([FromBody] LoginNhanVienRequest req)
        {
            // Nhân viên lưu mật khẩu thẳng (theo DB hiện tại)
            var nv = _db.NhanViens
                .FirstOrDefault(n => n.TenTk == req.TenTk && n.MatKhau == req.MatKhau);

            if (nv == null)
                return Json(new { success = false, message = "Tài khoản hoặc mật khẩu không đúng!" });

            if (nv.TrangThai == false)
                return Json(new { success = false, message = "Tài khoản đã bị vô hiệu hóa!" });

            // Xác định role
            var role = nv.IsAdministrator ? "Admin" : "NhanVien";
            SetNhanVienSession(nv, role);

            return Json(new
            {
                success = true,
                message = $"Xin chào {nv.HoTen} ({role})!",
                user = new
                {
                    nv.MaNv,
                    nv.TenTk,
                    nv.HoTen,
                    nv.Email,
                    Role = role
                }
            });
        }

        // ─── ĐĂNG XUẤT ───────────────────────────────────────────────────────
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(new { success = true, message = "Đã đăng xuất!" });
        }

        // ─── LẤY THÔNG TIN USER HIỆN TẠI ────────────────────────────────────
        [HttpGet]
        public IActionResult Me()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == null)
                return Json(new { loggedIn = false });

            return Json(new
            {
                loggedIn = true,
                role,
                maKh = HttpContext.Session.GetString("MaKH"),
                maNv = HttpContext.Session.GetString("MaNV"),
                hoTen = HttpContext.Session.GetString("HoTen"),
                email = HttpContext.Session.GetString("Email")
            });
        }

        // ─── Helpers ─────────────────────────────────────────────────────────
        private void SetKhachHangSession(KhachHang kh)
        {
            HttpContext.Session.SetString("Role", "KhachHang");
            HttpContext.Session.SetString("MaKH", kh.MaKh.ToString());
            HttpContext.Session.SetString("HoTen", kh.HoTen ?? "");
            HttpContext.Session.SetString("Email", kh.Email);
        }

        private void SetNhanVienSession(NhanVien nv, string role)
        {
            HttpContext.Session.SetString("Role", role);
            HttpContext.Session.SetString("MaNV", nv.MaNv.ToString());
            HttpContext.Session.SetString("HoTen", nv.HoTen);
            HttpContext.Session.SetString("Email", nv.Email);
        }

        private static object BuildUserDto(KhachHang kh) => new
        {
            kh.MaKh,
            kh.Username,
            kh.HoTen,
            kh.Email,
            Role = "KhachHang"
        };

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }
    }

    // ─── DTOs ─────────────────────────────────────────────────────────────────
    public class RegisterRequest
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string HoTen { get; set; } = "";
        public string MatKhau { get; set; } = "";
    }

    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string MatKhau { get; set; } = "";
    }

    public class LoginNhanVienRequest
    {
        public string TenTk { get; set; } = "";
        public string MatKhau { get; set; } = "";
    }
}