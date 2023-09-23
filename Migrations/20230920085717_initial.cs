using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buois",
                columns: table => new
                {
                    TenBuoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buois", x => x.TenBuoi);
                });

            migrationBuilder.CreateTable(
                name: "BuoiThucHanhs",
                columns: table => new
                {
                    STT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuoiThucHanhs", x => x.STT);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Ten = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Ten);
                });

            migrationBuilder.CreateTable(
                name: "HocKyNamHocs",
                columns: table => new
                {
                    HK_NH = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NgayBatDau = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HocKyHienTai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocKyNamHocs", x => x.HK_NH);
                });

            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    MaHP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TenHocPhan = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false),
                    SoTietThucHanh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.MaHP);
                });

            migrationBuilder.CreateTable(
                name: "OCungs",
                columns: table => new
                {
                    DungLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCungs", x => x.DungLuong);
                });

            migrationBuilder.CreateTable(
                name: "PhanMems",
                columns: table => new
                {
                    IdPhanMem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhanMem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanMems", x => x.IdPhanMem);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    DungLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.DungLuong);
                });

            migrationBuilder.CreateTable(
                name: "Tuans",
                columns: table => new
                {
                    SoTuan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tuans", x => x.SoTuan);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhomHocPhans",
                columns: table => new
                {
                    MaNhomHP = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SoLuongSV = table.Column<int>(type: "int", nullable: false),
                    HocPhanMaHP = table.Column<string>(type: "nvarchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhomHocPhans", x => x.MaNhomHP);
                    table.ForeignKey(
                        name: "FK_NhomHocPhans_HocPhans_HocPhanMaHP",
                        column: x => x.HocPhanMaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP");
                });

            migrationBuilder.CreateTable(
                name: "CauHinhs",
                columns: table => new
                {
                    IdCauHinh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPUTen = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    RAMDungLuong = table.Column<int>(type: "int", nullable: true),
                    OCungDungLuong = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CauHinhs", x => x.IdCauHinh);
                    table.ForeignKey(
                        name: "FK_CauHinhs_CPUs_CPUTen",
                        column: x => x.CPUTen,
                        principalTable: "CPUs",
                        principalColumn: "Ten");
                    table.ForeignKey(
                        name: "FK_CauHinhs_OCungs_OCungDungLuong",
                        column: x => x.OCungDungLuong,
                        principalTable: "OCungs",
                        principalColumn: "DungLuong");
                    table.ForeignKey(
                        name: "FK_CauHinhs_RAMs_RAMDungLuong",
                        column: x => x.RAMDungLuong,
                        principalTable: "RAMs",
                        principalColumn: "DungLuong");
                });

            migrationBuilder.CreateTable(
                name: "GiangDays",
                columns: table => new
                {
                    HK_NH = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    GiangVienId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    MaNhomHP = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiangDays", x => new { x.HK_NH, x.GiangVienId, x.STT, x.MaNhomHP });
                    table.ForeignKey(
                        name: "FK_GiangDays_AspNetUsers_GiangVienId",
                        column: x => x.GiangVienId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_BuoiThucHanhs_STT",
                        column: x => x.STT,
                        principalTable: "BuoiThucHanhs",
                        principalColumn: "STT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_HocKyNamHocs_HK_NH",
                        column: x => x.HK_NH,
                        principalTable: "HocKyNamHocs",
                        principalColumn: "HK_NH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GiangDays_NhomHocPhans_MaNhomHP",
                        column: x => x.MaNhomHP,
                        principalTable: "NhomHocPhans",
                        principalColumn: "MaNhomHP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaiDatPhanMems",
                columns: table => new
                {
                    IdCauHinh = table.Column<int>(type: "int", nullable: false),
                    IdPhanMem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaiDatPhanMems", x => new { x.IdCauHinh, x.IdPhanMem });
                    table.ForeignKey(
                        name: "FK_CaiDatPhanMems_CauHinhs_IdCauHinh",
                        column: x => x.IdCauHinh,
                        principalTable: "CauHinhs",
                        principalColumn: "IdCauHinh",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaiDatPhanMems_PhanMems_IdPhanMem",
                        column: x => x.IdPhanMem,
                        principalTable: "PhanMems",
                        principalColumn: "IdPhanMem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phongs",
                columns: table => new
                {
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    SoLuongMayTinh = table.Column<int>(type: "int", nullable: false),
                    CauHinhIdCauHinh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongs", x => x.SoPhong);
                    table.ForeignKey(
                        name: "FK_Phongs_CauHinhs_CauHinhIdCauHinh",
                        column: x => x.CauHinhIdCauHinh,
                        principalTable: "CauHinhs",
                        principalColumn: "IdCauHinh");
                });

            migrationBuilder.CreateTable(
                name: "HocPhanPhuHops",
                columns: table => new
                {
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    MaHP = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhanPhuHops", x => new { x.SoPhong, x.MaHP });
                    table.ForeignKey(
                        name: "FK_HocPhanPhuHops_HocPhans_MaHP",
                        column: x => x.MaHP,
                        principalTable: "HocPhans",
                        principalColumn: "MaHP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HocPhanPhuHops_Phongs_SoPhong",
                        column: x => x.SoPhong,
                        principalTable: "Phongs",
                        principalColumn: "SoPhong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichThucHanhs",
                columns: table => new
                {
                    NgayThucHanh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SoPhong = table.Column<int>(type: "int", nullable: false),
                    TenBuoi = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    TuanSoTuan = table.Column<int>(type: "int", nullable: true),
                    GiangDayHK_NH = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    GiangDayGiangVienId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GiangDaySTT = table.Column<int>(type: "int", nullable: true),
                    GiangDayMaNhomHP = table.Column<string>(type: "nvarchar(15)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichThucHanhs", x => new { x.NgayThucHanh, x.SoPhong, x.TenBuoi });
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Buois_TenBuoi",
                        column: x => x.TenBuoi,
                        principalTable: "Buois",
                        principalColumn: "TenBuoi",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_GiangDays_GiangDayHK_NH_GiangDayGiangVienId_GiangDaySTT_GiangDayMaNhomHP",
                        columns: x => new { x.GiangDayHK_NH, x.GiangDayGiangVienId, x.GiangDaySTT, x.GiangDayMaNhomHP },
                        principalTable: "GiangDays",
                        principalColumns: new[] { "HK_NH", "GiangVienId", "STT", "MaNhomHP" });
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Phongs_SoPhong",
                        column: x => x.SoPhong,
                        principalTable: "Phongs",
                        principalColumn: "SoPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichThucHanhs_Tuans_TuanSoTuan",
                        column: x => x.TuanSoTuan,
                        principalTable: "Tuans",
                        principalColumn: "SoTuan");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaiDatPhanMems_IdPhanMem",
                table: "CaiDatPhanMems",
                column: "IdPhanMem");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_CPUTen",
                table: "CauHinhs",
                column: "CPUTen");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_OCungDungLuong",
                table: "CauHinhs",
                column: "OCungDungLuong");

            migrationBuilder.CreateIndex(
                name: "IX_CauHinhs_RAMDungLuong",
                table: "CauHinhs",
                column: "RAMDungLuong");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_GiangVienId",
                table: "GiangDays",
                column: "GiangVienId");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_MaNhomHP",
                table: "GiangDays",
                column: "MaNhomHP");

            migrationBuilder.CreateIndex(
                name: "IX_GiangDays_STT",
                table: "GiangDays",
                column: "STT");

            migrationBuilder.CreateIndex(
                name: "IX_HocPhanPhuHops_MaHP",
                table: "HocPhanPhuHops",
                column: "MaHP");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_GiangDayHK_NH_GiangDayGiangVienId_GiangDaySTT_GiangDayMaNhomHP",
                table: "LichThucHanhs",
                columns: new[] { "GiangDayHK_NH", "GiangDayGiangVienId", "GiangDaySTT", "GiangDayMaNhomHP" });

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_SoPhong",
                table: "LichThucHanhs",
                column: "SoPhong");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_TenBuoi",
                table: "LichThucHanhs",
                column: "TenBuoi");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanhs_TuanSoTuan",
                table: "LichThucHanhs",
                column: "TuanSoTuan");

            migrationBuilder.CreateIndex(
                name: "IX_NhomHocPhans_HocPhanMaHP",
                table: "NhomHocPhans",
                column: "HocPhanMaHP");

            migrationBuilder.CreateIndex(
                name: "IX_Phongs_CauHinhIdCauHinh",
                table: "Phongs",
                column: "CauHinhIdCauHinh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CaiDatPhanMems");

            migrationBuilder.DropTable(
                name: "HocPhanPhuHops");

            migrationBuilder.DropTable(
                name: "LichThucHanhs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PhanMems");

            migrationBuilder.DropTable(
                name: "Buois");

            migrationBuilder.DropTable(
                name: "GiangDays");

            migrationBuilder.DropTable(
                name: "Phongs");

            migrationBuilder.DropTable(
                name: "Tuans");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BuoiThucHanhs");

            migrationBuilder.DropTable(
                name: "HocKyNamHocs");

            migrationBuilder.DropTable(
                name: "NhomHocPhans");

            migrationBuilder.DropTable(
                name: "CauHinhs");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "OCungs");

            migrationBuilder.DropTable(
                name: "RAMs");
        }
    }
}
