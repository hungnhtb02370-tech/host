-- ==============================================================
--  PCPartPicker Clone - SQL Server Schema
--  Đồ án môn học - Hoàn chỉnh, đã sửa toàn bộ lỗi
-- ==============================================================

-- ==============================================================
--  HƯỚNG DẪN CHẠY FILE NÀY:
--  1. Mở SSMS, kết nối server
--  2. Dropdown database trên toolbar chọn "asm_pc"
--     (nếu chưa có thì tạo trước: chuột phải Databases > New Database)
--  3. Nhấn F5 để chạy toàn bộ file
-- ==============================================================
create database asm_pc;
USE asm_pc;
GO

-- Xóa bảng theo đúng thứ tự FK (bảng con trước, bảng cha sau)
DROP TABLE IF EXISTS ChiTietThongBao;
DROP TABLE IF EXISTS ThongBao;
DROP TABLE IF EXISTS DanhGia;
DROP TABLE IF EXISTS ThanhToan;
DROP TABLE IF EXISTS ChiTietHoaDon;
DROP TABLE IF EXISTS HoaDon;
DROP TABLE IF EXISTS BaoCaoDoanhThu;
DROP TABLE IF EXISTS ChiTietGioHang;
DROP TABLE IF EXISTS GioHang;
DROP TABLE IF EXISTS ChiTietBuild;
DROP TABLE IF EXISTS Build;
DROP TABLE IF EXISTS GiaTungShop;
DROP TABLE IF EXISTS Benchmark;
DROP TABLE IF EXISTS TuongThich;
DROP TABLE IF EXISTS QuanLySanPham;
DROP TABLE IF EXISTS ChiTietDanhMucSP;
DROP TABLE IF EXISTS AnhSanPham;
DROP TABLE IF EXISTS SanPham;
DROP TABLE IF EXISTS Kho;
DROP TABLE IF EXISTS DanhMuc;
DROP TABLE IF EXISTS NhaCungCap;
DROP TABLE IF EXISTS DiaChi;
DROP TABLE IF EXISTS KhachHang;
DROP TABLE IF EXISTS ChiTietChucNang;
DROP TABLE IF EXISTS NhanVien;
DROP TABLE IF EXISTS ChucNang;
GO

-- ==============================================================
--  1. PHÂN QUYỀN & NHÂN VIÊN
-- ==============================================================

CREATE TABLE ChucNang (
    MaCN        INT PRIMARY KEY IDENTITY(1,1),
    TenChucNang NVARCHAR(100) NOT NULL,
    MoTa        NVARCHAR(255),
    TrangThai   BIT NOT NULL DEFAULT 1
);

CREATE TABLE NhanVien (
    MaNV            INT PRIMARY KEY IDENTITY(1,1),
    HoTen           NVARCHAR(100) NOT NULL,
    HinhAnh         NVARCHAR(255),
    GioiTinh        NVARCHAR(10),
    Luong           DECIMAL(15,2),
    SDT             NVARCHAR(15),
    Email           NVARCHAR(100) NOT NULL UNIQUE,
    CCCD            NVARCHAR(20),   -- filtered index bên dưới
    DiaChi          NVARCHAR(255),
    TenTK           NVARCHAR(50)  NOT NULL UNIQUE,
    MatKhau         NVARCHAR(50)  NOT NULL,
    IsAdministrator BIT NOT NULL DEFAULT 0,
    TrangThai       BIT NOT NULL DEFAULT 1,
    ChucVu          NVARCHAR(50),
    NgayTao         DATETIME NOT NULL DEFAULT GETDATE()
);

-- Cho phép nhiều NULL, chỉ ràng buộc UNIQUE khi CCCD có giá trị thực
CREATE UNIQUE INDEX UX_NhanVien_CCCD ON NhanVien (CCCD) WHERE CCCD IS NOT NULL;

CREATE TABLE ChiTietChucNang (
    MaCTCN       INT PRIMARY KEY IDENTITY(1,1),
    MaNV         INT NOT NULL REFERENCES NhanVien(MaNV) ON DELETE CASCADE,
    MaCN         INT NOT NULL REFERENCES ChucNang(MaCN) ON DELETE CASCADE,
    NgayCapQuyen DATETIME NOT NULL DEFAULT GETDATE(),
    TrangThai    BIT NOT NULL DEFAULT 1,
    UNIQUE (MaNV, MaCN)
);

-- ==============================================================
--  2. KHÁCH HÀNG
-- ==============================================================

CREATE TABLE KhachHang (
    MaKH      INT PRIMARY KEY IDENTITY(1,1),
    Username  NVARCHAR(50)  NOT NULL UNIQUE,
    Email     NVARCHAR(100) NOT NULL UNIQUE,
    MatKhau   NVARCHAR(MAX) NOT NULL,
    HoTen     NVARCHAR(100),
    SDT       NVARCHAR(15),
    CCCD      NVARCHAR(20),
    GioiTinh  NVARCHAR(10),
    NgayTao   DATETIME NOT NULL DEFAULT GETDATE(),
    TrangThai BIT NOT NULL DEFAULT 1
);

CREATE TABLE DiaChi (
    MaDC           INT PRIMARY KEY IDENTITY(1,1),
    MaKH           INT NOT NULL REFERENCES KhachHang(MaKH) ON DELETE CASCADE,
    HoTenNguoiNhan NVARCHAR(100),
    SDT            NVARCHAR(15),
    DiaChiDay      NVARCHAR(255) NOT NULL,
    TinhThanh      NVARCHAR(100),
    QuanHuyen      NVARCHAR(100),
    PhuongXa       NVARCHAR(100),
    MacDinh        BIT NOT NULL DEFAULT 0
);

-- ==============================================================
--  3. DANH MỤC & NHÀ CUNG CẤP
-- ==============================================================

CREATE TABLE NhaCungCap (
    MaNCC       INT PRIMARY KEY IDENTITY(1,1),
    TenNCC      NVARCHAR(100) NOT NULL,
    SoDienThoai NVARCHAR(15),
    Email       NVARCHAR(100),
    DiaChi      NVARCHAR(255),
    TrangThai   BIT NOT NULL DEFAULT 1
);

