// Controllers/CartController.cs
using asm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asm.Controllers
{
    public class CartController : Controller
    {
        private readonly AsmPcContext _db;
        public CartController(AsmPcContext db) => _db = db;

        private int? GetCurrentMaKh()
        {
            var val = HttpContext.Session.GetString("MaKH");
            return int.TryParse(val, out var id) ? id : null;
        }

        private async Task<GioHang> GetOrCreateCartAsync(int maKh)
        {
            var cart = await _db.GioHangs
                .Include(g => g.ChiTietGioHangs)
                    .ThenInclude(c => c.MaSpNavigation)
                        .ThenInclude(s => s!.AnhSanPhams)
                .FirstOrDefaultAsync(g => g.MaKh == maKh);

            if (cart == null)
            {
                cart = new GioHang { MaKh = maKh };
                _db.GioHangs.Add(cart);
                await _db.SaveChangesAsync();
            }
            return cart;
        }

        // ── GET /Cart/GetCart ────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var maKh = GetCurrentMaKh();
            if (maKh == null)
                return Json(new { success = false, message = "Chưa đăng nhập", items = Array.Empty<object>(), total = 0, soLuong = 0 });

            var cart = await GetOrCreateCartAsync(maKh.Value);

            var items = cart.ChiTietGioHangs.Select(c => new
            {
                maCtGh = c.MaCtgh,
                maSp = c.MaSp,
                tenSp = c.MaSpNavigation?.TenSp,
                dm = c.MaSpNavigation?.MaDanhMuc,
                soLuong = c.SoLuong,
                donGia = c.DonGia,
                thanhTien = c.SoLuong * c.DonGia,
            }).ToList();

            return Json(new
            {
                success = true,
                items,
                total = items.Sum(i => i.thanhTien),
                soLuong = items.Sum(i => i.soLuong)
            });
        }

        // ── POST /Cart/AddToCart ─────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest req)
        {
            var maKh = GetCurrentMaKh();
            if (maKh == null)
                return Json(new { success = false, message = "Vui lòng đăng nhập để thêm vào giỏ hàng!" });

            var sanPham = await _db.SanPhams.FindAsync(req.MaSp);
            if (sanPham == null || sanPham.TrangThai == false)
                return Json(new { success = false, message = "Sản phẩm không tồn tại!" });

            if (sanPham.SoLuongTon < req.SoLuong)
                return Json(new { success = false, message = $"Chỉ còn {sanPham.SoLuongTon} sản phẩm trong kho!" });

            var cart = await GetOrCreateCartAsync(maKh.Value);

            // Kiểm tra đã có trong giỏ chưa
            var existing = await _db.ChiTietGioHangs
                .FirstOrDefaultAsync(c => c.MaGh == cart.MaGh && c.MaSp == req.MaSp);

            if (existing != null)
            {
                // UPDATE số lượng bằng raw SQL để tránh vấn đề ValueGeneratedOnAdd
                await _db.Database.ExecuteSqlRawAsync(
                    "UPDATE ChiTietGioHang SET SoLuong = SoLuong + {0}, DonGia = {1} WHERE MaCTGH = {2}",
                    req.SoLuong, sanPham.GiaBan, existing.MaCtgh);
            }
            else
            {
                // INSERT bằng raw SQL để tránh computed column ThanhTien và ValueGeneratedOnAdd
                await _db.Database.ExecuteSqlRawAsync(
                    "INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong, DonGia) VALUES ({0}, {1}, {2}, {3})",
                    cart.MaGh, req.MaSp, req.SoLuong, sanPham.GiaBan);
            }

            var tongSoLuong = await _db.ChiTietGioHangs
                .Where(c => c.MaGh == cart.MaGh)
                .SumAsync(c => c.SoLuong);

            return Json(new { success = true, message = $"Đã thêm \"{sanPham.TenSp}\" vào giỏ hàng!", tongSoLuong });
        }

        // ── POST /Cart/UpdateQuantity ────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartRequest req)
        {
            var maKh = GetCurrentMaKh();
            if (maKh == null)
                return Json(new { success = false, message = "Chưa đăng nhập!" });

            // Lấy item và xác minh thuộc về đúng user
            var item = await _db.ChiTietGioHangs
                .Include(c => c.MaGhNavigation)
                .Include(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(c => c.MaCtgh == req.MaCtGh && c.MaGhNavigation.MaKh == maKh);

            if (item == null)
                return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });

            if (req.SoLuong <= 0)
            {
                await _db.Database.ExecuteSqlRawAsync(
                    "DELETE FROM ChiTietGioHang WHERE MaCTGH = {0}", req.MaCtGh);
            }
            else
            {
                if (item.MaSpNavigation!.SoLuongTon < req.SoLuong)
                    return Json(new { success = false, message = $"Chỉ còn {item.MaSpNavigation.SoLuongTon} trong kho!" });

                await _db.Database.ExecuteSqlRawAsync(
                    "UPDATE ChiTietGioHang SET SoLuong = {0} WHERE MaCTGH = {1}",
                    req.SoLuong, req.MaCtGh);
            }

            // Tính lại tổng
            var maGh = item.MaGhNavigation.MaGh;
            var items = await _db.ChiTietGioHangs
                .Where(c => c.MaGh == maGh)
                .ToListAsync();

            return Json(new
            {
                success = true,
                total = items.Sum(c => c.SoLuong * c.DonGia),
                tongSoLuong = items.Sum(c => c.SoLuong)
            });
        }

        // ── POST /Cart/RemoveItem ────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveCartRequest req)
        {
            var maKh = GetCurrentMaKh();
            if (maKh == null)
                return Json(new { success = false, message = "Chưa đăng nhập!" });

            var item = await _db.ChiTietGioHangs
                .Include(c => c.MaGhNavigation)
                .FirstOrDefaultAsync(c => c.MaCtgh == req.MaCtGh && c.MaGhNavigation.MaKh == maKh);

            if (item == null)
                return Json(new { success = false, message = "Không tìm thấy!" });

            var maGh = item.MaGhNavigation.MaGh;

            await _db.Database.ExecuteSqlRawAsync(
                "DELETE FROM ChiTietGioHang WHERE MaCTGH = {0}", req.MaCtGh);

            var items = await _db.ChiTietGioHangs
                .Where(c => c.MaGh == maGh)
                .ToListAsync();

            return Json(new
            {
                success = true,
                total = items.Sum(c => c.SoLuong * c.DonGia),
                tongSoLuong = items.Sum(c => c.SoLuong)
            });
        }

        // ── POST /Cart/Checkout ──────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest req)
        {
            var maKh = GetCurrentMaKh();
            if (maKh == null)
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });

            // Load giỏ hàng
            var cart = await _db.GioHangs
                .Include(g => g.ChiTietGioHangs)
                    .ThenInclude(c => c.MaSpNavigation)
                .FirstOrDefaultAsync(g => g.MaKh == maKh);

            if (cart == null || !cart.ChiTietGioHangs.Any())
                return Json(new { success = false, message = "Giỏ hàng đang trống!" });

            // Lấy địa chỉ (nullable)
            int? maDc = null;
            if (req.MaDc > 0)
            {
                var dc = await _db.DiaChis.FirstOrDefaultAsync(d => d.MaDc == req.MaDc && d.MaKh == maKh);
                maDc = dc?.MaDc;
            }
            if (maDc == null)
            {
                var dc = await _db.DiaChis.FirstOrDefaultAsync(d => d.MaKh == maKh && d.MacDinh == true)
                      ?? await _db.DiaChis.FirstOrDefaultAsync(d => d.MaKh == maKh);
                maDc = dc?.MaDc;
            }

            // Kiểm tra tồn kho
            foreach (var item in cart.ChiTietGioHangs)
            {
                if (item.MaSpNavigation == null)
                    return Json(new { success = false, message = "Sản phẩm không hợp lệ!" });
                if (item.MaSpNavigation.SoLuongTon < item.SoLuong)
                    return Json(new { success = false, message = $"\"{item.MaSpNavigation.TenSp}\" chỉ còn {item.MaSpNavigation.SoLuongTon} trong kho!" });
            }

            var tongTien = cart.ChiTietGioHangs.Sum(c => c.SoLuong * c.DonGia);

            // 1. INSERT HoaDon — dùng EF bình thường (không có computed column)
            var hoaDon = new HoaDon
            {
                MaKh = maKh.Value,
                MaDc = maDc,
                TongTien = tongTien,
                TrangThai = "Chờ xác nhận"
            };
            _db.HoaDons.Add(hoaDon);
            await _db.SaveChangesAsync(); // cần MaHd trước

            // 2. INSERT ChiTietHoaDon bằng raw SQL (tránh computed column ThanhTien + ValueGeneratedOnAdd SoLuong)
            foreach (var item in cart.ChiTietGioHangs)
            {
                await _db.Database.ExecuteSqlRawAsync(
                    "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia) VALUES ({0}, {1}, {2}, {3})",
                    hoaDon.MaHd, item.MaSp, item.SoLuong, item.DonGia);

                // Trừ kho
                await _db.Database.ExecuteSqlRawAsync(
                    "UPDATE SanPham SET SoLuongTon = SoLuongTon - {0} WHERE MaSP = {1}",
                    item.SoLuong, item.MaSp);
            }

            // 3. INSERT ThanhToan — primary key là MaGD (auto), không set
            await _db.Database.ExecuteSqlRawAsync(
                "INSERT INTO ThanhToan (MaHD, PhuongThuc, SoTien, TrangThai) VALUES ({0}, {1}, {2}, {3})",
                hoaDon.MaHd, req.PhuongThuc ?? "COD", tongTien, "Chờ thanh toán");

            // 4. Xóa giỏ hàng bằng raw SQL
            await _db.Database.ExecuteSqlRawAsync(
                "DELETE FROM ChiTietGioHang WHERE MaGH = {0}", cart.MaGh);

            return Json(new
            {
                success = true,
                message = "Đặt hàng thành công!",
                maHd = hoaDon.MaHd,
                tongTien
            });
        }
    }

    // ── DTOs ──────────────────────────────────────────────────────────────────
    public class AddToCartRequest
    {
        public int MaSp { get; set; }
        public int SoLuong { get; set; } = 1;
    }
    public class UpdateCartRequest
    {
        public int MaCtGh { get; set; }
        public int SoLuong { get; set; }
    }
    public class RemoveCartRequest
    {
        public int MaCtGh { get; set; }
    }
    public class CheckoutRequest
    {
        public int MaDc { get; set; }
        public string? PhuongThuc { get; set; }
    }
}