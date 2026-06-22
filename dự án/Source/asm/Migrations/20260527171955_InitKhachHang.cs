using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asm.Migrations
{
    /// <inheritdoc />
    public partial class InitKhachHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucNang",
                columns: table => new
                {
                    MaCN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucNang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChucNang__27258E0E9456497D", x => x.MaCN);
                });

            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThuTu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhMuc__B3750887B2A58642", x => x.MaDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KhachHan__2725CF1E38BE2BFE", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    MaKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SucChua = table.Column<int>(type: "int", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Kho__3BDA935069FFA161", x => x.MaKho);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    MaNCC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNCC = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhaCungC__3A185DEB82E6A929", x => x.MaNCC);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Luong = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CCCD = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TenTK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAdministrator = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NhanVien__2725D70A5D888752", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "TuongThich",
                columns: table => new
                {
                    MaTT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDanhMucA = table.Column<int>(type: "int", nullable: false),
                    MaDanhMucB = table.Column<int>(type: "int", nullable: false),
                    ThuocTinhA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ThuocTinhB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TuongThi__27250079E4C91C81", x => x.MaTT);
                    table.ForeignKey(
                        name: "FK__TuongThic__MaDan__17F790F9",
                        column: x => x.MaDanhMucA,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc");
                    table.ForeignKey(
                        name: "FK__TuongThic__MaDan__18EBB532",
                        column: x => x.MaDanhMucB,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc");
                });

            migrationBuilder.CreateTable(
                name: "Build",
                columns: table => new
                {
                    MaBuild = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    TenBuild = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongGia = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CongKhai = table.Column<bool>(type: "bit", nullable: false),
                    TongWatt = table.Column<int>(type: "int", nullable: true),
                    LuotXem = table.Column<int>(type: "int", nullable: false),
                    LuotThich = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Build__B4A44200BA6BCFDE", x => x.MaBuild);
                    table.ForeignKey(
                        name: "FK__Build__MaKH__1BC821DD",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiaChi",
                columns: table => new
                {
                    MaDC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    HoTenNguoiNhan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    DiaChiDay = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TinhThanh = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QuanHuyen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhuongXa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MacDinh = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DiaChi__27258664E4123622", x => x.MaDC);
                    table.ForeignKey(
                        name: "FK__DiaChi__MaKH__73BA3083",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    MaGH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GioHang__2725AE85C91B5164", x => x.MaGH);
                    table.ForeignKey(
                        name: "FK__GioHang__MaKH__32AB8735",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoDoanhThu",
                columns: table => new
                {
                    MaBaoCao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime", nullable: false),
                    TongDoanhThu = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TongDonHang = table.Column<int>(type: "int", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BaoCaoDo__25A9188CD8CD6738", x => x.MaBaoCao);
                    table.ForeignKey(
                        name: "FK__BaoCaoDoan__MaNV__3D2915A8",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietChucNang",
                columns: table => new
                {
                    MaCTCN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    MaCN = table.Column<int>(type: "int", nullable: false),
                    NgayCapQuyen = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietC__1E4E48DFCE9F3468", x => x.MaCTCN);
                    table.ForeignKey(
                        name: "FK__ChiTietChu__MaCN__693CA210",
                        column: x => x.MaCN,
                        principalTable: "ChucNang",
                        principalColumn: "MaCN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietChu__MaNV__68487DD7",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    MaNCC = table.Column<int>(type: "int", nullable: true),
                    TenSP = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ThuongHieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GiaBan = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    GiaNhap = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "datetime", nullable: true),
                    SoLuongTon = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ThongSoKyThuat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaNVQuanLy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SanPham__2725081CB2331181", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK__SanPham__MaDanhM__00200768",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc");
                    table.ForeignKey(
                        name: "FK__SanPham__MaNCC__01142BA1",
                        column: x => x.MaNCC,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNCC");
                    table.ForeignKey(
                        name: "FK__SanPham__MaNVQua__04E4BC85",
                        column: x => x.MaNVQuanLy,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "ThongBao",
                columns: table => new
                {
                    MaTB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: true),
                    MaNV = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Chưa đọc")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThongBao__2725006F950812E7", x => x.MaTB);
                    table.ForeignKey(
                        name: "FK__ThongBao__MaKH__5BAD9CC8",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                    table.ForeignKey(
                        name: "FK__ThongBao__MaNV__5CA1C101",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    MaHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaBaoCao = table.Column<int>(type: "int", nullable: true),
                    MaDC = table.Column<int>(type: "int", nullable: true),
                    NgayLap = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TongTien = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Chờ xác nhận")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HoaDon__2725A6E05B02752E", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK__HoaDon__MaBaoCao__43D61337",
                        column: x => x.MaBaoCao,
                        principalTable: "BaoCaoDoanhThu",
                        principalColumn: "MaBaoCao");
                    table.ForeignKey(
                        name: "FK__HoaDon__MaDC__44CA3770",
                        column: x => x.MaDC,
                        principalTable: "DiaChi",
                        principalColumn: "MaDC");
                    table.ForeignKey(
                        name: "FK__HoaDon__MaKH__42E1EEFE",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "AnhSanPham",
                columns: table => new
                {
                    MaAnh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    DuongDan = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LaAnhChinh = table.Column<bool>(type: "bit", nullable: false),
                    ThuTu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AnhSanPh__356240DF82D123E2", x => x.MaAnh);
                    table.ForeignKey(
                        name: "FK__AnhSanPham__MaSP__07C12930",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benchmark",
                columns: table => new
                {
                    MaBM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    LoaiBM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CongCuTest = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiemSo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    NgayTest = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Benchmar__272475AC9DBBE8BB", x => x.MaBM);
                    table.ForeignKey(
                        name: "FK__Benchmark__MaSP__29221CFB",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietBuild",
                columns: table => new
                {
                    MaCTB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaBuild = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietB__3DCB540B2BDEA9D3", x => x.MaCTB);
                    table.ForeignKey(
                        name: "FK__ChiTietBu__MaBui__245D67DE",
                        column: x => x.MaBuild,
                        principalTable: "Build",
                        principalColumn: "MaBuild",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietBui__MaSP__25518C17",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDanhMucSP",
                columns: table => new
                {
                    MaCTDM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDanhMuc = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietD__1E4E40FF7CE0C26A", x => x.MaCTDM);
                    table.ForeignKey(
                        name: "FK__ChiTietDa__MaDan__0D7A0286",
                        column: x => x.MaDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "MaDanhMuc");
                    table.ForeignKey(
                        name: "FK__ChiTietDan__MaSP__0E6E26BF",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHang",
                columns: table => new
                {
                    MaCTGH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaGH = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DonGia = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(26,2)", nullable: true, computedColumnSql: "([SoLuong]*[DonGia])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietG__1E4FAF542E60E9FE", x => x.MaCTGH);
                    table.ForeignKey(
                        name: "FK__ChiTietGio__MaGH__37703C52",
                        column: x => x.MaGH,
                        principalTable: "GioHang",
                        principalColumn: "MaGH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietGio__MaSP__3864608B",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "GiaTungShop",
                columns: table => new
                {
                    MaGTS = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    TenShop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ConHang = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GiaTungS__3CD364FEFF449073", x => x.MaGTS);
                    table.ForeignKey(
                        name: "FK__GiaTungSho__MaSP__2CF2ADDF",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuanLySanPham",
                columns: table => new
                {
                    MaQL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    MaKho = table.Column<int>(type: "int", nullable: true),
                    NgayQuanLy = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    VaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuanLySa__2725F852CB48FE1F", x => x.MaQL);
                    table.ForeignKey(
                        name: "FK__QuanLySanP__MaNV__123EB7A3",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV");
                    table.ForeignKey(
                        name: "FK__QuanLySanP__MaSP__1332DBDC",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                    table.ForeignKey(
                        name: "FK__QuanLySan__MaKho__14270015",
                        column: x => x.MaKho,
                        principalTable: "Kho",
                        principalColumn: "MaKho");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietThongBao",
                columns: table => new
                {
                    MaCTTB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaTB = table.Column<int>(type: "int", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    DaXem = table.Column<bool>(type: "bit", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietT__1E4FC51EF774303B", x => x.MaCTTB);
                    table.ForeignKey(
                        name: "FK__ChiTietTho__MaKH__625A9A57",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                    table.ForeignKey(
                        name: "FK__ChiTietTho__MaTB__6166761E",
                        column: x => x.MaTB,
                        principalTable: "ThongBao",
                        principalColumn: "MaTB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDon",
                columns: table => new
                {
                    MaCTHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DonGia = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal(26,2)", nullable: true, computedColumnSql: "([SoLuong]*[DonGia])", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChiTietH__1E4FA7710E7E44A4", x => x.MaCTHD);
                    table.ForeignKey(
                        name: "FK__ChiTietHoa__MaHD__4B7734FF",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ChiTietHoa__MaSP__4C6B5938",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    MaDG = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    MaSP = table.Column<int>(type: "int", nullable: false),
                    MaHD = table.Column<int>(type: "int", nullable: true),
                    SoSao = table.Column<int>(type: "int", nullable: false),
                    BinhLuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DanhGia__2725866018BBB931", x => x.MaDG);
                    table.ForeignKey(
                        name: "FK__DanhGia__MaHD__56E8E7AB",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "FK__DanhGia__MaKH__55009F39",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "MaKH");
                    table.ForeignKey(
                        name: "FK__DanhGia__MaSP__55F4C372",
                        column: x => x.MaSP,
                        principalTable: "SanPham",
                        principalColumn: "MaSP");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    MaGD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHD = table.Column<int>(type: "int", nullable: false),
                    PhuongThuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Chờ thanh toán"),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ThanhToa__2725AE816E54C71C", x => x.MaGD);
                    table.ForeignKey(
                        name: "FK__ThanhToan__MaHD__503BEA1C",
                        column: x => x.MaHD,
                        principalTable: "HoaDon",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnhSanPham_MaSP",
                table: "AnhSanPham",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoDoanhThu_MaNV",
                table: "BaoCaoDoanhThu",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_Benchmark_MaSP",
                table: "Benchmark",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "IX_Build_MaKH",
                table: "Build",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietBuild_MaSP",
                table: "ChiTietBuild",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__ChiTietB__66D61280DA33EC1D",
                table: "ChiTietBuild",
                columns: new[] { "MaBuild", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChucNang_MaCN",
                table: "ChiTietChucNang",
                column: "MaCN");

            migrationBuilder.CreateIndex(
                name: "UQ__ChiTietC__D5578FEB6124A8D3",
                table: "ChiTietChucNang",
                columns: new[] { "MaNV", "MaCN" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDanhMucSP_MaSP",
                table: "ChiTietDanhMucSP",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__ChiTietD__61075807B0AB6E5B",
                table: "ChiTietDanhMucSP",
                columns: new[] { "MaDanhMuc", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHang_MaSP",
                table: "ChiTietGioHang",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__ChiTietG__F557FE05DF855DED",
                table: "ChiTietGioHang",
                columns: new[] { "MaGH", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDon_MaSP",
                table: "ChiTietHoaDon",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__ChiTietH__F557F660137C9172",
                table: "ChiTietHoaDon",
                columns: new[] { "MaHD", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietThongBao_MaKH",
                table: "ChiTietThongBao",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietThongBao_MaTB",
                table: "ChiTietThongBao",
                column: "MaTB");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaHD",
                table: "DanhGia",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_MaSP",
                table: "DanhGia",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__DanhGia__F5579F9E508AD176",
                table: "DanhGia",
                columns: new[] { "MaKH", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__DanhMuc__BC7B5FB6D8AF0494",
                table: "DanhMuc",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiaChi_MaKH",
                table: "DiaChi",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_GiaTungShop_MaSP",
                table: "GiaTungShop",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__GioHang__2725CF1F43B5B440",
                table: "GioHang",
                column: "MaKH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaBaoCao",
                table: "HoaDon",
                column: "MaBaoCao");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaDC",
                table: "HoaDon",
                column: "MaDC");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_MaKH",
                table: "HoaDon",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__536C85E491C1BECE",
                table: "KhachHang",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__KhachHan__A9D105342F6369F2",
                table: "KhachHang",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__4CF9E777BB23C81B",
                table: "NhanVien",
                column: "TenTK",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__A955A0AA72823FCE",
                table: "NhanVien",
                column: "CCCD",
                unique: true,
                filter: "[CCCD] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__NhanVien__A9D105345235AC39",
                table: "NhanVien",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuanLySanPham_MaKho",
                table: "QuanLySanPham",
                column: "MaKho");

            migrationBuilder.CreateIndex(
                name: "IX_QuanLySanPham_MaSP",
                table: "QuanLySanPham",
                column: "MaSP");

            migrationBuilder.CreateIndex(
                name: "UQ__QuanLySa__F557878A56D763F4",
                table: "QuanLySanPham",
                columns: new[] { "MaNV", "MaSP" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaDanhMuc",
                table: "SanPham",
                column: "MaDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNCC",
                table: "SanPham",
                column: "MaNCC");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNVQuanLy",
                table: "SanPham",
                column: "MaNVQuanLy");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_MaHD",
                table: "ThanhToan",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_MaKH",
                table: "ThongBao",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBao_MaNV",
                table: "ThongBao",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_TuongThich_MaDanhMucA",
                table: "TuongThich",
                column: "MaDanhMucA");

            migrationBuilder.CreateIndex(
                name: "IX_TuongThich_MaDanhMucB",
                table: "TuongThich",
                column: "MaDanhMucB");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhSanPham");

            migrationBuilder.DropTable(
                name: "Benchmark");

            migrationBuilder.DropTable(
                name: "ChiTietBuild");

            migrationBuilder.DropTable(
                name: "ChiTietChucNang");

            migrationBuilder.DropTable(
                name: "ChiTietDanhMucSP");

            migrationBuilder.DropTable(
                name: "ChiTietGioHang");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDon");

            migrationBuilder.DropTable(
                name: "ChiTietThongBao");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "GiaTungShop");

            migrationBuilder.DropTable(
                name: "QuanLySanPham");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "TuongThich");

            migrationBuilder.DropTable(
                name: "Build");

            migrationBuilder.DropTable(
                name: "ChucNang");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "ThongBao");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Kho");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "BaoCaoDoanhThu");

            migrationBuilder.DropTable(
                name: "DiaChi");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "KhachHang");
        }
    }
}