CREATE TABLE DanhMuc (
    MaDanhMuc  INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc NVARCHAR(100) NOT NULL,
    MoTa       NVARCHAR(255),
    Slug       NVARCHAR(100) NOT NULL UNIQUE,
    Icon       NVARCHAR(100),
    ThuTu      INT NOT NULL DEFAULT 0
);

-- ==============================================================
--  4. SẢN PHẨM & KHO
-- ==============================================================

CREATE TABLE Kho (
    MaKho   INT PRIMARY KEY IDENTITY(1,1),
    TenKho  NVARCHAR(100) NOT NULL,
    DiaChi  NVARCHAR(255),
    SucChua INT,
    GhiChu  NVARCHAR(255)
);

CREATE TABLE SanPham (
    MaSP           INT PRIMARY KEY IDENTITY(1,1),
    MaDanhMuc      INT NOT NULL REFERENCES DanhMuc(MaDanhMuc),
    MaNCC          INT REFERENCES NhaCungCap(MaNCC),
    TenSP          NVARCHAR(200) NOT NULL,
    ThuongHieu     NVARCHAR(100),
    GiaBan         DECIMAL(15,2) NOT NULL DEFAULT 0,
    GiaNhap        DECIMAL(15,2),
    NgayNhap       DATETIME,
    SoLuongTon     INT NOT NULL DEFAULT 0,
    TrangThai      BIT NOT NULL DEFAULT 1,
    ThongSoKyThuat NVARCHAR(MAX),
    MoTa           NVARCHAR(MAX),
    -- FIX: thêm ON DELETE SET NULL để tránh lỗi khi xóa nhân viên
    MaNVQuanLy     INT REFERENCES NhanVien(MaNV) ON DELETE SET NULL
);

CREATE TABLE AnhSanPham (
    MaAnh      INT PRIMARY KEY IDENTITY(1,1),
    MaSP       INT NOT NULL REFERENCES SanPham(MaSP) ON DELETE CASCADE,
    DuongDan   NVARCHAR(255) NOT NULL,
    LaAnhChinh BIT NOT NULL DEFAULT 0,
    ThuTu      INT NOT NULL DEFAULT 0
);

-- FIX: Filtered unique index đảm bảo mỗi sản phẩm chỉ có 1 ảnh chính
CREATE UNIQUE INDEX UX_AnhSanPham_AnhChinh
    ON AnhSanPham (MaSP)
    WHERE LaAnhChinh = 1;

CREATE TABLE ChiTietDanhMucSP (
    MaCTDM    INT PRIMARY KEY IDENTITY(1,1),
    MaDanhMuc INT NOT NULL REFERENCES DanhMuc(MaDanhMuc),
    MaSP      INT NOT NULL REFERENCES SanPham(MaSP) ON DELETE CASCADE,
    UNIQUE (MaDanhMuc, MaSP)
);

CREATE TABLE QuanLySanPham (
    MaQL       INT PRIMARY KEY IDENTITY(1,1),
    MaNV       INT NOT NULL REFERENCES NhanVien(MaNV),
    MaSP       INT NOT NULL REFERENCES SanPham(MaSP),
    MaKho      INT REFERENCES Kho(MaKho),
    NgayQuanLy DATETIME NOT NULL DEFAULT GETDATE(),
    VaiTro     NVARCHAR(50),
    GhiChu     NVARCHAR(255),
    UNIQUE (MaNV, MaSP)
);

-- ==============================================================
--  5. TÍNH NĂNG PCPARTPICKER
-- ==============================================================

CREATE TABLE TuongThich (
    MaTT       INT PRIMARY KEY IDENTITY(1,1),
    MaDanhMucA INT NOT NULL REFERENCES DanhMuc(MaDanhMuc),
    MaDanhMucB INT NOT NULL REFERENCES DanhMuc(MaDanhMuc),
    ThuocTinhA NVARCHAR(100) NOT NULL,
    ThuocTinhB NVARCHAR(100) NOT NULL,
    MoTa       NVARCHAR(255)
);

