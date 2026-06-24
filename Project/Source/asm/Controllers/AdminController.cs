// Controllers/AdminController.cs
// Chỉ dành cho Role = "Admin"
using asm.Filters;
using asm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asm.Controllers
{
    [RoleAuthorize("Admin")]
    public class AdminController : Controller
    {
        private readonly AsmPcContext _db;
        public AdminController(AsmPcContext db) => _db = db;

        // ─── DASHBOARD ────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var tongDoanhThu = await _db.HoaDons
                .Where(h => h.TrangThai == "Hoàn thành")
                .SumAsync(h => h.TongTien);

            var tongDonHang = await _db.HoaDons.CountAsync();
            var tongKhachHang = await _db.KhachHangs.CountAsync();
            var tongSanPham = await _db.SanPhams.CountAsync();

            return Json(new
            {
                success = true,
                tongDoanhThu,
                tongDonHang,
                tongKhachHang,
                tongSanPham
            });
        }

        // ─── QUẢN LÝ NHÂN VIÊN ───────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetNhanViens()
        {
            var list = await _db.NhanViens
                .Select(n => new
                {
                    n.MaNv,
                    n.HoTen,
                    n.Email,
                    n.TenTk,
                    n.ChucVu,
                    n.IsAdministrator,
                    n.TrangThai,
                    n.NgayTao
                }).ToListAsync();

            return Json(new { success = true, data = list });
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhanVien([FromBody] NhanVienRequest req)
        {
            if (_db.NhanViens.Any(n => n.TenTk == req.TenTk))
                return Json(new { success = false, message = "Tên tài khoản đã tồn tại!" });

            if (_db.NhanViens.Any(n => n.Email == req.Email))
                return Json(new { success = false, message = "Email đã được sử dụng!" });

            var nv = new NhanVien
            {
                HoTen = req.HoTen,
                Email = req.Email,
                TenTk = req.TenTk,
                MatKhau = req.MatKhau,
                ChucVu = req.ChucVu,
                IsAdministrator = req.IsAdministrator,
                TrangThai = true,
                NgayTao = DateTime.Now
            };

            _db.NhanViens.Add(nv);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Tạo nhân viên thành công!", maNv = nv.MaNv });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNhanVien([FromBody] NhanVienUpdateRequest req)
        {
            var nv = await _db.NhanViens.FindAsync(req.MaNv);
            if (nv == null)
                return Json(new { success = false, message = "Không tìm thấy nhân viên!" });

            nv.HoTen = req.HoTen ?? nv.HoTen;
            nv.ChucVu = req.ChucVu ?? nv.ChucVu;
            nv.IsAdministrator = req.IsAdministrator ?? nv.IsAdministrator;
            nv.TrangThai = req.TrangThai ?? nv.TrangThai;

            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleNhanVien([FromBody] IdRequest req)
        {
            var nv = await _db.NhanViens.FindAsync(req.Id);
            if (nv == null)
                return Json(new { success = false, message = "Không tìm thấy!" });

            nv.TrangThai = !nv.TrangThai;
            await _db.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = nv.TrangThai == true ? "Đã kích hoạt tài khoản!" : "Đã vô hiệu hóa tài khoản!",
                trangThai = nv.TrangThai
            });
        }

        // ─── QUẢN LÝ KHÁCH HÀNG ──────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetKhachHangs(int page = 1, int pageSize = 20)
        {
            var query = _db.KhachHangs.AsQueryable();
            var total = await query.CountAsync();

            var list = await query
                .OrderByDescending(k => k.NgayTao)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(k => new
                {
                    k.MaKh,
                    k.Username,
                    k.HoTen,
                    k.Email,
                    k.Sdt,
                    k.TrangThai,
                    k.NgayTao
                }).ToListAsync();

            return Json(new { success = true, data = list, total, page, pageSize });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleKhachHang([FromBody] IdRequest req)
        {
            var kh = await _db.KhachHangs.FindAsync(req.Id);
            if (kh == null)
                return Json(new { success = false, message = "Không tìm thấy!" });

            kh.TrangThai = !kh.TrangThai;
            await _db.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = kh.TrangThai == true ? "Đã mở khóa tài khoản!" : "Đã khóa tài khoản!",
                trangThai = kh.TrangThai
            });
        }

        // ─── QUẢN LÝ ĐƠN HÀNG ────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetHoaDons(string? trangThai = null, int page = 1, int pageSize = 20)
        {
            var query = _db.HoaDons
                .Include(h => h.MaKhNavigation)
                .AsQueryable();

            if (!string.IsNullOrEmpty(trangThai))
                query = query.Where(h => h.TrangThai == trangThai);

            var total = await query.CountAsync();

            var list = await query
                .OrderByDescending(h => h.NgayLap)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(h => new
                {
                    h.MaHd,
                    h.TongTien,
                    h.TrangThai,
                    h.NgayLap,
                    khachHang = h.MaKhNavigation == null ? null : new
                    {
                        h.MaKhNavigation.HoTen,
                        h.MaKhNavigation.Email
                    }
                }).ToListAsync();

            return Json(new { success = true, data = list, total, page, pageSize });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHoaDon([FromBody] UpdateHoaDonRequest req)
        {
            var validStatuses = new[] { "Chờ xác nhận", "Đang xử lý", "Đang giao", "Hoàn thành", "Đã hủy" };
            if (!validStatuses.Contains(req.TrangThai))
                return Json(new { success = false, message = "Trạng thái không hợp lệ!" });

            var hd = await _db.HoaDons.FindAsync(req.MaHd);
            if (hd == null)
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });

            hd.TrangThai = req.TrangThai;
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = $"Đã cập nhật trạng thái: {req.TrangThai}", trangThai = req.TrangThai });
        }

        [HttpGet]
        public async Task<IActionResult> GetHoaDonDetail(int id)
        {
            var hd = await _db.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.MaSpNavigation)
                .Include(h => h.ThanhToans)
                .FirstOrDefaultAsync(h => h.MaHd == id);

            if (hd == null)
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });

            return Json(new
            {
                success = true,
                data = new
                {
                    hd.MaHd,
                    hd.TongTien,
                    hd.TrangThai,
                    hd.NgayLap,
                    khachHang = hd.MaKhNavigation == null ? null : new
                    {
                        hd.MaKhNavigation.HoTen,
                        hd.MaKhNavigation.Email,
                        hd.MaKhNavigation.Sdt
                    },
                    chiTiet = hd.ChiTietHoaDons.Select(c => new
                    {
                        tenSp = c.MaSpNavigation != null ? c.MaSpNavigation.TenSp : $"SP #{c.MaSp}",
                        c.SoLuong,
                        c.DonGia,
                        thanhTien = c.SoLuong * c.DonGia
                    }),
                    thanhToan = hd.ThanhToans.Select(t => new
                    {
                        t.PhuongThuc,
                        t.SoTien,
                        t.TrangThai,
                        t.NgayThanhToan
                    })
                }
            });
        }
    }

    // ─── DTOs Admin ───────────────────────────────────────────────────────────
    public class NhanVienRequest
    {
        public string HoTen { get; set; } = "";
        public string Email { get; set; } = "";
        public string TenTk { get; set; } = "";
        public string MatKhau { get; set; } = "";
        public string? ChucVu { get; set; }
        public bool IsAdministrator { get; set; } = false;
    }

    public class NhanVienUpdateRequest
    {
        public int MaNv { get; set; }
        public string? HoTen { get; set; }
        public string? ChucVu { get; set; }
        public bool? IsAdministrator { get; set; }
        public bool? TrangThai { get; set; }
    }

    public class UpdateHoaDonRequest
    {
        public int MaHd { get; set; }
        public string TrangThai { get; set; } = "";
    }

    public class IdRequest
    {
        public int Id { get; set; }
    }
}