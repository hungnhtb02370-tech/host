// Controllers/NhanVienController.cs
using asm.Filters;
using asm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asm.Controllers
{
    // BƯỚC 1: Comment dòng phân quyền lại để ai cũng gọi được API
    // [RoleAuthorize("NhanVien", "Admin")] 
    public class NhanVienController : Controller
    {
        private readonly AsmPcContext _db;
        public NhanVienController(AsmPcContext db) => _db = db;

        // BƯỚC 2: Fix cứng trả về MaNV = 1 (Tài khoản Admin) 
        // để khi Insert/Update vào SQL không bị lỗi khóa ngoại (Foreign Key) do Session rỗng
        private int GetMaNv() => 1;

        // ─── DANH MỤC & NHÀ CUNG CẤP (dùng cho form) ─────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetDanhMucs()
        {
            var list = await _db.DanhMucs
                .OrderBy(d => d.ThuTu)
                .Select(d => new { d.MaDanhMuc, d.TenDanhMuc, d.Icon })
                .ToListAsync();
            return Json(new { success = true, data = list });
        }

        // ... (GIỮ NGUYÊN TOÀN BỘ CÁC HÀM BÊN DƯỚI NHƯ CŨ) ...

        // ─── SẢN PHẨM ─────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetSanPhams(int? maDanhMuc = null, int page = 1, int pageSize = 20)
        {
            var query = _db.SanPhams
                .Include(s => s.AnhSanPhams)
                .Include(s => s.MaDanhMucNavigation)
                .AsQueryable();

            if (maDanhMuc.HasValue)
                query = query.Where(s => s.MaDanhMuc == maDanhMuc.Value);

            var total = await query.CountAsync();

            var list = await query
                .OrderByDescending(s => s.MaSp)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new
                {
                    s.MaSp,
                    s.TenSp,
                    s.ThuongHieu,
                    s.GiaBan,
                    s.SoLuongTon,
                    s.TrangThai,
                    danhMuc = s.MaDanhMucNavigation.TenDanhMuc,
                    anhChinh = s.AnhSanPhams.FirstOrDefault(a => a.LaAnhChinh) != null
                               ? s.AnhSanPhams.FirstOrDefault(a => a.LaAnhChinh)!.DuongDan
                               : s.AnhSanPhams.FirstOrDefault() != null
                               ? s.AnhSanPhams.First().DuongDan : null
                }).ToListAsync();

            return Json(new { success = true, data = list, total, page, pageSize });
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateSanPham([FromBody] SanPhamRequest req)
        {
            try
            {
                var sp = new SanPham
                {
                    MaDanhMuc = req.MaDanhMuc,
                    MaNcc = req.MaNcc,
                    TenSp = req.TenSp,
                    ThuongHieu = req.ThuongHieu,
                    GiaBan = req.GiaBan,
                    GiaNhap = req.GiaNhap,
                    SoLuongTon = req.SoLuongTon,
                    MoTa = req.MoTa,
                    ThongSoKyThuat = req.ThongSoKyThuat,
                    TrangThai = true,
                    NgayNhap = DateTime.Now,
                    MaNvquanLy = GetMaNv() // Fix cứng là 1 theo code của bạn
                };

                _db.SanPhams.Add(sp);
                await _db.SaveChangesAsync();

                return Json(new { success = true, message = "Thêm sản phẩm vào SQL thành công!", maSp = sp.MaSp });
            }
            catch (Exception ex)
            {
                // In chính xác lỗi SQL ra màn hình
                string errorMsg = ex.InnerException?.Message ?? ex.Message;
                return Json(new { success = false, message = "Lỗi SQL: " + errorMsg });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSanPham([FromBody] SanPhamUpdateRequest req)
        {
            try
            {
                var sp = await _db.SanPhams.FindAsync(req.MaSp);
                if (sp == null)
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

                sp.TenSp = req.TenSp ?? sp.TenSp;
                sp.GiaBan = req.GiaBan ?? sp.GiaBan;
                sp.GiaNhap = req.GiaNhap ?? sp.GiaNhap;
                sp.SoLuongTon = req.SoLuongTon ?? sp.SoLuongTon;
                sp.MoTa = req.MoTa ?? sp.MoTa;
                sp.ThongSoKyThuat = req.ThongSoKyThuat ?? sp.ThongSoKyThuat;
                sp.TrangThai = req.TrangThai ?? sp.TrangThai;
                sp.MaNvquanLy = GetMaNv();

                await _db.SaveChangesAsync();
                return Json(new { success = true, message = "Cập nhật SQL thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi SQL: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSanPham([FromBody] IdRequest req)
        {
            try
            {
                var sp = await _db.SanPhams.FindAsync(req.Id);
                if (sp == null)
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm!" });

                // Soft delete
                sp.TrangThai = false;
                await _db.SaveChangesAsync();

                return Json(new { success = true, message = "Đã ẩn sản phẩm trong SQL!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi SQL: " + (ex.InnerException?.Message ?? ex.Message) });
            }
        }

        // ─── ĐƠN HÀNG ─────────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetDonHangs(string? trangThai = null, int page = 1, int pageSize = 20)
        {
            var query = _db.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.MaSpNavigation)
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
                    khachHang = new { h.MaKhNavigation!.HoTen, h.MaKhNavigation.Email },
                    soSanPham = h.ChiTietHoaDons.Count
                }).ToListAsync();

            return Json(new { success = true, data = list, total, page, pageSize });
        }

        [HttpGet]
        public async Task<IActionResult> GetDonHangDetail(int id)
        {
            var hd = await _db.HoaDons
                .Include(h => h.MaKhNavigation)
                .Include(h => h.MaDcNavigation)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.MaSpNavigation)
                        .ThenInclude(s => s!.AnhSanPhams)
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
                    khachHang = new { hd.MaKhNavigation!.HoTen, hd.MaKhNavigation.Email, hd.MaKhNavigation.Sdt },
                    diaChi = hd.MaDcNavigation == null ? null : new
                    {
                        hd.MaDcNavigation.DiaChiDay,
                        hd.MaDcNavigation.TinhThanh,
                        hd.MaDcNavigation.QuanHuyen
                    },
                    chiTiet = hd.ChiTietHoaDons.Select(c => new
                    {
                        c.MaSp,
                        tenSp = c.MaSpNavigation!.TenSp,
                        c.SoLuong,
                        c.DonGia,
                        thanhTien = c.SoLuong * c.DonGia,
                        anh = c.MaSpNavigation.AnhSanPhams
                                      .FirstOrDefault(a => a.LaAnhChinh)?.DuongDan
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

        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThaiDonHang([FromBody] UpdateHoaDonRequest req)
        {
            var hd = await _db.HoaDons.FindAsync(req.MaHd);
            if (hd == null)
                return Json(new { success = false, message = "Không tìm thấy đơn hàng!" });

            var trangThaiHopLe = new[] { "Chờ xác nhận", "Đang xử lý", "Đang giao", "Hoàn thành", "Đã hủy" };
            if (!trangThaiHopLe.Contains(req.TrangThai))
                return Json(new { success = false, message = "Trạng thái không hợp lệ!" });

            hd.TrangThai = req.TrangThai;
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = $"Đã cập nhật: {req.TrangThai}" });
        }
    }

    // ─── DTOs NhanVien ────────────────────────────────────────────────────────
    public class SanPhamRequest
    {
        public int MaDanhMuc { get; set; }
        public int? MaNcc { get; set; }
        public string TenSp { get; set; } = "";
        public string? ThuongHieu { get; set; }
        public decimal GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public int SoLuongTon { get; set; }
        public string? MoTa { get; set; }
        public string? ThongSoKyThuat { get; set; }
    }

    public class SanPhamUpdateRequest
    {
        public int MaSp { get; set; }
        public string? TenSp { get; set; }
        public decimal? GiaBan { get; set; }
        public decimal? GiaNhap { get; set; }
        public int? SoLuongTon { get; set; }
        public string? MoTa { get; set; }
        public string? ThongSoKyThuat { get; set; }
        public bool? TrangThai { get; set; }
    }
}