CREATE TABLE Build (
    MaBuild   INT PRIMARY KEY IDENTITY(1,1),
    MaKH      INT NOT NULL REFERENCES KhachHang(MaKH) ON DELETE CASCADE,
    TenBuild  NVARCHAR(200) NOT NULL,
    MoTa      NVARCHAR(MAX),
    TongGia   DECIMAL(15,2) NOT NULL DEFAULT 0,
    CongKhai  BIT NOT NULL DEFAULT 0,
    TongWatt  INT,
    LuotXem   INT NOT NULL DEFAULT 0,
    LuotThich INT NOT NULL DEFAULT 0,
    NgayTao   DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ChiTietBuild (
    MaCTB   INT PRIMARY KEY IDENTITY(1,1),
    MaBuild INT NOT NULL REFERENCES Build(MaBuild) ON DELETE CASCADE,
    MaSP    INT NOT NULL REFERENCES SanPham(MaSP),
    SoLuong INT NOT NULL DEFAULT 1,
    GhiChu  NVARCHAR(255),
    UNIQUE (MaBuild, MaSP)
);

CREATE TABLE Benchmark (
    MaBM       INT PRIMARY KEY IDENTITY(1,1),
    MaSP       INT NOT NULL REFERENCES SanPham(MaSP) ON DELETE CASCADE,
    LoaiBM     NVARCHAR(50) NOT NULL,
    CongCuTest NVARCHAR(100),
    DiemSo     DECIMAL(10,2),
    NgayTest   DATETIME NOT NULL DEFAULT GETDATE(),
    GhiChu     NVARCHAR(255)
);

CREATE TABLE GiaTungShop (
    MaGTS       INT PRIMARY KEY IDENTITY(1,1),
    MaSP        INT NOT NULL REFERENCES SanPham(MaSP) ON DELETE CASCADE,
    TenShop     NVARCHAR(100) NOT NULL,
    Gia         DECIMAL(15,2) NOT NULL,
    Link        NVARCHAR(500),
    NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE(),
    ConHang     BIT NOT NULL DEFAULT 1
);

-- ==============================================================
--  6. GIỎ HÀNG
-- ==============================================================

CREATE TABLE GioHang (
    MaGH    INT PRIMARY KEY IDENTITY(1,1),
    MaKH    INT NOT NULL REFERENCES KhachHang(MaKH) ON DELETE CASCADE UNIQUE,
    NgayTao DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE ChiTietGioHang (
    MaCTGH    INT PRIMARY KEY IDENTITY(1,1),
    MaGH      INT NOT NULL REFERENCES GioHang(MaGH) ON DELETE CASCADE,
    MaSP      INT NOT NULL REFERENCES SanPham(MaSP),
    SoLuong   INT NOT NULL DEFAULT 1,
    DonGia    DECIMAL(15,2) NOT NULL DEFAULT 0,
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    UNIQUE (MaGH, MaSP)
);

-- ==============================================================
--  7. HÓA ĐƠN & THANH TOÁN
-- ==============================================================

CREATE TABLE BaoCaoDoanhThu (
    MaBaoCao     INT PRIMARY KEY IDENTITY(1,1),
    MaNV         INT NOT NULL REFERENCES NhanVien(MaNV),
    NgayBatDau   DATETIME NOT NULL,
    NgayKetThuc  DATETIME NOT NULL,
    TongDoanhThu DECIMAL(15,2) NOT NULL DEFAULT 0,
    TongDonHang  INT NOT NULL DEFAULT 0,
    NgayTao      DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE HoaDon (
    MaHD      INT PRIMARY KEY IDENTITY(1,1),
    MaKH      INT NOT NULL REFERENCES KhachHang(MaKH),
    MaBaoCao  INT REFERENCES BaoCaoDoanhThu(MaBaoCao),
    -- FIX: ON DELETE SET NULL để tránh lỗi khi DiaChi bị xóa theo cascade KhachHang
    MaDC      INT REFERENCES DiaChi(MaDC) ON DELETE SET NULL,
    NgayLap   DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien  DECIMAL(15,2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chờ xác nhận'
);

CREATE TABLE ChiTietHoaDon (
    MaCTHD    INT PRIMARY KEY IDENTITY(1,1),
    MaHD      INT NOT NULL REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    MaSP      INT NOT NULL REFERENCES SanPham(MaSP),
    SoLuong   INT NOT NULL DEFAULT 1,
    DonGia    DECIMAL(15,2) NOT NULL,
    ThanhTien AS (SoLuong * DonGia) PERSISTED,
    UNIQUE (MaHD, MaSP)
);

CREATE TABLE ThanhToan (
    MaGD          INT PRIMARY KEY IDENTITY(1,1),
    MaHD          INT NOT NULL REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    PhuongThuc    NVARCHAR(50) NOT NULL,
    SoTien        DECIMAL(15,2) NOT NULL,
    TrangThai     NVARCHAR(50) NOT NULL DEFAULT N'Chờ thanh toán',
    NgayThanhToan DATETIME
);

-- ==============================================================
--  8. ĐÁNH GIÁ
-- ==============================================================

CREATE TABLE DanhGia (
    MaDG        INT PRIMARY KEY IDENTITY(1,1),
    MaKH        INT NOT NULL REFERENCES KhachHang(MaKH),
    MaSP        INT NOT NULL REFERENCES SanPham(MaSP),
    MaHD        INT REFERENCES HoaDon(MaHD),
    SoSao       INT NOT NULL CHECK (SoSao BETWEEN 1 AND 5),
    BinhLuan    NVARCHAR(MAX),
    HinhAnh     NVARCHAR(255),
    NgayDanhGia DATETIME NOT NULL DEFAULT GETDATE(),
    UNIQUE (MaKH, MaSP)
);

-- ==============================================================
--  9. THÔNG BÁO
-- ==============================================================

CREATE TABLE ThongBao (
    MaTB      INT PRIMARY KEY IDENTITY(1,1),
    MaKH      INT REFERENCES KhachHang(MaKH),
    MaNV      INT REFERENCES NhanVien(MaNV),
    NoiDung   NVARCHAR(500) NOT NULL,
    NgayTao   DATETIME NOT NULL DEFAULT GETDATE(),
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chưa đọc'
);

CREATE TABLE ChiTietThongBao (
    MaCTTB   INT PRIMARY KEY IDENTITY(1,1),
    MaTB     INT NOT NULL REFERENCES ThongBao(MaTB) ON DELETE CASCADE,
    MaKH     INT NOT NULL REFERENCES KhachHang(MaKH),
    DaXem    BIT NOT NULL DEFAULT 0,
    NgayNhan DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ==============================================================
--  DỮ LIỆU MẪU
-- ==============================================================

-- ------------------------------------------------------------
-- Chức năng
-- ------------------------------------------------------------
INSERT INTO ChucNang (TenChucNang, MoTa) VALUES
(N'Quản lý sản phẩm',   N'Thêm, sửa, xóa sản phẩm'),
(N'Quản lý đơn hàng',   N'Xem và xử lý đơn hàng'),
(N'Quản lý khách hàng', N'Xem thông tin khách hàng'),
(N'Quản lý kho',        N'Nhập xuất kho'),
(N'Báo cáo doanh thu',  N'Xem báo cáo thống kê'),
(N'Quản lý nhân viên',  N'Thêm sửa tài khoản nhân viên');

-- ------------------------------------------------------------
-- Nhân viên
-- ------------------------------------------------------------
INSERT INTO NhanVien (HoTen, Email, TenTK, MatKhau, IsAdministrator, ChucVu) VALUES
(N'Nguyễn Văn Admin',   'admin@pcpartpicker.vn', 'admin', 'Admin123', 1, N'Quản trị viên'),
(N'Trần Thị Nhân Viên', 'nv01@pcpartpicker.vn',  'nv01',  'Nv123456', 0, N'Nhân viên bán hàng');

-- ------------------------------------------------------------
-- FIX: Phân quyền dùng subquery, không hardcode ID
-- ------------------------------------------------------------
-- Admin: toàn bộ chức năng
INSERT INTO ChiTietChucNang (MaNV, MaCN)
SELECT nv.MaNV, cn.MaCN
FROM NhanVien nv
CROSS JOIN ChucNang cn
WHERE nv.TenTK = 'admin';

-- Nhân viên bán hàng: chỉ quản lý đơn hàng + khách hàng
INSERT INTO ChiTietChucNang (MaNV, MaCN)
SELECT nv.MaNV, cn.MaCN
FROM NhanVien nv
JOIN ChucNang cn ON cn.TenChucNang IN (N'Quản lý đơn hàng', N'Quản lý khách hàng')
WHERE nv.TenTK = 'nv01';

-- ------------------------------------------------------------
-- Nhà cung cấp
-- ------------------------------------------------------------
INSERT INTO NhaCungCap (TenNCC, SoDienThoai, Email, DiaChi) VALUES
(N'AMD',      '0123456789', 'contact@amd.com',      N'Santa Clara, California, USA'),
(N'Intel',    '0123456788', 'contact@intel.com',    N'Santa Clara, California, USA'),
(N'NVIDIA',   '0123456787', 'contact@nvidia.com',   N'Santa Clara, California, USA'),
(N'ASUS',     '0123456786', 'contact@asus.com',     N'Đài Bắc, Đài Loan'),
(N'MSI',      '0123456785', 'contact@msi.com',      N'Đài Bắc, Đài Loan'),
(N'Gigabyte', '0123456784', 'contact@gigabyte.com', N'Tân Bắc, Đài Loan'),
(N'Corsair',  '0123456783', 'contact@corsair.com',  N'Fremont, California, USA'),
(N'Samsung',  '0123456782', 'contact@samsung.com',  N'Seoul, Hàn Quốc');

-- ------------------------------------------------------------
-- Danh mục
-- ------------------------------------------------------------
INSERT INTO DanhMuc (TenDanhMuc, MoTa, Slug, ThuTu) VALUES
(N'CPU',           N'Bộ vi xử lý',           'cpu',              1),
(N'CPU Cooler',    N'Tản nhiệt CPU',          'cpu-cooler',       2),
(N'Motherboard',   N'Bo mạch chủ',            'motherboard',      3),
(N'RAM',           N'Bộ nhớ trong DDR4/DDR5', 'memory',           4),
(N'Storage',       N'Ổ cứng SSD / HDD',       'storage',          5),
(N'GPU',           N'Card màn hình',           'gpu',              6),
(N'Case',          N'Vỏ thùng máy tính',      'case',             7),
(N'PSU',           N'Nguồn điện',             'psu',              8),
(N'Monitor',       N'Màn hình',               'monitor',          9),
(N'OS',            N'Hệ điều hành',           'os',               10),
(N'Keyboard',      N'Bàn phím',               'keyboard',         11),
(N'Mouse',         N'Chuột máy tính',         'mouse',            12),
(N'Headphones',    N'Tai nghe',               'headphones',       13),
(N'Case Fan',      N'Quạt case',              'case-fan',         14),
(N'Thermal Paste', N'Keo tản nhiệt',          'thermal-compound', 15);

-- ------------------------------------------------------------
-- Kho
-- ------------------------------------------------------------
INSERT INTO Kho (TenKho, DiaChi, SucChua) VALUES
(N'Kho Hà Nội',  N'Hoàn Kiếm, Hà Nội',       5000),
(N'Kho TP.HCM',  N'Quận 1, TP. Hồ Chí Minh', 8000),
(N'Kho Đà Nẵng', N'Hải Châu, Đà Nẵng',        3000);

-- ------------------------------------------------------------
-- Sản phẩm
-- ------------------------------------------------------------
INSERT INTO SanPham (MaDanhMuc, MaNCC, TenSP, ThuongHieu, GiaBan, GiaNhap, SoLuongTon, ThongSoKyThuat, MoTa)
SELECT dm.MaDanhMuc, ncc.MaNCC, sp.TenSP, sp.ThuongHieu, sp.GiaBan, sp.GiaNhap, sp.SoLuongTon, sp.ThongSoKyThuat, sp.MoTa
FROM (VALUES
    -- CPU
    ('CPU', 'AMD',
     N'AMD Ryzen 5 7600X', 'AMD', 5200000, 4500000, 50,
     '{"Socket":"AM5","Cores":6,"Threads":12,"TDP":105,"BoostClock":5.3}',
     N'CPU 6 nhân 12 luồng, hiệu năng cao cho gaming'),
    ('CPU', 'AMD',
     N'AMD Ryzen 7 7700X', 'AMD', 7800000, 6800000, 30,
     '{"Socket":"AM5","Cores":8,"Threads":16,"TDP":105,"BoostClock":5.4}',
     N'CPU 8 nhân 16 luồng, lý tưởng cho đa nhiệm'),
    ('CPU', 'Intel',
     N'Intel Core i5-13600K', 'Intel', 6500000, 5500000, 40,
     '{"Socket":"LGA1700","Cores":14,"Threads":20,"TDP":125,"BoostClock":5.1}',
     N'CPU Intel 14 nhân 20 luồng hiệu năng xuất sắc'),
    -- CPU Cooler
    ('CPU Cooler', 'ASUS',
     N'Thermalright Peerless Assassin 120 SE', 'Thermalright', 850000, 650000, 100,
     '{"Type":"Air","TDP":260,"FanSize":120,"Socket":["AM4","AM5","LGA1700"]}',
     N'Tản nhiệt khí 2 tháp giá rẻ hiệu năng cao'),
    ('CPU Cooler', 'ASUS',
     N'Noctua NH-D15', 'Noctua', 2800000, 2300000, 20,
     '{"Type":"Air","TDP":250,"FanSize":140,"Socket":["AM4","AM5","LGA1700"]}',
     N'Tản nhiệt khí cao cấp hàng đầu thế giới'),
    -- Motherboard
    ('Motherboard', 'Gigabyte',
     N'Gigabyte B650M DS3H', 'Gigabyte', 3200000, 2700000, 35,
     '{"Socket":"AM5","Chipset":"B650","RamType":"DDR5","FormFactor":"Micro-ATX"}',
     N'Bo mạch chủ Micro-ATX tầm trung AMD AM5'),
    ('Motherboard', 'ASUS',
     N'ASUS ROG STRIX B650E-F', 'ASUS', 7500000, 6500000, 15,
     '{"Socket":"AM5","Chipset":"B650E","RamType":"DDR5","FormFactor":"ATX"}',
     N'Bo mạch chủ ATX cao cấp ROG STRIX'),
    -- RAM
    ('RAM', 'Corsair',
     N'Corsair Vengeance DDR5-6000 32GB', 'Corsair', 2800000, 2300000, 60,
     '{"Capacity":32,"Type":"DDR5","Speed":6000,"CL":30,"Modules":2}',
     N'RAM DDR5 6000MHz 32GB kit 2x16GB'),
    ('RAM', 'Samsung',
     N'Samsung DDR5-4800 16GB', 'Samsung', 1500000, 1200000, 80,
     '{"Capacity":16,"Type":"DDR5","Speed":4800,"CL":40,"Modules":1}',
     N'RAM DDR5 Samsung 4800MHz 16GB'),
    -- Storage
    ('Storage', 'Samsung',
     N'Samsung 990 Pro 1TB NVMe', 'Samsung', 2500000, 2100000, 45,
     '{"Capacity":1000,"Interface":"PCIe 4.0 x4","FormFactor":"M.2 2280","ReadSpeed":7450}',
     N'SSD NVMe PCIe 4.0 tốc độ cao cho gaming'),
    ('Storage', 'Samsung',
     N'Samsung 870 EVO 1TB SATA', 'Samsung', 1800000, 1500000, 30,
     '{"Capacity":1000,"Interface":"SATA III","FormFactor":"2.5 inch","ReadSpeed":560}',
     N'SSD SATA đáng tin cậy cho lưu trữ phổ thông'),
    -- GPU
    ('GPU', 'NVIDIA',
     N'NVIDIA GeForce RTX 4070', 'NVIDIA', 16000000, 14000000, 20,
     '{"VRAM":12,"MemType":"GDDR6X","TDP":200,"CUDA":5888}',
     N'GPU tầm cao cho 1440p gaming với ray tracing'),
    ('GPU', 'AMD',
     N'AMD Radeon RX 7800 XT', 'AMD', 12000000, 10500000, 25,
     '{"VRAM":16,"MemType":"GDDR6","TDP":263,"StreamProc":3840}',
     N'GPU AMD 16GB VRAM xuất sắc cho 1440p'),
    -- PSU
    ('PSU', 'Corsair',
     N'Corsair RM750x 750W', 'Corsair', 2500000, 2100000, 40,
     '{"Wattage":750,"Efficiency":"80+ Gold","Modular":"Full"}',
     N'Nguồn 750W 80+ Gold fully modular'),
    ('PSU', 'MSI',
     N'MSI MAG A650BN 650W', 'MSI', 1600000, 1300000, 55,
     '{"Wattage":650,"Efficiency":"80+ Bronze","Modular":"None"}',
     N'Nguồn 650W 80+ Bronze tầm trung giá tốt'),
    -- Case
    ('Case', 'ASUS',
     N'ASUS Prime AP201', 'ASUS', 1800000, 1500000, 30,
     '{"FormFactor":"Micro-ATX","SidePanelType":"Mesh","MaxGPULength":338}',
     N'Vỏ case Micro-ATX nhỏ gọn airflow tốt'),
    ('Case', 'MSI',
     N'MSI MAG Forge 320R', 'MSI', 2200000, 1900000, 25,
     '{"FormFactor":"ATX","SidePanelType":"Tempered Glass","MaxGPULength":380}',
     N'Vỏ case ATX kính cường lực airflow tối ưu'),
    -- Monitor
    ('Monitor', 'ASUS',
     N'ASUS TUF Gaming VG27AQ', 'ASUS', 7200000, 6200000, 18,
     '{"Size":27,"Resolution":"2560x1440","RefreshRate":165,"Panel":"IPS"}',
     N'Màn hình 27 inch 1440p 165Hz IPS cho gaming'),
    -- Keyboard
    ('Keyboard', 'MSI',
     N'MSI Vigor GK50 Elite', 'MSI', 1200000, 950000, 40,
     '{"Type":"Mechanical","Switch":"Kailh Box White","Backlight":"RGB"}',
     N'Bàn phím cơ gaming RGB switch Kailh Box White'),
    -- Mouse
    ('Mouse', 'ASUS',
     N'ASUS ROG Keris', 'ASUS', 1500000, 1200000, 35,
     '{"DPI":16000,"Buttons":7,"Weight":79,"Wireless":false}',
     N'Chuột gaming ROG nhẹ 79g DPI 16000')
) AS sp (SlugDM, TenNCC, TenSP, ThuongHieu, GiaBan, GiaNhap, SoLuongTon, ThongSoKyThuat, MoTa)
JOIN DanhMuc dm   ON dm.TenDanhMuc = sp.SlugDM
JOIN NhaCungCap ncc ON ncc.TenNCC = sp.TenNCC;

-- ------------------------------------------------------------
-- Ảnh sản phẩm (dùng subquery theo TenSP)
-- ------------------------------------------------------------
UPDATE AnhSanPham
SET DuongDan = N'"C:\Users\hung\OneDrive\Documents\kĩ thuật phần mềm\code chung\ktpm\ảnh\ryzen5-7600x.jpg"'
WHERE MaSP = 1;
INSERT INTO AnhSanPham (MaSP, DuongDan, LaAnhChinh, ThuTu)
SELECT MaSP, DuongDan, 1, 1
FROM (VALUES
    (N'AMD Ryzen 5 7600X',                   '/images/products/ryzen5-7600x.jpg'),
    (N'AMD Ryzen 7 7700X',                   '/images/products/ryzen7-7700x.jpg'),
    (N'Intel Core i5-13600K',                '/images/products/i5-13600k.jpg'),
    (N'Thermalright Peerless Assassin 120 SE','/images/products/pa120-se.jpg'),
    (N'Noctua NH-D15',                       '/images/products/noctua-nhd15.jpg'),
    (N'Gigabyte B650M DS3H',                 '/images/products/b650m-ds3h.jpg'),
    (N'ASUS ROG STRIX B650E-F',              '/images/products/rog-b650e.jpg'),
    (N'Corsair Vengeance DDR5-6000 32GB',    '/images/products/corsair-ddr5.jpg'),
    (N'Samsung DDR5-4800 16GB',              '/images/products/samsung-ddr5.jpg'),
    (N'Samsung 990 Pro 1TB NVMe',            '/images/products/990pro.jpg'),
    (N'Samsung 870 EVO 1TB SATA',            '/images/products/870evo.jpg'),
    (N'NVIDIA GeForce RTX 4070',             '/images/products/rtx4070.jpg'),
    (N'AMD Radeon RX 7800 XT',               '/images/products/rx7800xt.jpg'),
    (N'Corsair RM750x 750W',                 '/images/products/rm750x.jpg'),
    (N'MSI MAG A650BN 650W',                 '/images/products/msi-a650bn.jpg'),
    (N'ASUS Prime AP201',                    '/images/products/asus-ap201.jpg'),
    (N'MSI MAG Forge 320R',                  '/images/products/forge320r.jpg'),
    (N'ASUS TUF Gaming VG27AQ',              '/images/products/vg27aq.jpg'),
    (N'MSI Vigor GK50 Elite',                '/images/products/gk50.jpg'),
    (N'ASUS ROG Keris',                      '/images/products/rog-keris.jpg')
) AS src (TenSP, DuongDan)
JOIN SanPham sp ON sp.TenSP = src.TenSP;

-- ------------------------------------------------------------
-- Giá từng shop
-- ------------------------------------------------------------
INSERT INTO GiaTungShop (MaSP, TenShop, Gia, Link, ConHang)
SELECT sp.MaSP, g.TenShop, g.Gia, g.Link, 1
FROM (VALUES
    (N'AMD Ryzen 5 7600X',       N'Phong Vũ',       5200000, 'https://phongvu.vn'),
    (N'AMD Ryzen 5 7600X',       N'Gearvn',          5150000, 'https://gearvn.com'),
    (N'AMD Ryzen 5 7600X',       N'Hanoicomputer',   5180000, 'https://hanoicomputer.vn'),
    (N'Intel Core i5-13600K',    N'Phong Vũ',        6500000, 'https://phongvu.vn'),
    (N'Intel Core i5-13600K',    N'Gearvn',          6450000, 'https://gearvn.com'),
    (N'NVIDIA GeForce RTX 4070', N'Phong Vũ',       16000000, 'https://phongvu.vn'),
    (N'NVIDIA GeForce RTX 4070', N'Gearvn',         15800000, 'https://gearvn.com'),
    (N'AMD Radeon RX 7800 XT',   N'Phong Vũ',       12000000, 'https://phongvu.vn'),
    (N'AMD Radeon RX 7800 XT',   N'Gearvn',         11800000, 'https://gearvn.com')
) AS g (TenSP, TenShop, Gia, Link)
JOIN SanPham sp ON sp.TenSP = g.TenSP;

-- ------------------------------------------------------------
-- Benchmark
-- ------------------------------------------------------------
INSERT INTO Benchmark (MaSP, LoaiBM, CongCuTest, DiemSo, GhiChu)
SELECT sp.MaSP, b.LoaiBM, b.CongCuTest, b.DiemSo, b.GhiChu
FROM (VALUES
    (N'AMD Ryzen 5 7600X',       'rendering', 'Cinebench R23 Multi', 15200, N'Ryzen 5 7600X đa nhân'),
    (N'AMD Ryzen 5 7600X',       'gaming',    'CS2 1080p High',        280, N'FPS trung bình CS2'),
    (N'AMD Ryzen 7 7700X',       'rendering', 'Cinebench R23 Multi', 20100, N'Ryzen 7 7700X đa nhân'),
    (N'Intel Core i5-13600K',    'rendering', 'Cinebench R23 Multi', 24500, N'i5-13600K P+E core'),
    (N'NVIDIA GeForce RTX 4070', 'gaming',    '3DMark Time Spy',     19800, N'RTX 4070 1440p Ultra'),
    (N'AMD Radeon RX 7800 XT',   'gaming',    '3DMark Time Spy',     17200, N'RX 7800 XT 1440p Ultra')
) AS b (TenSP, LoaiBM, CongCuTest, DiemSo, GhiChu)
JOIN SanPham sp ON sp.TenSP = b.TenSP;

-- ------------------------------------------------------------
-- Luật tương thích
-- ------------------------------------------------------------
INSERT INTO TuongThich (MaDanhMucA, MaDanhMucB, ThuocTinhA, ThuocTinhB, MoTa)
SELECT a.MaDanhMuc, b.MaDanhMuc, t.ThuocTinhA, t.ThuocTinhB, t.MoTa
FROM (VALUES
    ('CPU', 'Motherboard', 'Socket',  'Socket',  N'CPU và Motherboard phải cùng Socket'),
    ('CPU', 'CPU Cooler',  'TDP',     'TDP',     N'TDP CPU không vượt quá khả năng tản nhiệt'),
    ('RAM', 'Motherboard', 'Type',    'RamType', N'Loại RAM phải Motherboard hỗ trợ'),
    ('PSU', 'GPU',         'Wattage', 'TDP',     N'Công suất nguồn phải đủ cho toàn hệ thống')
) AS t (SlugA, SlugB, ThuocTinhA, ThuocTinhB, MoTa)
JOIN DanhMuc a ON a.TenDanhMuc = t.SlugA
JOIN DanhMuc b ON b.TenDanhMuc = t.SlugB;

-- ------------------------------------------------------------
-- Khách hàng
-- ------------------------------------------------------------
INSERT INTO KhachHang (Username, Email, MatKhau, HoTen, SDT) VALUES
('nguyenvana', 'nguyenvana@gmail.com', '123456', N'Nguyễn Văn A', '0901234567'),
('tranthib',   'tranthib@gmail.com',   '123456', N'Trần Thị B',   '0912345678'),
('levanc',     'levanc@gmail.com',     '123456', N'Lê Văn C',     '0923456789');

-- ------------------------------------------------------------
-- Địa chỉ
-- ------------------------------------------------------------
INSERT INTO DiaChi (MaKH, HoTenNguoiNhan, SDT, DiaChiDay, TinhThanh, QuanHuyen, PhuongXa, MacDinh)
SELECT kh.MaKH, d.HoTenNguoiNhan, d.SDT, d.DiaChiDay, d.TinhThanh, d.QuanHuyen, d.PhuongXa, 1
FROM (VALUES
    ('nguyenvana', N'Nguyễn Văn A', '0901234567', N'123 Nguyễn Huệ', N'TP. Hồ Chí Minh', N'Quận 1',    N'Bến Nghé'),
    ('tranthib',   N'Trần Thị B',   '0912345678', N'456 Hàng Bài',   N'Hà Nội',           N'Hoàn Kiếm', N'Tràng Tiền'),
    ('levanc',     N'Lê Văn C',     '0923456789', N'789 Trần Phú',   N'Đà Nẵng',          N'Hải Châu',  N'Thạch Thang')
) AS d (Username, HoTenNguoiNhan, SDT, DiaChiDay, TinhThanh, QuanHuyen, PhuongXa)
JOIN KhachHang kh ON kh.Username = d.Username;

-- ------------------------------------------------------------
-- Giỏ hàng
-- ------------------------------------------------------------
INSERT INTO GioHang (MaKH)
SELECT MaKH FROM KhachHang WHERE Username IN ('nguyenvana', 'tranthib');

-- FIX: dùng subquery thay vì hardcode MaGH, MaSP
INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong, DonGia)
SELECT gh.MaGH, sp.MaSP, 1, sp.GiaBan
FROM GioHang gh
JOIN KhachHang kh ON kh.MaKH = gh.MaKH
JOIN SanPham sp ON sp.TenSP = N'AMD Ryzen 5 7600X'
WHERE kh.Username = 'nguyenvana';

INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong, DonGia)
SELECT gh.MaGH, sp.MaSP, 1, sp.GiaBan
FROM GioHang gh
JOIN KhachHang kh ON kh.MaKH = gh.MaKH
JOIN SanPham sp ON sp.TenSP = N'NVIDIA GeForce RTX 4070'
WHERE kh.Username = 'nguyenvana';

INSERT INTO ChiTietGioHang (MaGH, MaSP, SoLuong, DonGia)
SELECT gh.MaGH, sp.MaSP, 2, sp.GiaBan
FROM GioHang gh
JOIN KhachHang kh ON kh.MaKH = gh.MaKH
JOIN SanPham sp ON sp.TenSP = N'Corsair Vengeance DDR5-6000 32GB'
WHERE kh.Username = 'tranthib';

-- ------------------------------------------------------------
-- Build PC
-- ------------------------------------------------------------
INSERT INTO Build (MaKH, TenBuild, MoTa, TongGia, CongKhai, TongWatt)
SELECT kh.MaKH, b.TenBuild, b.MoTa, b.TongGia, b.CongKhai, b.TongWatt
FROM (VALUES
    ('nguyenvana', N'Gaming PC tầm trung AM5',
     N'Build 1440p gaming Ryzen 5 7600X + RTX 4070', 31650000, 1, 450),
    ('nguyenvana', N'Budget Build Intel',
     N'Build giá rẻ văn phòng và gaming nhẹ',         18000000, 1, 280),
    ('tranthib',   N'Workstation AMD Ryzen 7',
     N'Máy trạm đa nhiệm Ryzen 7 7700X',              38000000, 0, 500)
) AS b (Username, TenBuild, MoTa, TongGia, CongKhai, TongWatt)
JOIN KhachHang kh ON kh.Username = b.Username;

-- FIX: dùng subquery thay vì hardcode MaBuild, MaSP
INSERT INTO ChiTietBuild (MaBuild, MaSP, SoLuong)
SELECT b.MaBuild, sp.MaSP, 1
FROM Build b
JOIN KhachHang kh ON kh.MaKH = b.MaKH
JOIN SanPham sp ON sp.TenSP IN (
    N'AMD Ryzen 5 7600X',
    N'Thermalright Peerless Assassin 120 SE',
    N'Gigabyte B650M DS3H',
    N'Corsair Vengeance DDR5-6000 32GB',
    N'Samsung 990 Pro 1TB NVMe',
    N'NVIDIA GeForce RTX 4070',
    N'Corsair RM750x 750W',
    N'ASUS Prime AP201'
)
WHERE b.TenBuild = N'Gaming PC tầm trung AM5'
  AND kh.Username = 'nguyenvana';

-- ------------------------------------------------------------
-- Hóa đơn
-- ------------------------------------------------------------
INSERT INTO HoaDon (MaKH, MaDC, TongTien, TrangThai)
SELECT kh.MaKH, dc.MaDC, h.TongTien, h.TrangThai
FROM (VALUES
    ('nguyenvana', 21200000, N'Hoàn thành'),
    ('tranthib',    5200000, N'Đang giao'),
    ('levanc',      7800000, N'Chờ xác nhận')
) AS h (Username, TongTien, TrangThai)
JOIN KhachHang kh ON kh.Username = h.Username
JOIN DiaChi dc    ON dc.MaKH = kh.MaKH AND dc.MacDinh = 1;

-- FIX: dùng subquery thay vì hardcode MaHD, MaSP
INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia)
SELECT hd.MaHD, sp.MaSP, 1, sp.GiaBan
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
JOIN SanPham sp ON sp.TenSP = N'NVIDIA GeForce RTX 4070'
WHERE kh.Username = 'nguyenvana' AND hd.TrangThai = N'Hoàn thành';

INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia)
SELECT hd.MaHD, sp.MaSP, 1, sp.GiaBan
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
JOIN SanPham sp ON sp.TenSP = N'AMD Ryzen 5 7600X'
WHERE kh.Username = 'nguyenvana' AND hd.TrangThai = N'Hoàn thành';

INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia)
SELECT hd.MaHD, sp.MaSP, 1, sp.GiaBan
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
JOIN SanPham sp ON sp.TenSP = N'AMD Ryzen 5 7600X'
WHERE kh.Username = 'tranthib' AND hd.TrangThai = N'Đang giao';

INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia)
SELECT hd.MaHD, sp.MaSP, 1, sp.GiaBan
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
JOIN SanPham sp ON sp.TenSP = N'AMD Ryzen 7 7700X'
WHERE kh.Username = 'levanc' AND hd.TrangThai = N'Chờ xác nhận';

-- ------------------------------------------------------------
-- Thanh toán
-- ------------------------------------------------------------
INSERT INTO ThanhToan (MaHD, PhuongThuc, SoTien, TrangThai, NgayThanhToan)
SELECT hd.MaHD, N'Chuyển khoản', hd.TongTien, N'Đã thanh toán', GETDATE()
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
WHERE kh.Username = 'nguyenvana' AND hd.TrangThai = N'Hoàn thành';

INSERT INTO ThanhToan (MaHD, PhuongThuc, SoTien, TrangThai, NgayThanhToan)
SELECT hd.MaHD, N'COD', hd.TongTien, N'Chờ thanh toán', NULL
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
WHERE kh.Username = 'tranthib' AND hd.TrangThai = N'Đang giao';

INSERT INTO ThanhToan (MaHD, PhuongThuc, SoTien, TrangThai, NgayThanhToan)
SELECT hd.MaHD, N'Momo', hd.TongTien, N'Chờ thanh toán', NULL
FROM HoaDon hd
JOIN KhachHang kh ON kh.MaKH = hd.MaKH
WHERE kh.Username = 'levanc' AND hd.TrangThai = N'Chờ xác nhận';

