// Controllers/OrderController.cs
// Khách hàng xem đơn hàng của CHÍNH MÌNH — filter 100% ở server theo session
using asm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asm.Controllers
{
    public class OrderController : Controller
    {
        private readonly AsmPcContext _db;
        public OrderController(AsmPcContext db) => _db = db;

        // Lấy MaKH từ session — null = chưa đăng nhập
        private int? CurrentMaKh =>
            int.TryParse(HttpContext.Session.GetString("MaKH"), out var id) ? id : null;

        // ── GET /Order/MyOrders ──────────────────────────────────────────────
        // Chỉ trả về đơn hàng của tài khoản đang đăng nhập (WHERE MaKH = session)
        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            var maKh = CurrentMaKh;
            if (maKh == null)
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });

            var orders = await _db.HoaDons
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.MaSpNavigation)
                .Include(h => h.ThanhToans)
                .Include(h => h.MaDcNavigation)
                .Where(h => h.MaKh == maKh)          // ← filter server-side
                .OrderByDescending(h => h.NgayLap)
                .ToListAsync();

            var result = orders.Select(h => new
            {
                h.MaHd,
                h.TongTien,
                h.TrangThai,
                h.NgayLap,
                diaChi = h.MaDcNavigation == null ? null : new
                {
                    h.MaDcNavigation.HoTenNguoiNhan,
                    h.MaDcNavigation.Sdt,
                    h.MaDcNavigation.DiaChiDay,
                    h.MaDcNavigation.TinhThanh,
                    h.MaDcNavigation.QuanHuyen
                },
                phuongThucThanhToan = h.ThanhToans.FirstOrDefault()!.PhuongThuc,
                trangThaiThanhToan  = h.ThanhToans.FirstOrDefault()!.TrangThai,
                chiTiet = h.ChiTietHoaDons.Select(c => new
                {
                    c.MaSp,
                    tenSp     = c.MaSpNavigation!.TenSp,
                    c.SoLuong,
                    c.DonGia,
                    thanhTien = c.SoLuong * c.DonGia
                }).ToList()
            }).ToList();

            return Json(new { success = true, orders = result });
        }

        // ── GET /Order/Detail?id=5 ───────────────────────────────────────────
        // Chi tiết 1 đơn — server xác minh đơn phải thuộc về MaKH trong session
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var maKh = CurrentMaKh;
            if (maKh == null)
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });

            var hd = await _db.HoaDons
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.MaSpNavigation)
                .Include(h => h.ThanhToans)
                .Include(h => h.MaDcNavigation)
                // ← WHERE cả MaHD và MaKH → không thể xem đơn của người khác
                .FirstOrDefaultAsync(h => h.MaHd == id && h.MaKh == maKh);

            if (hd == null)
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });

            return Json(new
            {
                success = true,
                order = new
                {
                    hd.MaHd,
                    hd.TongTien,
                    hd.TrangThai,
                    hd.NgayLap,
                    buocHienTai = new[] { "Chờ xác nhận","Đang xử lý","Đang giao","Hoàn thành" }
                        .ToList().IndexOf(hd.TrangThai),
                    diaChi = hd.MaDcNavigation == null ? null : new
                    {
                        hd.MaDcNavigation.HoTenNguoiNhan,
                        hd.MaDcNavigation.Sdt,
                        hd.MaDcNavigation.DiaChiDay,
                        hd.MaDcNavigation.TinhThanh,
                        hd.MaDcNavigation.QuanHuyen
                    },
                    chiTiet = hd.ChiTietHoaDons.Select(c => new
                    {
                        c.MaSp,
                        tenSp     = c.MaSpNavigation!.TenSp,
                        c.SoLuong,
                        c.DonGia,
                        thanhTien = c.SoLuong * c.DonGia
                    }).ToList(),
                    thanhToan = hd.ThanhToans.Select(t => new
                    {
                        t.PhuongThuc,
                        t.SoTien,
                        t.TrangThai,
                        t.NgayThanhToan
                    }).ToList()
                }
            });
        }
    }
}