-- ------------------------------------------------------------
-- Đánh giá
-- ------------------------------------------------------------
INSERT INTO DanhGia (MaKH, MaSP, MaHD, SoSao, BinhLuan)
SELECT kh.MaKH, sp.MaSP, hd.MaHD, 5, N'RTX 4070 cực mạnh, chơi game 1440p siêu mượt!'
FROM KhachHang kh
JOIN HoaDon hd    ON hd.MaKH = kh.MaKH AND hd.TrangThai = N'Hoàn thành'
JOIN SanPham sp   ON sp.TenSP = N'NVIDIA GeForce RTX 4070'
WHERE kh.Username = 'nguyenvana';

INSERT INTO DanhGia (MaKH, MaSP, MaHD, SoSao, BinhLuan)
SELECT kh.MaKH, sp.MaSP, hd.MaHD, 5, N'Ryzen 5 7600X hiệu năng tốt, nhiệt độ ổn định'
FROM KhachHang kh
JOIN HoaDon hd    ON hd.MaKH = kh.MaKH AND hd.TrangThai = N'Hoàn thành'
JOIN SanPham sp   ON sp.TenSP = N'AMD Ryzen 5 7600X'
WHERE kh.Username = 'nguyenvana';

INSERT INTO DanhGia (MaKH, MaSP, MaHD, SoSao, BinhLuan)
SELECT kh.MaKH, sp.MaSP, hd.MaHD, 4, N'CPU tốt nhưng cần tản nhiệt tốt hơn'
FROM KhachHang kh
JOIN HoaDon hd    ON hd.MaKH = kh.MaKH AND hd.TrangThai = N'Đang giao'
JOIN SanPham sp   ON sp.TenSP = N'AMD Ryzen 5 7600X'
WHERE kh.Username = 'tranthib';

-- ------------------------------------------------------------
-- Thông báo
-- ------------------------------------------------------------
INSERT INTO ThongBao (MaKH, NoiDung, TrangThai)
SELECT MaKH, N'Đơn hàng đã hoàn thành. Cảm ơn bạn đã mua hàng!', N'Chưa đọc'
FROM KhachHang WHERE Username = 'nguyenvana';

INSERT INTO ThongBao (MaKH, NoiDung, TrangThai)
SELECT MaKH, N'Đơn hàng đang được giao đến bạn.', N'Chưa đọc'
FROM KhachHang WHERE Username = 'tranthib';

INSERT INTO ThongBao (MaKH, NoiDung, TrangThai)
SELECT MaKH, N'Chào mừng bạn đến với PCPartPicker VN!', N'Chưa đọc'
FROM KhachHang WHERE Username = 'levanc';

-- FIX: dùng subquery thay vì hardcode MaTB, MaKH
INSERT INTO ChiTietThongBao (MaTB, MaKH, DaXem)
SELECT tb.MaTB, tb.MaKH, 0
FROM ThongBao tb
WHERE tb.MaKH IS NOT NULL;
GO

-- ==============================================================
--  KIỂM TRA KẾT QUẢ
-- ==============================================================
SELECT 'KhachHang'       AS Bang, COUNT(*) AS SoBan FROM KhachHang       UNION ALL
SELECT 'NhanVien',                 COUNT(*)           FROM NhanVien        UNION ALL
SELECT 'ChiTietChucNang',          COUNT(*)           FROM ChiTietChucNang UNION ALL
SELECT 'DanhMuc',                  COUNT(*)           FROM DanhMuc         UNION ALL
SELECT 'SanPham',                  COUNT(*)           FROM SanPham         UNION ALL
SELECT 'AnhSanPham',               COUNT(*)           FROM AnhSanPham      UNION ALL
SELECT 'GiaTungShop',              COUNT(*)           FROM GiaTungShop     UNION ALL
SELECT 'Benchmark',                COUNT(*)           FROM Benchmark       UNION ALL
SELECT 'TuongThich',               COUNT(*)           FROM TuongThich      UNION ALL
SELECT 'GioHang',                  COUNT(*)           FROM GioHang         UNION ALL
SELECT 'ChiTietGioHang',           COUNT(*)           FROM ChiTietGioHang  UNION ALL
SELECT 'Build',                    COUNT(*)           FROM Build           UNION ALL
SELECT 'ChiTietBuild',             COUNT(*)           FROM ChiTietBuild    UNION ALL
SELECT 'HoaDon',                   COUNT(*)           FROM HoaDon          UNION ALL
SELECT 'ChiTietHoaDon',            COUNT(*)           FROM ChiTietHoaDon   UNION ALL
SELECT 'ThanhToan',                COUNT(*)           FROM ThanhToan       UNION ALL
SELECT 'DanhGia',                  COUNT(*)           FROM DanhGia         UNION ALL
SELECT 'ThongBao',                 COUNT(*)           FROM ThongBao        UNION ALL
SELECT 'ChiTietThongBao',          COUNT(*)           FROM ChiTietThongBao;
GO